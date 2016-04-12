using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS08Lib;

namespace HCS08Text
{
    class Program
    {
        class MyConsoleInterface : ConsoleInterface
        {
            public string ReadLine()
            {
                return Console.ReadLine();
            }

            public void Write(string s)
            {
                Console.Write(s);
            }

            public void WriteLine(string s)
            {
                Console.WriteLine(s);
            }
        }

        static void Main(string[] args)
        {
            MyConsoleInterface MyConsole = new MyConsoleInterface();
        }
    }
}
