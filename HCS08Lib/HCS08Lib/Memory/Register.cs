using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Memory
{
    public class Register : MemorySpace
    {
        public Register(uint memory_size) : base(memory_size) { }
    }
}
