using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS08Lib.Memory;

namespace HCS08Lib.Core.Addressing
{
    public class Inherent : DataAccess
    {
        public override byte GetOperand(CoreMemory mem, UInt16 offset)
        {
            return mem[offset];
        }
    }
}
