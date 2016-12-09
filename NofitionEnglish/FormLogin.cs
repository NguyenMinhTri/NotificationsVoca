using Parse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
namespace NofitionEnglish
{
    public partial class FormLogin : Form
    {
        DuLieuLichSu data;
        Button btnUp, btnDown,btnAcout;
        
        public FormLogin(DuLieuLichSu data,Button Up,Button Down,Button DangNhap)
        {
            InitializeComponent();
            btnAcout = DangNhap;
            
            btnUp = Up;
            btnDown = Down;
            ParseClient.Initialize("rJsGmzNY00iXnK2NqRPggOkTSJj3qzZfcJuLz6yx", "3Dnm8m0HEHufMQSfxhFRXHgS78iNk2Ry4pdf3ED8");
            this.data = data;
            tbPass.Text = Properties.Settings.Default.Password;
            tbUser.Text = Properties.Settings.Default.Username;
            cbGhiNho.Checked = Properties.Settings.Default.cbGhiNho;
            if (Properties.Settings.Default.cbGhiNho)
            {
                Login(Properties.Settings.Default.Username, Properties.Settings.Default.Password);
            }
        }

        async public void Updata()
        {
            if (ParseUser.CurrentUser.Username == null)
                return;
            btnDown.Enabled = btnLogin.Enabled = btnUp.Enabled = btnAcout.Enabled  = false;
            try
            {
                ParseQuery<ParseObject> query = ParseObject.GetQuery("History");
                query = query.Limit(1000);

                //query = query.Skip();
                query= query.WhereEqualTo( "User", ParseUser.CurrentUser.Username);



                IEnumerable<ParseObject> result = await query.FindAsync();
               
                 
                
            
                for (int i = 0; i <= result.Count<ParseObject>(); i++)
                {
                    result.ElementAt(i).DeleteAsync();
                }
                await result.SaveAllAsync();

            }
            catch
            {

            }
            List<ParseObject> listParse = new List<ParseObject>();
            IEnumerable<ParseObject> demo;
            
           int soCot = 0;
            foreach (DataGridViewRow dr in Form1.dataGridView1.Rows)
            {
                String Voca = (String)Form1.dataGridView1.Rows[dr.Index].Cells["Vocabulary"].Value.ToString();
                String PhienAm = (String)Form1.dataGridView1.Rows[dr.Index].Cells["PhienAm"].Value.ToString();
                String Mean = (String)Form1.dataGridView1.Rows[dr.Index].Cells["Mean"].Value.ToString();
                String Check = Form1.dataGridView1.Rows[dr.Index].Cells["Check"].Value.ToString();
                bool isCheck = false;
                if (Check == "1" || Check == "True")
                {
                    isCheck = true;
                }
              
                ParseObject tableHistory = new ParseObject("History");
                tableHistory["Voca"] = Voca;
                tableHistory["DauNhan"] = PhienAm;
                tableHistory["Mean"] = Mean;
                tableHistory["User"] = ParseUser.CurrentUser.Username;
                tableHistory["Check"] = isCheck;
                tableHistory["STT"] = Form1.dataGridView1.Rows.Count-dr.Index;
                listParse.Add(tableHistory);
               // await tableHistory.SaveAsync();
                soCot = dr.Index;
            }
            demo = listParse;
            await demo.SaveAllAsync();
            btnDown.Enabled = btnLogin.Enabled = btnUp.Enabled = btnAcout.Enabled = true;

        }


        async public void Login(String user,String pass)
        {
            try
            {

                await ParseUser.LogInAsync(user, pass);
                btnDown.Enabled = btnUp.Enabled = true;
                btnAcout.Text =  ParseUser.CurrentUser.Username;
                Form1.isLogin = true;
                this.Close();

            }
            catch (Exception e)
            {
                btnAcout.Text = "Error";
                Properties.Settings.Default.cbGhiNho = false;
                Properties.Settings.Default.Save();
                this.Invoke(new Action(() => { MessageBox.Show(this, e.Message); }));
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login(tbUser.Text, tbPass.Text);
        }

        async void Register(String username,String mail,String pass)
        {
            var user = new ParseUser()
            {
                Username = username,
                Password = pass,
                Email = mail,
            };



            await user.SignUpAsync();
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Register fmRes = new Register(this);
            fmRes.Show();
            
        }

        private void tbPass_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        async public void GetData()
        {
            try
            {
                btnDown.Enabled =btnLogin.Enabled=btnUp.Enabled=btnAcout.Enabled= false;
                ParseQuery<ParseObject> query = ParseObject.GetQuery("History");
                query = query.Limit(1000);

                //query = query.Skip();
                //query.WhereEqualTo("User", "tri");
                query = query.WhereEqualTo("User", ParseUser.CurrentUser.Username);
                IEnumerable<ParseObject> result = await query.FindAsync();

                 Form1.dataGridView1.Rows.Clear();
                string PhienAm = "♥";
                bool Check = false;
                for (int i = 0; i <= result.Count<ParseObject>(); i++)
                {
                    string Voca = result.ElementAt<ParseObject>(i).Get<string>("Voca");
                    string Mean = result.ElementAt<ParseObject>(i).Get<string>("Mean");
                    try {
                         PhienAm = result.ElementAt<ParseObject>(i).Get<string>("DauNhan");
                    }
                    catch
                    {
                        
                    }
                    try {
                        Check = result.ElementAt<ParseObject>(i).Get<bool>("Check");
                    }
                    catch
                    {

                    }
                    int Stt = result.ElementAt<ParseObject>(i).Get<int>("STT");
                    data.Write(ref Form1.dataGridView1, Voca, Mean, PhienAm, Check, Stt);

                }
                int a = result.Count<ParseObject>();
            }
            catch
            {

            }
            btnDown.Enabled = btnLogin.Enabled = btnUp.Enabled = btnAcout.Enabled = true;
            Form1.dataGridView1.Sort(Form1.dataGridView1.Columns[0], ListSortDirection.Descending);
        }

        private  void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.cbGhiNho = cbGhiNho.Checked;

            if (cbGhiNho.Checked)
            {
                Properties.Settings.Default.Username = tbUser.Text;
                Properties.Settings.Default.Password = tbPass.Text;
            }
            Properties.Settings.Default.Save();
        }
    }
}
