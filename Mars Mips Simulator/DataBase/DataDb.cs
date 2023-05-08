using Mars_Mips_Simulator.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mars_Mips_Simulator.DataBase
{
    class DataDb
    {
        Random rnd = new Random();
        
        int randomNumber;
        List<Data> data = new List<Data>() {
            new Data("0","0x10000000","0x0000033","0x0000000","0x0000000","0x0000000"),
            new Data("1","0x00000012","0x0000062","0x0000000","0x0000000","0x0000000"),
            new Data("2","0x10000025","0x0000000","0x0000000","0x0000000","0x0000000"),
            new Data("3","0x15000012","0x0000062","0x0000000","0x0000000","0x0000000"),
            new Data("4","0x15400012","0x0000000","0x0000000","0x0000000","0x0000000"),
            new Data("5","0x82000012","0x0000062","0x0000000","0x0000000","0x0000000"),
            new Data("6","0x10000012","0x0000000","0x0000000","0x0000000","0x0000000"),
            new Data("7","0x10000752","0x0000062","0x0000000","0x0000000","0x0000000"),
            new Data("8","0x10000098","0x0000000","0x0000000","0x0000000","0x0000000"),
            new Data("9","0x00000025","0x0000062","0x0000000","0x0000000","0x0000000"),
            new Data("10","0x10000065","0x0000000","0x0000000","0x0000000","0x0000000"),
            new Data("11","0x10000011","0x0000000","0x0000000","0x0000000","0x0000000"),
            new Data("12","0x10000002","0x0001542","0x0000000","0x0000000","0x0000000"),

        };
        public List<Data> getData()
        {
            return this.data;
        }
        public void setValue(String num,String value)
        {

           data.Where(p => p.number == num).First().value0 = value;
        }
    }

  
}
