using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS08Lib.Memory;

namespace HCS08Lib.Core.Addressing
{
    class Indexed : DataAccess
    {
        protected bool post_increment;

        public Indexed(bool post_inc)
        {
            post_increment = post_inc;
        }

        public override byte DataLength
        {
            get
            {
                return 0;
            }
        }

        protected virtual UInt16 GetAddress(CoreMemory mem, UInt16 offset, Registers reg)
        {
            return reg.HX;
        }

        public override byte GetOperand(CoreMemory mem, UInt16 offset, Registers reg)
        {
            byte retval = mem[GetAddress(mem, offset, reg)];
            if (post_increment)
                reg.HX++;
            return retval;
        }
    }

    class Indexed1 : Indexed
    {
        public Indexed1(bool post_inc) : base(post_inc) { }

        public override byte DataLength
        {
            get
            {
                return 1;
            }
        }

        protected override ushort GetAddress(CoreMemory mem, UInt16 offset, Registers reg)
        {
            return (UInt16)(reg.HX + mem[offset]);
        }
    }

    class Indexed2 : Indexed
    {
        public Indexed2(bool post_inc) : base(post_inc) { }

        public override byte DataLength
        {
            get
            {
                return 2;
            }
        }

        protected override ushort GetAddress(CoreMemory mem, UInt16 offset, Registers reg)
        {
            return (UInt16)(reg.HX + (mem[offset] << 8) + mem[(UInt16)(offset+1)]);
        }
    }
}
