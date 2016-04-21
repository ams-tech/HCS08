using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Core
{
    public class Vector
    {
        Memory.CoreMemory mem;
        UInt16 addr;

        public Vector(Memory.CoreMemory memory, UInt16 address)
        {
            if (address > (memory.Length - 1))
                throw new ArgumentException();
            addr = address;
            mem = memory;
        } 

        public UInt16 Payload
        {
            get { return (UInt16)((mem[addr] << 8) + mem[(UInt16)(addr+1)]); }
        }

        public static List<Vector> GetListOfVectors(Memory.CoreMemory memory, UInt16 start_address, int num_vectors)
        {
            List<Vector> retval = new List<Vector>();
            while (num_vectors-- > 0)
            {
                retval.Add(new Vector(memory, start_address));
                start_address += 2;
            }
            return retval;
        }
    }
}
