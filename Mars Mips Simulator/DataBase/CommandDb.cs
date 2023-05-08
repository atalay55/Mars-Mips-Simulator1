using System;
using System.Collections.Generic;
using System.Text;

namespace Mars_Mips_Simulator.DataBase
{
    class CommandDb
    {
        List<string> commands = new List<string>() { "add", "sub", "and", "or", "addi", "andi", "ori", "xor", "slt"
            ,"slti","j","jr","jal","sll","srl","sra","beq","bne","mul","muli","lui","lw","sw","lb","sb","mfhi","mflo",};

        public List<string> getCommand()
        {
            return this.commands;
        }

    }
}
