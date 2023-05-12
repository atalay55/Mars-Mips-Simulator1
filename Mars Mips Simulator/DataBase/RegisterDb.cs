using Mars_Mips_Simulator.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mars_Mips_Simulator.DataBase
{
    class RegisterDb
    {
        List<Register> registers;
        public RegisterDb()
        {
            registers = new List<Register>() {
            new Register("$zero","0","0x00000000"),
            new Register("$at","1","0x00000000"),
            new Register("$v0","2","0x00000000"),
            new Register("$v1","3","0x00000000"),
            new Register("$a0","4","0x00000000"),
            new Register("$a1","5","0x00000000"),
            new Register("$a2","6","0x00000000"),
            new Register("$a3","7","0x00000000"),
            new Register("$t0","8","0x00000000"),
            new Register("$t1","9","0x00000000"),
            new Register("$t2","10","0x00000000"),
            new Register("$t3","11","0x00000000"),
            new Register("$t4","12","0x00000000"),
            new Register("$t5","13","0x00000000"),
            new Register("$t6","14","0x00000000"),
            new Register("$t7","15","0x00000000"),
            new Register("$s0","16","0x00000000"),
            new Register("$s1","17","0x00000000"),
            new Register("$s2","18","0x00000000"),
            new Register("$s3","19","0x00000000"),
            new Register("$s4","20","0x00000000"),
            new Register("$s5","21","0x00000000"),
            new Register("$s6","22","0x00000000"),
            new Register("$s7","23","0x00000000"),
            new Register("$t8","24","0x00000000"),
            new Register("$t9","25","0x00000000"),
            new Register("$k0","26","0x00000000"),
            new Register("$k1","27","0x00000000"),
            new Register("$gp","28","0x00000000"),
            new Register("$sp","29","0x00000000"),
            new Register("$fp","30","0x00000000"),
            new Register("$ra","31","0x00000000"),
            new Register("$pc","32","0x00000000"),
            new Register("$hi","33","0x00000000"),
            new Register("$lo","34","0x00000000"),

        };

        }

      

        public List<Register> getRegisters()
        {
            return this.registers;
        }
        public Register getRegister(string registerName)
        {
           return this.registers.Where(p => p.name == registerName).First();
        }
        public void changeValue( Register r)
        {
             this.registers.Where(p => p.name == r.name ).First().value=r.value;
        }
        public void assignValue(Register r , string registerValue)
        {
            this.registers.Where(p => p.name == r.name).First().value = registerValue;
        }
      

    }
}
