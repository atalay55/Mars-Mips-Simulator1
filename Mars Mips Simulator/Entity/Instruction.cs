using System;
using System.Collections.Generic;
using System.Text;

namespace Mars_Mips_Simulator.Entity
{
    class Instruction
    {
        public Instruction(string data , int insMemory)
        {
            this.data = data;
            this.insMemory = insMemory;
        }
        public string data { get; set; }
        public int insMemory { get; set; }

    }
}
