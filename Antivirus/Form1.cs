using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Antivirus
{
    public partial class Antivirus : Form
    {
        public Antivirus()
        {
            InitializeComponent();
        }
        public string filepathvariable = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            //disables the scan button when the form loads to prevent crashing
            scanbtn.Enabled = false;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void browse1_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //Adds filters to allow the user to browse for specific file types
            openFileDialog1.Filter = "All Files|*.*|Image Files|*.gif;*.png*;*.jpeg;*.raw;*.bnp;*.tif;*.mpeg|Disk Files|*.iso;*.bin|Executable Files|*.exe;*.cmd;*.run;*.bat;*.msi|Video Files|*.mov;*.avi;*.mp4;*.dvi|Sound Files|*.mp3;*.acc;*.wav;*.wma|Data Files|*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx;*.txt;";
            //http://msdn.microsoft.com/en-us/library/61097ykx.aspx
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //Displays the filepath of the file in the text box
                filepath1.Text = openFileDialog1.FileName;
            //Gives a variable the value of the filepath
            filepathvariable = openFileDialog1.FileName;
            //empties the text box to prevent false data when scanning multiple files
            textBox1.Text = "";
            //enables the button once the browse button has been pressed
            scanbtn.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //http://stackoverflow.com/questions/18717182/convert-jpeg-image-to-hex-format
            //Declares a streamwriter function called 'store'.  Rewrites the file every time it is used.
            using (StreamWriter store = new StreamWriter("hexstore.txt"))
            {
                //Creates a variable called 'selected' Reads the filepath stored within the filepathvariable variable
                var selected = System.IO.File.ReadAllBytes(filepathvariable);
                
                //Ensures that if the text in the filepath is altered the the scan button is disabled
                if (!Directory.Exists(filepathvariable))
                    scanbtn.Enabled = false;
                else
                {
                    scanbtn.Enabled = true;

                }
                //Creates a variable called 'hex', joins the string characters together, selects all binary characters in the file and converts them to a unicode format
                var hex = string.Join("", selected.Select(x => x.ToString("X2")));
                //Function StreamWriter is called.  Writes the variable hex into the hexstore.txt file
                store.WriteLine(hex);
                //ends the streamwriter process
                store.Close();
                //declares the counter integer as 0
                int counter = 0;              
                //creates an array for the name of the malware
                string[] malname = new string[3];
                malname[0] = "Beep.sys";
                malname[1] = "AIDS/Taunt";
                malname[2] = "Vienna-1028";
                malname[3] = "Abraxas-15xx";
                malname[4] = "Chkbox-936";
                malname[5] = "DOS.SuperWorm-393";
                //creates an array for the type of malware
                string[] maltype = new string[3];
                maltype[0] = "Rootkit";
                maltype[1] = "Virus";
                maltype[2] = "Rootkit";
                malname[3] = "Trojan";
                malname[4] = "Virus";
                malname[5] = "Trojan";
                //creates an array for the hexadecimal of malware
                string[] malhex = new string[3];
                malhex[0] = "C37C085755FF15E00401005F5E5D5BC20800437265617465436C6F736521006850030100E863010";
                malhex[1] = "2a546869732046696c6520486173204265656e20496e66656374656420427920";
                malhex[2] = "b440b904048bd681ea130352515350b4";
                malhex[3] = "b90200b44ebaa80190cd21b8023c33c9ba9e00cd21b74093";
                malhex[4] = "b80363cd213dffff747f90908cd8488ed88b1e030083eb3e";
                malhex[5] = "e80b00e82300b000b44ccd210000e80301bb5302e8be00e89c008bebbf7202e84a00e82600e82f00";
                //

               //declares the string 'combo' and gives it an empty value
                string combo = "";
                //starts a loop to perform the following code until counter becomes equal or greater than 3
                while (counter < 3)
                {
                    //declares malhexstring and gives it the value of malhex array entry, depending on what value the counter holds
                    string malhexstring = malhex[counter];
                    //declares maltypestring and gives it the value of maltype array entry, depending on what value the counter holds
                    string maltypestring = maltype[counter];
                    //declares malnamestring and gives it the value of malname array entry, depending on what value the counter holds
                    string malnamestring = malname[counter];

                    
                    //http://www.dreamincode.net/forums/topic/70162-search-for-a-string-in-a-text-file/
                    //creates a string called content, gives it a value of the text stored in the file
                    string content = File.ReadAllText("hexstore.txt");
                    //checks the content string as to whether it contains the value of malhexstring.
                    if (content.IndexOf(malhexstring) > -1)
                    {
                        //if true:
                        //gives the value of combo itself, the counter, file path, malware type and malware name to display in the textbox
                        combo = combo + counter + "    |    " + filepathvariable + "    |    " + maltypestring + "    |    " + malnamestring + "\r\n";
                        //assigns the value of the textbox as combo
                        textBox1.Text = combo;
                        //increments the counter
                        counter++;
                    }
                        //if false
                    else
                    {
                        //increments the counter
                        counter++;
                    }
                }
                //checks as to whether the text box has no length (no input)
                if (textBox1.Text.Length == 0)
                   {
                    //gives the text box the value of 'no malware found!'
                       textBox1.Text = "No Malware Found!";
                   }
                //disables the scan button to prevent multiple clicks
                scanbtn.Enabled = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}

    
