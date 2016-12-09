using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace NofitionEnglish
{
    internal static class Program
    {
        private static Form1 form1;

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form1 = new Form1();

            Program.form1 = form1;
            SingleInstance.Run(form1, new StartupNextInstanceEventHandler(Program.StartupNextInstanceHandler));
        }

        public static void StartupNextInstanceHandler(object sender, StartupNextInstanceEventArgs e)
        {
            Program.form1.Show();

        }
    }
}
