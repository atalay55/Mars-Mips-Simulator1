using Mars_Mips_Simulator.DataBase;
using Mars_Mips_Simulator.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mars_Mips_Simulator
{
    class Function
    {
        DataDb dataDb;
        Data result;
        RegisterDb registerDb;
        public Function()
        {
            dataDb = new DataDb();
            registerDb = new RegisterDb();
        }

       public string add( string num1 , string num2)
        {


            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return (Convert.ToInt32(num1, 16) + (int.Parse(num2, System.Globalization.NumberStyles.AllowLeadingSign))).ToString();

            }
            else if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number)&& int.TryParse(num1, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return ((int.Parse(num1)) + (int.Parse(num2))).ToString();
            }
            else if (num2.Contains("-"))
            {
                return (Convert.ToInt32(num1, 16) - int.Parse(num2.TrimStart('-'))).ToString();
            }
            else
            {
      
                return (Convert.ToInt32(num2, 16) + Convert.ToInt32(num1, 16)).ToString();
            }

           
        }
        public string delete(string num1, string num2)
        {

            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return (Convert.ToInt32(num1, 16) - (int.Parse(num2))).ToString();

            }
            else if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number) && int.TryParse(num1, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return ((int.Parse(num1)) - (int.Parse(num2))).ToString();
            }
            else if (num2.Contains("-"))
            {
                return (Convert.ToInt32(num1, 16) + int.Parse(num2.TrimStart('-'))).ToString();
            }
            else
            {

                return (Convert.ToInt32(num2, 16) - (int.Parse(num1))).ToString();
            }



        }

        public string and(string num1, string num2)
        {

            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return (Convert.ToInt32(num1, 16) & (int.Parse(num2))).ToString();

            }
            else if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number) && int.TryParse(num1, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return ((int.Parse(num1)) & (int.Parse(num2))).ToString();
            }
            else
            {

                return (Convert.ToInt32(num2, 16) & Convert.ToInt32(num1, 16)).ToString();
            }



        }
       

        public string or(string num1, string num2)
        {

            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return (Convert.ToInt32(num1, 16) | (int.Parse(num2))).ToString();

            }
            else if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number) && int.TryParse(num1, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return ((int.Parse(num1)) | (int.Parse(num2))).ToString();
            }
            else
            {

                return (Convert.ToInt32(num2, 16) | Convert.ToInt32(num1, 16)).ToString();
            }



        }

        public string beq(string num1, string num2)
        {

            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return (Convert.ToInt32(num1, 16) == (int.Parse(num2)) ? 1 : 0).ToString();

            }
            else if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number) && int.TryParse(num1, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return ((int.Parse(num1)) == (int.Parse(num2)) ? 1 : 0).ToString();
            }
            else
            {
                Console.WriteLine(num1);
                Console.WriteLine(num2);

                return ((Convert.ToInt32(num2, 16) == Convert.ToInt32(num1, 16))? 1 : 0).ToString();
            }



        }
        public string bne(string num1, string num2)
        {

            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return (Convert.ToInt32(num1, 16) != (int.Parse(num2)) ? 1 : 0).ToString();

            }
            else if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number) && int.TryParse(num1, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return ((int.Parse(num1)) != (int.Parse(num2)) ? 1 : 0).ToString();
            }
            else
            {

                return ((Convert.ToInt32(num2, 16) != Convert.ToInt32(num1, 16)) ? 1 : 0).ToString();
            }



        }


        public string mult(string num1, string num2)
        {


            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return (Convert.ToInt64(num1, 16) * (int.Parse(num2))).ToString();

            }
            else if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number) && int.TryParse(num1, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return ((int.Parse(num1)) * (int.Parse(num2))).ToString();
            }
            else
            {

                return (Convert.ToInt64(num2, 16) * Convert.ToInt32(num1, 16)).ToString();
            }


        }

        public string lui(string registerName, string num2)
        {
            
        

            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                num2 = "0x"+int.Parse(num2).ToString("x");
              return num2;
                
               

            }


            return num2; 

        }





        public string xor(string num1, string num2)
        {

            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return (Convert.ToInt32(num1, 16) ^ (int.Parse(num2))).ToString();

            }
            else if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number) && int.TryParse(num1, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return ((int.Parse(num1)) ^ (int.Parse(num2))).ToString();
            }
            else
            {

                return (Convert.ToInt32(num2, 16) ^ Convert.ToInt32(num1, 16)).ToString();
            }

        }

        public string stl(string num1, string num2)
        {

            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return  (Convert.ToInt32(num1, 16) < (int.Parse(num2))) ? "1" : "0";

            }
        
            else
            {

                return (Convert.ToInt32(num1, 16) < Convert.ToInt32(num2, 16)) ? "1" : "0";
            }

        }
        public string srl(string num1, string num2)
        {


            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return (Convert.ToInt32(num1, 16) >> (int.Parse(num2))).ToString();

            }
            else if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number) && int.TryParse(num1, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return ((int.Parse(num1)) >> (int.Parse(num2))).ToString();
            }
            else
            {

                return (Convert.ToInt32(num2, 16) >> Convert.ToInt32(num1, 16)).ToString();
            }


        }
        public string sll(string num1, string num2)
        {


            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return (Convert.ToInt32(num1, 16) << (int.Parse(num2))).ToString();

            }
            else if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number) && int.TryParse(num1, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                return ((int.Parse(num1)) << (int.Parse(num2))).ToString();
            }
            else
            {

                return (Convert.ToInt32(num2, 16) << Convert.ToInt32(num1, 16)).ToString();
            }


        }


        public string lw(string num1, string num2)
        {

            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                string addressint = (Convert.ToInt64(num1, 16) + (int.Parse(num2))).ToString();
                string addresHex = "0x" + int.Parse(addressint).ToString("X");

                //this.result = this.dataDb.getData().Where(p => Convert.ToInt32(p.adress, 16) == Convert.ToInt32(addresHex, 16)).First();

                return (addresHex);

            }
            else
            {
                string addressint = (Convert.ToInt64(num2, 16) + Convert.ToInt64(num1, 16)).ToString();
                string addresHex = "0x" + int.Parse(addressint).ToString("X");

                // this.result = this.dataDb.getData().Where(p => Convert.ToInt32(p.adress, 16) == Convert.ToInt32(addresHex, 16)).First();
                //string value = result.value0;

                //return (value);
                return (addresHex);
            }


        }
        public string sw(string num1, string num2)
        {

            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
                string addressint = (Convert.ToInt32(num1, 16) + (int.Parse(num2))).ToString();
                string addresHex = "0x" + int.Parse(addressint).ToString("X");
               // this.result = this.dataDb.getData().Where(p => Convert.ToInt32(p.adress, 16) == Convert.ToInt32(addresHex, 16)).First();

                return (addresHex);

            }
            else
            {
                string addressint = (Convert.ToInt32(num2, 16) + Convert.ToInt64(num1, 16)).ToString();
                string addresHex = "0x" + int.Parse(addressint).ToString("X");
               // this.result = this.dataDb.getData().Where(p => Convert.ToInt32(p.adress, 16) == Convert.ToInt32(addresHex, 16)).First();

                return (addresHex);
            }


        }
        public string sb(string num1, string num2)
        {

            int number;
            if (int.TryParse(num2, System.Globalization.NumberStyles.HexNumber, null, out number))
            {
              
                string addressint = (Convert.ToInt64(num1, 16) + (int.Parse(num2))).ToString();
           
                 string addresHex = "0x" + int.Parse(addressint).ToString("X");
                // this.result = this.dataDb.getData().Where(p => Convert.ToInt32(p.adress, 16) == Convert.ToInt32(addresHex, 16)).First();

                return (addresHex);

            }
            else
            {
                string addressint = (Convert.ToInt64(num2, 16) + Convert.ToInt64(num1, 16)).ToString();
                string addresHex = "0x" + int.Parse(addressint).ToString("X");
                // this.result = this.dataDb.getData().Where(p => Convert.ToInt32(p.adress, 16) == Convert.ToInt32(addresHex, 16)).First();

                return (addresHex);
            }


        }

    }
}
