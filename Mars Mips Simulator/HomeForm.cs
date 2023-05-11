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
        int line, i = 0;


        Dictionary<string,int> labes;

        List<Instruction> ınstructions;

        public HomeForm()
        {
            this.registerdb = new RegisterDb();
            this.datadb = new DataDb();
            this.validation = new Validate();
            this.func = new Function();
            ınstructions = new List<Instruction>();
            this.ra = registerdb.getRegister("$ra");
            this.labes = new Dictionary<string, int>();
            this.pc = registerdb.getRegister("$pc");
            InitializeComponent();

        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            this.listView2.Visible = false;
            this.listView1.Items.Clear();
            this.listView2.Items.Clear();
            showAllRegister();
            showAllData();


         //   this.richTextBox1.Text = "slt $t0, $s1, $s2";

            this.listView1.LabelEdit = true;
            this.listView1.FullRowSelect = true;
            this.listView1.Refresh();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      

        private void showAllRegister()
        {
            foreach (Register s in registerdb.getRegisters())
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

        private void button2_Click(object sender, EventArgs e)
        {
            labes.Clear();
      
            this.richTextBox1.Clear();
            this.registerdb = new RegisterDb();
            this.listView1.Items.Clear();
            this.listView2.Items.Clear();
            showAllRegister();
            showAllData();
        

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
             

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listView2.Visible = true;

            createInstruction();
            pc.value = "0x00400000";

            try
            {

                while (i <= ınstructions.Count)
                {
                    if (ınstructions[i].data.Contains("exit")) { break; }

                    string mainText = richTextBox1.Lines[i];
                    string[] arraay = mainText.Split(" ");
                    this.functionName = arraay[0];
                    string[] constants = arraay.Skip(1).ToArray();
                    string variables = string.Join("", constants);

                    string[] variableList = variables.Split(",");


                    foreach (var item in labes)
                    {

                        if (functionName == "j" && item.Key==variableList[0])
                        {
                            labes.Remove(item.Key);
                            pc.value =(item.Value + 4).ToString("X");
                     
                        }
                        else if (functionName == "jal" || functionName == "jalr")
                        {
                            ra.value = (Convert.ToInt32(pc.value, 16)).ToString("X");
                            listView1.Items[31].SubItems[2].Text = ra.value;
                            line = i;
                            Console.WriteLine(item);
                            pc.value = (item.Value + 4).ToString("X");
                        }



                        else
                        {
                            continue;
                        }
                   

                    }


             

                    if (Convert.ToInt32(pc.value, 16) == ınstructions[i].insMemory)
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

                }

                this.listView1.Items.Clear();
                this.listView2.Items.Clear();
                showAllRegister();
                showAllData();
                ınstructions.Clear();


            }
            catch(Exception ex)
            {
                
                
            }






        }

        public void createInstruction( )
        {
          
           
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {

                string[] variableList = string.Join("", richTextBox1.Lines[i].Split(" ").Skip(1).ToArray()).Split(",");
                if (string.IsNullOrEmpty(richTextBox1.Lines[i]))
                {
                    continue;
                }
              
             
                else
                {

                    int val = this.insMemory + (i * 4);
                    this.ınstructions.Add(new Instruction(richTextBox1.Lines[i], val));
                    if (richTextBox1.Lines[i].Contains(":"))
                    {
                        labes.Add(richTextBox1.Lines[i].Substring(0,richTextBox1.Lines[i].Length-1), val);
                    }



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

                        }
                        catch
                        {
                            result.value = this.func.add(v1.value, v2.value);
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
                        }


                        result.value = "0x" + int.Parse(result.value).ToString("D8");
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
                        }


                        result.value = "0x" + int.Parse(result.value).ToString("D8");

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

                        }
                        result.value = "0x" + int.Parse(result.value).ToString("D8");
                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = result.value;

                        break;
                    case "mul":
                    case "muli":

                        try
                        {
                            result.value = this.func.mul(v1.value, variableList[2]);

                        }
                        catch
                        {
                            result.value = this.func.mul(v1.value, v2.value);

                        }
                        result.value = "0x" + int.Parse(result.value).ToString("D8");
                        listView1.Items[int.Parse(result.number)].SubItems[2].Text = result.value;
                        break;


                    case "xor":

                        result.value = this.func.xor(v1.value, v2.value);
                        result.value = "0x" + int.Parse(result.value).ToString("D8");
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
                        }
                        result.value = "0x" + int.Parse(result.value).ToString("D8");

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
                        }
                        result.value = "0x" + int.Parse(result.value).ToString("D8");
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
                        }
                        result.value = "0x" + int.Parse(result.value).ToString("D8");
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

                    case "sw":
                        string offset1 = variableList[1].Split("(")[0];
                        string base2 = variableList[1].Split("(")[1].Split(")")[0];
                        baseRegister = registerdb.getRegisters().Where(p => p.name == base2).First();
                        string number = this.func.sw(baseRegister.value, offset1);
                        //result.value = "0x" + int.Parse(result.value).ToString("X");
                        datadb.setValue(number, result.value);
                        listView2.Items[int.Parse(number)].SubItems[1].Text = result.value;

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
    }
}
