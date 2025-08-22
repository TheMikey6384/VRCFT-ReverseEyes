using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VRCFT_ReverseEyes
{
    public partial class Form1 : Form
    {
        public int delay;
        public bool pimaxFixed = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void startBTN_Click(object sender, EventArgs e)
        {
            Program.StartTasks(inPortTXT.Text, outPortTXT.Text, outIPTXT.Text);
        }

        private void extensiveLog_CheckedChanged(object sender, EventArgs e)
        {
            Program.extensiveLog = extensiveLog.Checked;
        }

        private void pimaxFixToggle_CheckedChanged(object sender, EventArgs e)
        {
            pimaxFixed = pimaxFixToggle.Checked;
        }

        private void delayTxtBX_TextChanged(object sender, EventArgs e)
        {
            delay = int.Parse(delayTxtBX.Text);
        }

        private void delLogsBTN_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory);

            foreach (string file in files)
            {
                if (Path.GetFileName(file).Contains("log_") && !Path.GetFileName(file).Equals(Program.currLogName))
                {
                    File.Delete(file);
                }
            }
        }
    }
}
