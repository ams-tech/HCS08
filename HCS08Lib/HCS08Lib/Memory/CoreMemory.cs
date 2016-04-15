using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Memory
{
    public class CoreMemory
    {
        public static int CORE_MEMORY_SPACE_SIZE = 0x10000;

        List<MemorySpace> memory = new List<MemorySpace>();
        int length = 0;

        public CoreMemory() { }

        public CoreMemory(List<MemorySpace> m)
        {
            foreach (MemorySpace memory in m)
            {
                this.AddMemorySpace(memory);
            }
        }

        public void AddMemorySpace(MemorySpace s)
        {
            if ((length + s.Length) > CORE_MEMORY_SPACE_SIZE)
                throw new InvalidOperationException("Memory space would exceed " + CORE_MEMORY_SPACE_SIZE.ToString());
            else
            {
                length += s.Length;
                memory.Add(s);
            }
        }

        public byte this[UInt16 addr]
        {
            get
            {
                if (addr < length)
                {
                    foreach (MemorySpace s in memory)
                    {
                        if (addr < s.Length)
                            return s[addr];
                        else
                            addr -= (UInt16)s.Length;
                    }
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (addr < length)
                {
                    foreach (MemorySpace s in memory)
                    {
                        if (addr < s.Length)
                        {
                            s[addr] = value;
                            return;
                        }
                        else
                            addr -= (UInt16)s.Length;
                    }
                }
                throw new IndexOutOfRangeException();
            }
        }
    }
}
