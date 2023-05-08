using System;
using System.Collections.Generic;
using System.Text;

namespace Mars_Mips_Simulator.Entity
{
    class Register
    {
        public Register( string name , string number ,string value) {
            this.name = name;
            this.number = number;
            this.value = value;
        }
        public string name { get; set; }
        public string number { get; set; }
        public string  value { get; set; }
    }
}
