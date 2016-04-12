using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Memory
{
    public class Flash : MemoryByte
    {
        public override byte value
        {
            set {}
        }

        public void Erase()
        {
            my_value = 0xFF;
        }

        public void WriteValue(byte b)
        {
            my_value &= (byte)~b;
        }
    }
}
