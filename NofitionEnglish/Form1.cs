using System;
using Parse;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Timers;
namespace NofitionEnglish
{
    public partial class Form1 : Form
    {
        static int i = 0;
        public static int laplai = 0;
        
        // StreamReader history = new StreamReader(string.Concat(Application.StartupPath, "\\history.bat"));
        public static System.Timers.Timer aTimer = new System.Timers.Timer(); // vòng lặp thông báo
        DuLieuLichSu Data;
        VoiceOX VoiceOX;
        public static StreamReader fsData = new StreamReader((string.Concat(Application.StartupPath, "\\anhviet109K.dict")));
        public Form1()
        {
            VoiceOX = new VoiceOX();
            InitializeComponent();
            Data = new DuLieuLichSu(ref dataGridView1);
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
            aTimer.Interval = 100;
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            btnUpdata.Enabled = btnDown.Enabled = false;
            this.ActiveControl = textBox1;
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // bool checkTrung = false;
            Data.Write(ref dataGridView1, textBox1.Text.Replace("\t", "").Replace("\r\n", "").Replace("\n", ""), textBox1.Text.Replace("\t", "").Replace("\r\n", "").Replace("\n", ""));
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa", "Chú Ý", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Data.Delete(ref dataGridView1);
            }
            else if (dialogResult == DialogResult.No)
            {
               
            }
           

        }
        List<int> countCheck = new List<int>();
        public static int iTB = 0;
        private void button3_Click(object sender, EventArgs e)
        {
      
            try
            {
                for (int k = iTB - 1; k >= 0; )
                {
                    try
                    {
                        toastNotificationsManager2.Notifications.Remove(toastNotificationsManager2.Notifications[k]);
                        k--;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch
            {

            }
            demTB = 0;
            laplai = 0;
            iTB = 0;
            Data.Update(ref dataGridView1);
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                try
                {
                    if (((int)dataGridView1.Rows[dr.Index].Cells["Check"].Value) == 1 || ((bool)dataGridView1.Rows[dr.Index].Cells["Check"].Value) == true)
                    {
                        countCheck.Add(dr.Index);
                        
                        iTB++;
                    }
                }
                catch
                {
                    try
                    {
                        if (((bool)dataGridView1.Rows[dr.Index].Cells["Check"].Value) == true)
                        {
                            countCheck.Add(dr.Index);
                           
                            iTB++;
                        }
                    }
                    catch
                    {

                    }
                }
                
                //

            }
            List<int> ramPosition = new List<int>();
            ramPosition = UniqueRandomGenerator(0, iTB - 1);
            for (int i = 0; i < iTB; i++)
            {
                if (dataGridView1.Rows[countCheck[ramPosition[i]]].Cells["PhienAm"].Value.ToString() != "♥" && dataGridView1.Rows[countCheck[ramPosition[i]]].Cells["PhienAm"].Value.ToString() != "")
                {

                    toastNotificationsManager2.Notifications.Add(
                    new DevExpress.XtraBars.ToastNotifications.ToastNotification(ramPosition[i].ToString(), null, dataGridView1.Rows[countCheck[ramPosition[i]]].Cells["Vocabulary"].Value.ToString() + "   |" + dataGridView1.Rows[countCheck[ramPosition[i]]].Cells["PhienAm"].Value.ToString() + "|", dataGridView1.Rows[countCheck[ramPosition[i]]].Cells["Mean"].Value.ToString(), ramPosition[i].ToString(), DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.Text02));
                    toastNotificationsManager2.Notifications[i].Sound = DevExpress.XtraBars.ToastNotifications.ToastNotificationSound.NoSound;
                }
                else
                {

                    toastNotificationsManager2.Notifications.Add(
                   new DevExpress.XtraBars.ToastNotifications.ToastNotification(ramPosition[i].ToString(), null, dataGridView1.Rows[countCheck[ramPosition[i]]].Cells["Vocabulary"].Value.ToString(), dataGridView1.Rows[countCheck[ramPosition[i]]].Cells["Mean"].Value.ToString(), ramPosition[i].ToString(), DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.Text02));
                    toastNotificationsManager2.Notifications[i].Sound = DevExpress.XtraBars.ToastNotifications.ToastNotificationSound.NoSound;
                }
            }
             noti = new Nofition(ref checkBox3);
            noti.Show();
            Data.Update(ref dataGridView1);
            aTimer.Interval = 100;
            aTimer.Enabled = true;
        
        }
        Nofition noti;
        public List<int> UniqueRandomGenerator(int minVal, int maxVal)
        {
            Random rand = new Random();
            SortedList<int, int> uniqueList = new SortedList<int, int>();
            for (int i = minVal; i <= maxVal; i++)
                uniqueList.Add(rand.Next(), i);

            return uniqueList.Values.ToList();
        }
        private void toastNotificationsManager1_TimedOut(object sender, DevExpress.XtraBars.ToastNotifications.ToastNotificationEventArgs e)
        {
            MessageBox.Show("hello");
        }
      
        void mutiSpeaks()
        {
            if(btnTN.Enabled)
            VoiceOX.GirlSpeak(toastNotificationsManager2.Notifications[demTB].Header);
            else
            {
                VoiceOX.GirlSpeak(TracNghiem.label1.Text);
            }
        }
        public static int demTB = 0;
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (aTimer.Interval == 100 || aTimer.Interval == 500 || aTimer.Interval == 5000)
                aTimer.Interval = int.Parse(textBox3.Text) * 1000;
            if (demTB == (iTB))
                demTB = 0;

            try
            {
                if (checkBox2.Checked)
                {
                    //mutiSpeak = new Thread(mutiSpeaks);
                    //mutiSpeak.Start();

                    mutiSpeaks();
                }
                //VoiceOX.GirlSpeak(toastNotificationsManager2.Notifications[demTB].Header);
            }
            catch
            {

            }
            if (btnTN.Enabled)
            {
                laplai++;
                if (laplai >= int.Parse(textBox4.Text))
                {
                    laplai = 0;

                    demTB++;

                }
            }
           
           
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text != "" && textBox1.Text != "")
                {
                    Data.Write(ref dataGridView1, textBox1.Text.Replace("\t", "").Replace("\r\n", "").Replace("\n", ""), textBox2.Text.Replace("\t", "").Replace("\r\n", "").Replace("\n", ""));
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    this.textBox1.Focus();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đủ 2 khung");
                    if (textBox1.Text == "")
                        this.textBox1.Focus();
                    else
                        this.textBox2.Focus();
                }
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char aa = e.KeyChar;
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemtilde)
            {

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                try
                {
                    dataGridView1.Rows[dr.Index].Cells["Check"].Value = checkBox1.Checked;

                }
                catch
                {

                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "2";
            }

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "1";
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox2.Text!="")
            {
                Data.Write(ref dataGridView1, textBox1.Text.Replace("\t", "").Replace("\r\n", "").Replace("\n", ""), textBox2.Text.Replace("\t", "").Replace("\r\n", "").Replace("\n", ""));
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox1.Focus();
            }
            else
            Data.Update(ref dataGridView1);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/nmtri.uit");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.Update(ref dataGridView1);
            try {
                fmLogin.Updata();
            }catch
            {

            }
        }
        
        private void btnCNPA_Click(object sender, EventArgs e)
        {
            Data.UpdatePhienAm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            AddVoca fAddVoca = new AddVoca(ref button4);
            fAddVoca.Show();

            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text != "" && textBox1.Text != "")
                {
                    Data.Write(ref dataGridView1, textBox1.Text.Replace("\t", "").Replace("\r\n", "").Replace("\n", ""), textBox1.Text.Replace("\t", "").Replace("\r\n", "").Replace("\n", ""));
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    this.textBox1.Focus();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đủ 2 khung");
                    if (textBox1.Text == "")
                        this.textBox1.Focus();
                    else
                        this.textBox2.Focus();
                }
            }
        }

        private void btnTN_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount<2)
            {
                MessageBox.Show("Số Lượng từ trong bảng phải nhiều hơn 4");
                return;

            }
            if(iTB==0)
            {
                MessageBox.Show("Chọn từ cần học và ấn nút [Nhắc nhở], trước khi nhấn nút [TRẮC NGHIỆM] ");
                return;
            }
            if (iTB != 0)
            {
                TracNghiem tNghiem = new TracNghiem(ref btnTN, ref checkBox3,ref checkBox2);
                tNghiem.Show();
            }
        }

        private void contextMenuStrip1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void btnAN_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
             this.Visible = true;
        }
        FormLogin fmLogin;
        public static bool isLogin = false;
        private void btnAsyn_Click(object sender, EventArgs e)
        {
            if (!isLogin)
            {
                fmLogin = new FormLogin(Data, btnUpdata, btnDown, btnAsyn);
                if (Properties.Settings.Default.cbGhiNho)
                {

                }
                else
                    fmLogin.Show();
                
            }
            else
            {
                Properties.Settings.Default.cbGhiNho = false;
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
                fmLogin = new FormLogin(Data, btnUpdata, btnDown, btnAsyn);

                isLogin = false;
                fmLogin.Show();

            }
            
        }

        private void btnUpdata_Click(object sender, EventArgs e)
        {
            fmLogin.Updata();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            fmLogin.GetData();
        }
    }
}

