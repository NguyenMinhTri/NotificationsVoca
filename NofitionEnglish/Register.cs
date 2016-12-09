using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parse;
namespace NofitionEnglish
{
    public partial class Register : Form
    {
        FormLogin fmLogin;
       
        public Register(FormLogin fmLogin)
        {
            InitializeComponent();
            this.fmLogin = fmLogin;
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            try
            {
                DangKi(tbUser.Text, tbMail.Text, tbPass.Text);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        async void DangKi(String User,String Mail,String Pass)
        {
           
                var user = new ParseUser()
                {
                    Username = User,
                    Password = Pass,
                    Email = Mail
                };

            // other fields can be set just like with ParseObject
            try
            {
                await user.SignUpAsync();
                this.Close();
            }
            catch(Exception e)
            {
                this.Invoke(new Action(() => { MessageBox.Show(this,e.Message ); }));
            }

        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            fmLogin.Visible = true;
        }
    }
}
