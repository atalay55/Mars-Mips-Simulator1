using Mars_Mips_Simulator.DataBase;
using Mars_Mips_Simulator.Entity;
using Mars_Mips_Simulator.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mars_Mips_Simulator
{
  
    public partial class HomeForm : Form
    {

        RegisterDb registerdb;
        ListViewItem item;
        DataDb datadb;
        Validate validation;
        Function func;
        string functionName;
        Register result,v1,v2, baseRegister,pc,ra;
        int insMemory = 0x00400000;
        int line, i, j ,jline= 0;


        Dictionary<string,int> labes;

        List<Instruction> ınstructions;
        List<string> codes;

        public HomeForm()
        {
            this.registerdb = new RegisterDb();
            this.datadb = new DataDb();
            this.validation = new Validate();
            this.func = new Function();
            this.ınstructions = new List<Instruction>();
            this.ra = registerdb.getRegister("$ra");
            this.labes = new Dictionary<string, int>();
            this.pc = registerdb.getRegister("$pc");
            this.codes = new List<string>();
            InitializeComponent();

        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            this.listView2.Visible = false;
            this.listView3.Visible = false;
            this.listView1.Items.Clear();
            this.listView2.Items.Clear();
            showAllRegister();
            showAllData();
            showAllInstruc();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      

        private void showAllRegister()
        {
            foreach (Register s in this.registerdb.getRegisters())
            {

                this.item = new ListViewItem(s.name);
                this.item.SubItems.Add(s.number);
                this.item.SubItems.Add(s.value);

                listView1.Items.Add(item);

            }
        }

        private void showAllData()
        {
            foreach (Data s in datadb.getData())
            {

                this.item = new ListViewItem(s.adress);
                this.item.SubItems.Add(s.value0);
                this.item.SubItems.Add(s.value1);
                this.item.SubItems.Add(s.value2);
                this.item.SubItems.Add(s.value3);



                listView2.Items.Add(item);

            }
        }
        private void showAllInstruc()
        {

            for (i = 0; i < ınstructions.Count; i++)
            {

                this.item = new ListViewItem(ınstructions[i].insMemory.ToString("X"));
                this.item.SubItems.Add(ınstructions[i].insMemory.ToString());
                this.item.SubItems.Add(codes[i]);
                this.item.SubItems.Add(ınstructions[i].data);
          



                listView3.Items.Add(item);



            }
        }

            private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            labes.Clear();
            this.richTextBox1.Visible = true;
            this.listView3.Visible = false;
            this.richTextBox1.Clear();
            this.registerdb = new RegisterDb();
            this.listView1.Items.Clear();
            this.listView2.Items.Clear();
            showAllRegister();
            showAllData();
        

        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.line = 0; this.i = 0; this.j = 0; this.jline = 0;
            this.listView2.Visible = true;
            this.richTextBox1.Visible = false;
            this.listView3.Visible = true;
            createInstruction();
            pc.value = "0x00400000";

            try
            {

                while (j <= ınstructions.Count)
                {
                    if (ınstructions[i].data.Contains("exit")) { break; }

                    string mainText = richTextBox1.Lines[i];
                    if (string.IsNullOrEmpty(mainText))
                    {
                        i++;
                        continue;
                    }
                    string[] arraay = mainText.Split(" ");
                    this.functionName = arraay[0];
                    string[] constants = arraay.Skip(1).ToArray();
                    string variables = string.Join("", constants);

                    string[] variableList = variables.Split(",");


                    foreach (var item in labes)
                    {

                        if (functionName == "j" && item.Key == variableList[0] && Convert.ToInt32(pc.value, 16) <= ınstructions[j].insMemory)
                        {
                            labes.Remove(item.Key);
                            pc.value =(item.Value + 4).ToString("X");
                     
                        }
                        else if (functionName == "jal" && item.Key.Trim(':') == variableList[0])
                        {
                            ra.value = (Convert.ToInt32(pc.value, 16)).ToString("X");
                            listView1.Items[31].SubItems[2].Text = ra.value;
                            line = i;
                            jline = j;
                            pc.value = (item.Value + 4).ToString("X");
                        }



                        else
                        {
                            continue;
                        }
                   

                    }


             

                    if (Convert.ToInt32(pc.value, 16) == ınstructions[j].insMemory)
                    {

                        result = registerdb.getRegisters().Where(p => p.name == variableList[0]).First();
                        if (variableList.Length > 1)
                        {
                            if (variableList[1].Substring(0, 1) == "$")
                            {
                                v1 = registerdb.getRegisters().Where(p => p.name == variableList[1]).First();
                            }
                        }
                        

                        if (variableList.Length > 2)
                        {
                            if (variableList[2].Substring(0, 1) == "$")
                            {
                                v2 = registerdb.getRegisters().Where(p => p.name == variableList[2]).First();
                            }
                        }
                        runInstruction(variableList);
                        pc.value = (Convert.ToInt32(pc.value, 16) + 4).ToString("X");

                    }

                  




                    i++;
                    j++;

                }

             


            }
            catch(Exception ex)
            {
                
                
            }


            this.listView1.Items.Clear();
            this.listView2.Items.Clear();
            this.listView3.Items.Clear();
            showAllRegister();
            showAllData();
            showAllInstruc();
            this.ınstructions.Clear();


        }

        public void createInstruction( )
        {

            int k = 0;

            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {


                string[] variableList = string.Join("", richTextBox1.Lines[i].Split(" ").Skip(1).ToArray()).Split(",");
                if (string.IsNullOrEmpty(richTextBox1.Lines[i]))
                {
                    continue;
                }
              
             
                else
                {

                    int val = this.insMemory + (k * 4);
                    this.ınstructions.Add(new Instruction(richTextBox1.Lines[i], val));
                    if (richTextBox1.Lines[i].Contains(":"))
                    {
                        labes.Add(richTextBox1.Lines[i].Substring(0,richTextBox1.Lines[i].Length-1), val);
                       
                    }
                    k++ ;



                }

   
            }

        }

        private void runInstruction(string[] variableList)
        {
            if (validation.checkCommondName(this.functionName))
            {
                switch (functionName)
                {



                    case "addi":
                    case "add":

                        // result.value = this.func.add(v1.value, v2.value);

                        try
                        {
                            result.value = this.func.add(v1.value, variableList[2]);

                            codes.Add(Convert.ToInt32("000000" + func.itypeCode(result, v1, variableList[2]) + "100000", 2).ToString("X"));

                        }
                        catch
                        {
                            result.value = this.func.add(v1.value, v2.value);
                            


                            codes.Add(Convert.ToInt32("000000" + func.rtypeCode(v1, v2, result) + "100000", 2).ToString("X"));
                        }
                        result.value =int.Parse(result.value).ToString("x");
                        result.value = "0x" + result.value; 
                        registerdb.changeValue(result);
                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = result.value;
                        break;

                    case "andi":
                    case "and":
                        try
                        {
                            result.value = this.func.and(v1.value, variableList[2]);

                        }
                        catch
                        {
                            result.value = this.func.and(v1.value, v2.value);

                            codes.Add(Convert.ToInt32("000000" + func.rtypeCode(v1, v2, result) + "100100", 2).ToString("X"));

                        }


                        result.value = "0x" + int.Parse(result.value).ToString("x");
                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = result.value;

                        break;
                    case "ori":
                    case "or":
                        try
                        {
                            result.value = this.func.or(v1.value, variableList[2]);

                        }
                        catch
                        {
                            result.value = this.func.or(v1.value, v2.value);
                            codes.Add(Convert.ToInt32("000000" + func.rtypeCode(v1, v2, result) + "100101", 2).ToString("X"));

                        }


                        result.value = "0x" + int.Parse(result.value).ToString("x");

                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = result.value;

                        break;
                    case "subi":
                    case "sub":
                        try
                        {
                            result.value = this.func.delete(v1.value, variableList[2]);

                        }
                        catch
                        {
                            result.value = this.func.delete(v1.value, v2.value);
                            codes.Add(Convert.ToInt32("000000" + func.rtypeCode(v1, v2, result) + "100010", 2).ToString("X"));


                        }
                        result.value = "0x" + int.Parse(result.value).ToString("x");
                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = result.value;

                        break;
                    case "mult":
                        long longValue;
                        try
                        {
                            long.TryParse(func.mult(result.value, variableList[1]), out longValue);

                            if (4294967296 < longValue)
                            {
                                string bitsel = Convert.ToString(longValue, 2).PadLeft(64, '0');
                                registerdb.getRegister("$hi").value = "0x" + Convert.ToInt32(bitsel.Substring(0, bitsel.Length / 2), 2).ToString("X");
                                registerdb.getRegister("$lo").value = "0x" + Convert.ToInt32(bitsel.Substring(bitsel.Length / 2), 2).ToString("X");
                            }

                            else
                            {

                                registerdb.getRegister("$lo").value = (longValue).ToString("x");
                            }


                        }
                       
                        catch
                        {

                            long.TryParse(func.mult(result.value, v1.value), out longValue);
                            
                            if (4294967296 < longValue) 
                            {
                                string bitsel = Convert.ToString(longValue, 2).PadLeft(64, '0');
                                registerdb.getRegister("$hi").value= "0x"+ Convert.ToInt32(bitsel.Substring(0,bitsel.Length/2), 2).ToString("X");
                                registerdb.getRegister("$lo").value = "0x" + Convert.ToInt32(bitsel.Substring(bitsel.Length/2), 2).ToString("X");
                                

                            }

                            else 
                            {
                                
                                registerdb.getRegister("$lo").value = (longValue).ToString("x");
                               

                            }


                        }
                      

                        break;


                    case "xor":

                        result.value = this.func.xor(v1.value, v2.value);
                        codes.Add(Convert.ToInt32("000000" + func.rtypeCode(v1, v2, result) + "100110", 2).ToString("X"));

                        result.value = "0x" + int.Parse(result.value).ToString("x");
                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = result.value;

                        break;

                    case "slti":
                    case "slt":
                        try
                        {
                            result.value = this.func.stl(v1.value, variableList[2]);

                        }
                        catch
                        {
                            result.value = this.func.stl(v1.value, v2.value);
                            codes.Add(Convert.ToInt32("000000" + func.rtypeCode(v1, v2, result) + "101010", 2).ToString("X"));

                        }

                        result.value = "0x" + int.Parse(result.value).ToString("x");

                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = result.value;

                        break;
                    case "sra":
                    case "srl":
                        try
                        {
                            result.value = this.func.srl(v1.value, variableList[2]);

                        }
                        catch
                        {
                            result.value = this.func.srl(v1.value, v2.value);
                            codes.Add(Convert.ToInt32("000000" + func.rtypeCode(v1, v2, result) + "000010", 2).ToString("X"));

                        }
                        result.value = "0x" + int.Parse(result.value).ToString("x");
                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = result.value;
                        break;

                       case "sll":
                        try
                        {
                            result.value = this.func.sll(v1.value, variableList[2]);

                        }
                        catch
                        {
                            result.value = this.func.sll(v1.value, v2.value);
                            codes.Add(Convert.ToInt32("000000" + func.rtypeCode(v1, v2, result) + "000000", 2).ToString("X"));

                        }
                        result.value = "0x" + int.Parse(result.value).ToString("x");
                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = result.value;
                        break;

                    case "beq":
                        string val;
                        try
                        {
                            val = this.func.beq(result.value, variableList[1]);

                        }
                        catch
                        {
                            val = this.func.beq(result.value,v1.value);
                        }

                        if (val== "1")
                        {
                            foreach (var item in labes)
                            {
                                labes.Remove(item.Key);
                                pc.value = (item.Value ).ToString("X");
                            }    
                          

                        }
                        break;
                    case "bne":
             
                        try
                        {
                            val = this.func.bne(result.value, variableList[1]);

                        }
                        catch
                        {
                            val = this.func.bne(result.value, v1.value);
                        }

                        if (val == "1")
                        {
                            foreach (var item in labes)
                            {
                                labes.Remove(item.Key);
                                pc.value = (item.Value).ToString("X");
                            }


                        }

                        break;
                    case "lui":
                        result = registerdb.getRegisters().Where(p => p.name == variableList[0]).First();
                        result.value = func.lui(variableList[0], variableList[1]);
                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = result.value;

                        break;
                    case "mflo":

                        registerdb.getRegister(result.name).value = registerdb.getRegister("$lo").value;
                        listView1.Items[int.Parse("34")].SubItems[2].Text = "0x" + registerdb.getRegister("$lo").value;

                        break;
                    case "mfhi":


                        registerdb.getRegister(result.name).value = registerdb.getRegister("$hi").value;
                        listView1.Items[int.Parse("33")].SubItems[2].Text = "0x" + registerdb.getRegister("hi").value;
                        break;
                  
                    case "lw":
                        string offset = variableList[1].Split("(")[0];
                        string base1 = variableList[1].Split("(")[1].Split(")")[0];
                        baseRegister = registerdb.getRegisters().Where(p => p.name == base1).First();
                        //result.value = this.func.lw(baseRegister.value, offset);
                        string address = this.func.lw(baseRegister.value, offset);
                        string dataValue = datadb.getData().Where(p => Convert.ToInt32(p.adress, 16) == Convert.ToInt32(address, 16)).First().value0;
                        registerdb.assignValue(result, dataValue);
                        //result.value = "0x" + int.Parse(result.value).ToString("X");
                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = dataValue;

                        break;

                    case "lb":
                         offset = variableList[1].Split("(")[0];
                         base1 = variableList[1].Split("(")[1].Split(")")[0];
                        baseRegister = registerdb.getRegisters().Where(p => p.name == base1).First();
                        //result.value = this.func.lw(baseRegister.value, offset);
                         address = this.func.lw(baseRegister.value, offset);

                        Console.WriteLine((Convert.ToInt64(address, 16) ));
                        if (256 < (Convert.ToInt32(address, 16)))
                        {
                            MessageBox.Show("Data error");
                            break;
                        }
                         dataValue = datadb.getData().Where(p => Convert.ToInt32(p.adress, 16) == Convert.ToInt32(address, 16)).First().value0;
                        registerdb.assignValue(result, dataValue);
                        //result.value = "0x" + int.Parse(result.value).ToString("X");
                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = dataValue;

                        break;

                    case "sw":

                        Data resultData;
                        string offset1 = variableList[1].Split("(")[0];
                        string base2 = variableList[1].Split("(")[1].Split(")")[0];
                        baseRegister = registerdb.getRegisters().Where(p => p.name == base2).First();
                        string addressHex = this.func.sw(baseRegister.value, offset1);
                     
                        try
                        {
                            resultData = this.datadb.getData().Where(p => Convert.ToInt32(p.adress, 16) == Convert.ToInt32(addressHex, 16)).First();
                            datadb.setValue(resultData.number, result.value);
                            listView2.Items[int.Parse(resultData.number)].SubItems[1].Text = result.value;
                        }
                        catch
                        {
                            resultData = datadb.createData("35", result.value, addressHex);
                            string[] row = { result.value, addressHex, "0x0000000", "0x0000000", "0x0000000" };
                            var satır = new ListViewItem(row);
                            listView2.Items.Add(satır);
                        }
                        break;
                    case "sb":

                    
                         offset1 = variableList[1].Split("(")[0];
                         base2 = variableList[1].Split("(")[1].Split(")")[0];
                        baseRegister = registerdb.getRegisters().Where(p => p.name == base2).First();
                         addressHex = this.func.sw(baseRegister.value, offset1);
                        if (256 < (Convert.ToInt32(addressHex, 16)))
                        {
                            MessageBox.Show("Data error");
                            break;
                        }
                        try
                        {
                            resultData = this.datadb.getData().Where(p => Convert.ToInt32(p.adress, 16) == Convert.ToInt32(addressHex, 16)).First();
                            datadb.setValue(resultData.number, result.value);
                            listView2.Items[int.Parse(resultData.number)].SubItems[1].Text = result.value;
                        }
                        catch
                        {
                            resultData = datadb.createData("35", result.value, addressHex);
                            string[] row = { result.value, addressHex, "0x0000000", "0x0000000", "0x0000000" };
                            var satır = new ListViewItem(row);
                            listView2.Items.Add(satır);
                        }
                        break;



                    case "jr":

                        pc.value = ra.value;
                        listView1.Items[31].SubItems[2].Text = "0x00000000";
                        if (ra.value == "0x00000000")
                        {
                            i = 9999999;
                        }
                        else
                        {
                            i = line;
                            j = jline;
                        }


                        ra.value = "0x00000000";

                        break;


                    case "j":
                       

                        break;

                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show( "line da " + this.functionName + " hatalı yazım");
            }


        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void createCode(string text)
        {
            string[] arraay = text.Split(" ");
            this.functionName = arraay[0];
            string[] constants = arraay.Skip(1).ToArray();
            string variables = string.Join("", constants);

            string[] variableList = variables.Split(",");

        }

    }
}
