using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NofitionEnglish
{
    public partial class Nofition : Form
    {
        CheckBox HienTB;
        public Nofition(ref CheckBox HienTB)
        {
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
           
          
            this.HienTB = HienTB;
           
            timer2.Start();
            timer1.Start();
   
        }
       
        private void Nofition_Load(object sender, EventArgs e)
        {
           // 
            this.Location = new Point(SystemInformation.VirtualScreen.Width - this.Width, SystemInformation.VirtualScreen.Height - this.Height - 40);
        


        }
     
        public void lbVoca_TextChanged(object sender, EventArgs e)
        {
            
        }
        public void ShowNofition()
        {
            if (!HienTB.Checked)
            {
                timer1.Stop();
                this.Location = new Point(SystemInformation.VirtualScreen.Width - this.Width, SystemInformation.VirtualScreen.Height - this.Height - 40);
                timer1.Start();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Location.X == SystemInformation.VirtualScreen.Width - this.Width)
            //if (this.Visible == true)
            {
                this.Location = new Point(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height - this.Height - 40);
                timer1.Stop();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lbCheckChange.Text = Form1.demTB.ToString() + Form1.laplai.ToString();
            try
            {
                if (Form1.demTB != 0 && Form1.laplai==0)
                {
                    lbVoca.Text = Form1.toastNotificationsManager2.Notifications[Form1.demTB - 1].Header;
                    lbMean.Text = Form1.toastNotificationsManager2.Notifications[Form1.demTB - 1].Body;
                }
                else if(Form1.laplai!=0 )
                {
                    lbVoca.Text = Form1.toastNotificationsManager2.Notifications[Form1.demTB ].Header;
                    lbMean.Text = Form1.toastNotificationsManager2.Notifications[Form1.demTB ].Body;

                }
                
            }
            catch
            {
                try
                {
                    lbVoca.Text = Form1.toastNotificationsManager2.Notifications[Form1.demTB].Header;
                    lbMean.Text = Form1.toastNotificationsManager2.Notifications[Form1.demTB].Body;
                }
                catch
                {

                }
                if (lbVoca.Text == "" && this.Location.X == SystemInformation.VirtualScreen.Width - this.Width)
                    this.Location = new Point(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height - this.Height - 40);
            }
        }

        private void lbCheckChange_TextChanged(object sender, EventArgs e)
        {
            ShowNofition();
        }

        
    }
}
