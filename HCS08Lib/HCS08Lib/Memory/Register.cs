using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Memory
{
    public class Register : MemorySpace
    {
        Dictionary<UInt16, Peripheral.PeripheralRegister> peripherals = new Dictionary<UInt16, Peripheral.PeripheralRegister>();

        public Register(uint memory_size) : base(memory_size) { }

        public void AddPeripheral(Peripheral.Peripheral p)
        {
            //Note that peripherals isn't thread protected.  Add all of these before execution starts
            foreach (Peripheral.PeripheralRegister icpu in p.GetCPURegisters())
            {
                if (peripherals.ContainsKey(icpu.AddressOffset))
                    throw new InvalidOperationException("Register space already has a peripheral at offset " + icpu.AddressOffset);
                else if (icpu.AddressOffset >= Length)
                    throw new IndexOutOfRangeException();
                else
                    peripherals[icpu.AddressOffset] = icpu;
            }
        }

        public override byte this[ushort i]
        {
            get
            {
                if (peripherals.ContainsKey(i))
                    return peripherals[i].Value;
                return base[i];
            }

            set
            {
                if (peripherals.ContainsKey(i))
                   peripherals[i].Value = value;
                base[i] = value;
            }
        }
    }
}
