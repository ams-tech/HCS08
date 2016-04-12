using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Memory
{
    public abstract class MemoryByte
    {
        protected byte my_value = 0xFF;

        public virtual byte value
        {
            get
            {
                return my_value;
            }

            set
            {
                my_value = value;
            }
        }
    }
}
