using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Memory
{
    public class Flash : MemorySpace
    {
        public Flash(uint memory_size) : base(memory_size) { }

        public override byte this[ushort i]
        {
            set
            {
            }
        }

        public void Erase(uint start_offset, uint len)
        {
            for(uint i = start_offset; i < (start_offset + len); i++)
            {
                memory_space[i] = 0xFF;
            }
        }

        public void WriteValue(uint offset, byte value)
        {
            memory_space[offset] &= (byte)~value;
        }

        public void WriteValue(uint offset, byte[] value)
        {
            Array.Copy(value, 0, memory_space, offset, value.Length);
        }
    }
}
