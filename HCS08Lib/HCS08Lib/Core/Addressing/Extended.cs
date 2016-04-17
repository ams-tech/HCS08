using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS08Lib.Memory;

namespace HCS08Lib.Core.Addressing
{
    public class Extended : DataAccess
    {
        public override byte DataLength
        {
            get
            {
                return 2;
            }
        }

        public override byte GetOperand(CoreMemory mem, UInt16 offset, Registers reg)
        {
            UInt16 addr = (UInt16)((mem[offset] << 8) + mem[(UInt16)(offset + 1)]);
            return mem[addr];
        }
    }
}
