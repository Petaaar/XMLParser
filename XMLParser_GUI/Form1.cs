using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace XMLParser_GUI
{
    public partial class Form1 : Form
    {
        private static void DisposeProcess(Process p)
        {
            p.Dispose();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                new XMLParser.XMLParser(textBox1.Text);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please fill in the path to the file first!");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process process = new Process();

            process.StartInfo.FileName = "https://github.com/Petaaar";

            process.Start();

            DisposeProcess(process);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process process = new Process();

            process.StartInfo.FileName = "https://github.com/Petaaar/XMLParser";

            process.Start();

            DisposeProcess(process);
        }
    }
}
