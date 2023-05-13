using System;
using System.Collections.Generic;
using System.Text;

namespace Mars_Mips_Simulator.DataBase
{
    class CommandDb
    {
        List<string> commands = new List<string>() { "add", "sub","subi", "and", "or", "addi", "andi", "ori", "xor", "slt"
            ,"slti","j","jr","jal","sll","srl","sra","beq","bne","mult","lui","lw","sw","lb","sb","mfhi","mflo","div"};

        public List<string> getCommand()
        {
            return this.commands;
        }

    }
}
