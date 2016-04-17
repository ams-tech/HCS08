using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS08Lib.Core
{
    public class Registers
    {
        public byte A;
        public byte H;
        public byte X;
        public UInt16 SP; //Stack Pointer
        
        public class ConditionCodes
        {
            public bool V; //Two's complement Overflow
            public bool H; // Half-carry (from bit 3)
            public bool I; //Interrupt mask
            public bool N; //Negative
            public bool Z; //Zero
            public bool C; //Carry
        }

        public ConditionCodes Flags = new ConditionCodes();

        public byte CCR
        {
            get
            {
                byte retval = 0x60;
                if (Flags.V)
                    retval |= (byte)(1 << 7);
                if (Flags.H)
                    retval |= (byte)(1 << 4);
                if (Flags.I)
                    retval |= (byte)(1 << 3);
                if (Flags.N)
                    retval |= (byte)(1 << 2);
                if (Flags.Z)
                    retval |= (byte)(1 << 1);
                if (Flags.C)
                    retval |= (byte)(1 << 0);
                return retval;
            }
            set
            {
                Flags.V = (value & (1 << 7)) != 0;
                Flags.H = (value & (1 << 4)) != 0;
                Flags.I = (value & (1 << 3)) != 0;
                Flags.N = (value & (1 << 2)) != 0;
                Flags.Z = (value & (1 << 1)) != 0;
                Flags.C = (value & (1 << 0)) != 0;
            }
        }

        public byte SPL
        {
            get
            {
                return (byte)(SP & 0xFF);
            }
            set
            {
                SP = (UInt16)((SP & 0xFF00) + value);
            }
        }

        public byte SPH
        {
            get
            {
                return (byte)((SP >> 8) & 0xFF);
            }
            set
            {
                SP = (UInt16)((SP & 0xFF) + (value << 8));
            }
        }

        public UInt16 HX
        {
            get
            {
                return (UInt16)((H << 8) + X);
            }
            set
            {
                X = (byte)(value & 0xFF);
                H = (byte)((value >> 8) & 0xFF);
            }
        }
    }
}
