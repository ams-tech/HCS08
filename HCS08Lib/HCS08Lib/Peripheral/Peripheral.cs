﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Peripheral
{
    public abstract class iRegister
    {
        public abstract byte Value
        {
            get;
            set;
        }

        public abstract UInt16 RegisterOffset
        {
            //Effectively becomes the address when used for low-page registers
            //Effectively gets an offset of 0x1800 when used for high-page registers
            get;
        }
    }

    public abstract class Peripheral
    {
        protected List<iRegister> CPURegisters = new List<iRegister>();

        public List<iRegister> GetCPURegisters()
        {
            return CPURegisters;
        }
    }
}
