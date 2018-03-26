using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SelfUpdatingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            var args = Environment.GetCommandLineArgs();
            InitializeComponent();
            if (args.Length != 0 && args.Contains("ShowVersion"))
            {
                this.okButton.Visible = true;
                this.okButton.Enabled = true;
            }
            else
            {
                this.updateButton.Visible = true;
                this.updateButton.Enabled = true;
            }
            versionTextBox.Text += FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                File.Replace(@"new\test.exe", "test.exe", null);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Application.Exit();
            }
           
            Application.Restart();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
