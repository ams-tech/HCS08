using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib
{
    class MC9S08KS32
    {
        List<Memory.MemorySpace> memory_list = new List<Memory.MemorySpace>()
        {
            new Memory.Register(0x80),
            new Memory.RAM(0x1000),
            new Memory.NullMemory(0x6F80),
            new Memory.Flash(0x8000)
        };

        public MC9S08KS32()
        {
            Memory.CoreMemory memory_space = new Memory.CoreMemory(memory_list);
        }
    }
}
