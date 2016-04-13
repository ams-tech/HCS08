using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Memory
{
    class NullMemory : MemorySpace
    {
        public static byte NULL_BYTE_VALUE = 0xFF;

        public NullMemory(uint memory_size) : base(memory_size) { }

        public override byte this[ushort i]
        {
            get
            {
                return NULL_BYTE_VALUE;
            }
            set
            {
            }
        }
    }
}
