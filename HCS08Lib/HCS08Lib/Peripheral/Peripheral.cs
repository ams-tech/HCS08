using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Peripheral
{
    public class iRegister
    {
        public static uint NUM_BITS = 8;

        protected byte current_value;
        UInt16 register_offset;
        
        public delegate byte OnGetDelegate(iRegister register);
        public delegate void OnSetDelegate(iRegister register, byte new_value);

        protected OnGetDelegate OnGetValue = null;
        protected OnSetDelegate OnSetValue = null;

        public iRegister() { }

        public struct Bits
        {
            public uint num_bits;
            public string short_name;
            public string long_name;
            public string description;
            public string values;

            public Bits(uint _num_bits, string _short_name, string _long_name, string _description, string _values)
            {
                num_bits = _num_bits;
                short_name = _short_name;
                long_name = _long_name;
                description = _description;
                values = _values;
            }

            public static Bits NULL_BIT = new Bits(
                1, "NULL", "Unused Bit", "This bit has no assigned value", "Always 1 when read.  Writes have no effect.");
        }

        List<Bits> my_bits = new List<Bits>();
        uint num_bits = 0;
        
        public void RegisterBits(Bits bits)
        {
            //Note that bits need to be registered in order from highest to lowest (7 to 0)
            if ((bits.num_bits + num_bits) >= NUM_BITS)
                throw new InvalidOperationException();
            num_bits += bits.num_bits;
            my_bits.Add(bits);
        }

        public iRegister(UInt16 offset, OnGetDelegate on_get, OnSetDelegate on_set)
        {
            OnGetValue = on_get;
            OnSetValue = on_set;
            register_offset = offset;
        }

        public iRegister(UInt16 offset, OnGetDelegate on_get, OnSetDelegate on_set, List<Bits> _bits)
        {
            OnGetValue = on_get;
            OnSetValue = on_set;
            register_offset = offset;
            foreach (var bit in _bits)
            {
                RegisterBits(bit);
            }
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

        public UInt16 RegisterOffset
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
        protected virtual List<iRegister> CPURegisters
        {
            get { return new List<iRegister>(); }
        }

        public List<iRegister> GetCPURegisters()
        {
            return CPURegisters;
        }
    }
}
