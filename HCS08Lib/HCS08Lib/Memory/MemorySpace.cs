using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Memory
{
    public abstract class MemorySpace
    {
        protected byte[] memory_space;

        public MemorySpace(uint memory_size)
        {
            memory_space = new byte[memory_size];
        }

        public virtual byte this[UInt16 i]
        {
            get
            {
                return memory_space[i];
            }
            set
            {
                memory_space[i] = value;
            }
        }

        public int Length
        {
            get { return memory_space.Length; }
        }
    }
}
