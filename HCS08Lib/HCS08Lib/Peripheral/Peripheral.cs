using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HCS08Lib.Peripheral
{
    public class PeripheralRegisterInfo
    { 

        public struct RegisterDescription
        {
            public string short_name;
            public string long_name;
            public string description;

            public RegisterDescription(string _short_name, string _long_name, string _description)
            {
                short_name = _short_name;
                long_name = _long_name;
                description = _description;
            }
        }

        public struct BitDescription
        {
            public uint num_bits;
            public string short_name;
            public string long_name;
            public string description;
            public string values;

            public BitDescription(uint _num_bits, string _short_name, string _long_name, string _description, string _values)
            {
                num_bits = _num_bits;
                short_name = _short_name;
                long_name = _long_name;
                description = _description;
                values = _values;
            }

            public static BitDescription NULL_BIT = new BitDescription(
                1, "NULL", "Unused Bit", "This bit has no assigned value", "Always 1 when read.  Writes have no effect.");
        }

        public static XmlElement BlankXmlElement(XmlDocument doc)
        {
            XmlElement retval = doc.CreateElement(nameof(PeripheralRegisterInfo));
            XmlElement register_description = doc.CreateElement(nameof(RegisterDescription));
            XmlElement bit_description = doc.CreateElement(nameof(BitDescription));
            
            foreach (var field in typeof(RegisterDescription).GetFields())
            {
                register_description.AppendChild(doc.CreateElement(field.Name));
            }

            foreach (var field in typeof(BitDescription).GetFields())
            {
                bit_description.AppendChild(doc.CreateElement(field.Name));
            }

            retval.AppendChild(register_description);
            retval.AppendChild(bit_description);

            return retval;
        }

        public static PeripheralRegisterInfo ParseXmlNode(XmlNode root)
        {
            RegisterDescription? register_descritpion = null;
            List<BitDescription> bit_descriptions = new List<BitDescription>();

            if (root.Name != nameof(PeripheralRegisterInfo))
                throw new ArgumentException();
            foreach (XmlNode child in root.ChildNodes)
            {
                if (child.Name == nameof(RegisterDescription))
                {
                    if (register_descritpion != null)
                        throw new ArgumentException();
                    register_descritpion = new RegisterDescription(
                        child.SelectSingleNode("/" + nameof(RegisterDescription.short_name)).Value,
                        child.SelectSingleNode("/" + nameof(RegisterDescription.long_name)).Value,
                        child.SelectSingleNode("/" + nameof(RegisterDescription.description)).Value);
                }
                else if (child.Name == nameof(BitDescription))
                {
                    bit_descriptions.Add(new BitDescription(
                        uint.Parse(child.SelectSingleNode("/" + nameof(BitDescription.num_bits)).Value),
                        child.SelectSingleNode("/" + nameof(BitDescription.short_name)).Value,
                        child.SelectSingleNode("/" + nameof(BitDescription.long_name)).Value,
                        child.SelectSingleNode("/" + nameof(BitDescription.description)).Value,
                        child.SelectSingleNode("/" + nameof(BitDescription.values)).Value));
                }
                else
                    throw new ArgumentException();
            }

            return new PeripheralRegisterInfo(register_descritpion.Value, bit_descriptions);
        }

        RegisterDescription register_info;
        List<BitDescription> bit_info;
        private RegisterDescription? register_descritpion;
        private List<BitDescription> bit_descriptions;

        public PeripheralRegisterInfo(RegisterDescription reg, List<BitDescription> bit)
        {
            register_info = reg;
            bit_info = bit;
        }

    }

    public class PeripheralRegister
    {
        public static uint NUM_BITS = 8;

        protected byte current_value;
        UInt16 register_offset;
        
        public delegate byte OnGetDelegate(PeripheralRegister register);
        public delegate void OnSetDelegate(PeripheralRegister register, byte new_value);

        protected OnGetDelegate OnGetValue = null;
        protected OnSetDelegate OnSetValue = null;

        protected PeripheralRegisterInfo RegisterInfo;

        public PeripheralRegister() { }

        public PeripheralRegister(UInt16 offset, PeripheralRegisterInfo info, OnGetDelegate on_get, OnSetDelegate on_set)
        {
            OnGetValue = on_get;
            OnSetValue = on_set;
            register_offset = offset;
            RegisterInfo = info;
        }

        public virtual byte Value
        {
            get
            {
                if (OnGetValue != null)
                    return OnGetValue(this);
                else
                    return current_value;
            }
            set
            {
                if (OnSetValue != null)
                    OnSetValue(this, value);
                else
                    current_value = value;
            }
        }

        public UInt16 AddressOffset
        {
            //Effectively becomes the address when used for low-page registers
            //Effectively gets an offset of 0x1800 when used for high-page registers
            get
            {
                return register_offset;
            }
        }
    }

    public abstract class Peripheral
    {
        protected virtual List<PeripheralRegister> CPURegisters
        {
            get { return new List<PeripheralRegister>(); }
        }

        public List<PeripheralRegister> GetCPURegisters()
        {
            return CPURegisters;
        }
    }
}
