using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Memory
{
    public class MemorySpace
    {
        static int MEMORY_SPACE_SIZE = 0xFFFF;
        MemoryByte[] memory_space = new MemoryByte[MEMORY_SPACE_SIZE];

        public MemorySpace(MemoryByte[] memory)
        {

        }

        public byte this[UInt16 i]
        {
            get
            {
                return memory_space[i].value;
            }
            set
            {
                memory_space[i].value = value;
            }
        }

        public int Length
        {
            get { return MEMORY_SPACE_SIZE; }
        }
    }
}
