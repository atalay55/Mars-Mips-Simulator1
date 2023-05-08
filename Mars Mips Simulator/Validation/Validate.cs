using Mars_Mips_Simulator.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mars_Mips_Simulator.Validation
{
    class Validate
    {

        CommandDb commandDb;

        public Validate()
        {
            this.commandDb = new CommandDb();
        }



       public bool checkCommondName(string name)
        {
          
            return commandDb.getCommand().Contains(name);

        }
    }
}
