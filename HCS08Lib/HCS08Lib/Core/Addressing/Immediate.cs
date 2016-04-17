using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS08Lib.Memory;

namespace HCS08Lib.Core.Addressing
{
    public class Immediate : DataAccess
    {
        public override byte DataLength
        {
            get
            {
                return 1;
            }
        }

        public override byte GetOperand(CoreMemory mem, UInt16 offset, Registers reg)
        {
            return mem[offset];
        }
    }
}
