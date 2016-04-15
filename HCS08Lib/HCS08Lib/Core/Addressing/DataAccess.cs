using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS08Lib;

namespace HCS08Lib.Core.Addressing
{
    public abstract class DataAccess
    {
        //Offset is the first byte after the instruction i.e. the first byte that's fed into the MMU
        public abstract byte GetOperand(Memory.CoreMemory mem, UInt16 offset);
    }
}
