using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;
using System.Windows.Forms;
using System.IO;
using System.Speech.Synthesis;
using System.Net;
using System.Threading;

namespace NofitionEnglish
{
    public partial class TracNghiem : Form
    {
        Button btnTN;
        CheckBox AnTB;
        CheckBox TatPA;
        WindowsMediaPlayer Sound;
        VoiceOX VoiceOX;
        Score DiemTracNghiem;
        public TracNghiem(ref Button btnTN,ref CheckBox AnTB,ref CheckBox TatPA)
        {
            this.btnTN = btnTN;
            this.AnTB = AnTB;
            this.TatPA = TatPA;
            InitializeComponent();
            timer1.Start();
            VoiceOX = new NofitionEnglish.VoiceOX();
            Sound = new WindowsMediaPlayer();
            DiemTracNghiem = new Score(ref label7);
            cbbSelect.SelectedIndex = 0;
            label7.Text = "Score True/False : " + DiemTracNghiem.ScoreDung.ToString() + "/" + DiemTracNghiem.ScoreSai.ToString();
        }

        private void TracNghiem_Load(object sender, EventArgs e)
        {
            btnTN.Enabled = false;
            AnTB.Checked = true;
            TatPA.Checked = false;
        }
        public List<string> radomTracNghiemViet(int TB)
        {
            TB = TB - 1;
            // lay ramdom 4 cot trong data xong kiem tra co trung thi lap len khong thi remove xong add vao
            List<int> ramdomMean = new List<int>();
            ramdomMean = UniqueRandomGenerator(0, Form1.dataGridView1.RowCount);
            ramdomMean.RemoveRange(4, Form1.dataGridView1.RowCount - 3);
            for (int i = 0; i <= 3; i++)
            {
                if (Form1.dataGridView1.Rows[ramdomMean[i]].Cells["Mean"].Value.ToString() == label1.Text)
                {
                    ramdomMean.RemoveAt(i);
                }

            }
            try
            {
                ramdomMean.RemoveAt(3);
                ramdomMean.Add(TB);
            }
            catch
            {
                ramdomMean.Add(TB);
            }
            List<string> RamDomString = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                {

                    RamDomString.Add(lbDA.Text);


                }
                else
                    RamDomString.Add(Form1.dataGridView1.Rows[ramdomMean[i]].Cells["Vocabulary"].Value.ToString());
            }


            return RamDomString;
        }
        //Anh

        public List<string> radomTracNghiem(int TB)
        {
            TB = TB - 1;
            // lay ramdom 4 cot trong data xong kiem tra co trung thi lap len khong thi remove xong add vao
            List<int> ramdomMean = new List<int>();
            ramdomMean = UniqueRandomGenerator(0, Form1.dataGridView1.RowCount);
            ramdomMean.RemoveRange(4, Form1.dataGridView1.RowCount - 3);
            for (int i = 0; i <= 3;i++ )
            {
                if (Form1.dataGridView1.Rows[ramdomMean[i]].Cells["Vocabulary"].Value.ToString() == label1.Text)
                {
                    ramdomMean.RemoveAt(i);
                }

            }
            try
            {
                ramdomMean.RemoveAt(3);
                ramdomMean.Add(TB);
            }
            catch
            {
                ramdomMean.Add(TB);
            }
            List<string> RamDomString=new List<string>();
            for (int i = 0; i < 4;i++ )
            {
                if (i == 3)
                {
                    
                        RamDomString.Add(lbDA.Text);
                    
                    
                }
                else
                    RamDomString.Add(Form1.dataGridView1.Rows[ramdomMean[i]].Cells["Mean"].Value.ToString());
            }
                
           
            return RamDomString;
        }
        public List<int> UniqueRandomGenerator(int minVal, int maxVal)
        {
            Random rand = new Random();
            SortedList<int, int> uniqueList = new SortedList<int, int>();
            for (int i = minVal; i <= maxVal; i++)
                uniqueList.Add(rand.Next(), i);

            return uniqueList.Values.ToList();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = Form1.laplai.ToString() + Form1.demTB.ToString();
            
        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
            if (Form1.demTB > Form1.iTB)
                Form1.demTB = 0;
            if(cbbSelect.SelectedIndex==0)
            try
            {
                
                List<int> ramdom = new List<int>();
                ramdom = UniqueRandomGenerator(0, 3);
                int a = Form1.demTB;
                label1.Text = Form1.toastNotificationsManager2.Notifications[a - 1].Header;
                lbDA.Text = Form1.toastNotificationsManager2.Notifications[a-1].Body;
                VoiceOX.GirlSpeak(label1.Text);
                radioButton1.Text = radomTracNghiem(Form1.demTB)[ramdom[0]];
                radioButton2.Text = radomTracNghiem(Form1.demTB)[ramdom[1]];
                radioButton3.Text = radomTracNghiem(Form1.demTB)[ramdom[2]];
                radioButton4.Text = radomTracNghiem(Form1.demTB)[ramdom[3]];
            }
            catch
            {
                List<int> ramdom = new List<int>();
                ramdom = UniqueRandomGenerator(0, 3);
                int a = Form1.demTB;
                label1.Text = Form1.toastNotificationsManager2.Notifications[a].Header;
                lbDA.Text = Form1.toastNotificationsManager2.Notifications[a].Body;
                VoiceOX.GirlSpeak(label1.Text);
                radioButton1.Text = radomTracNghiem(Form1.demTB)[ramdom[0]];
                radioButton2.Text = radomTracNghiem(Form1.demTB)[ramdom[1]];
                radioButton3.Text = radomTracNghiem(Form1.demTB)[ramdom[2]];
                radioButton4.Text = radomTracNghiem(Form1.demTB)[ramdom[3]];
                Form1.demTB++;
            }
            else
            {
                try
                {

                    List<int> ramdom = new List<int>();
                    ramdom = UniqueRandomGenerator(0, 3);
                    int a = Form1.demTB;
                    label1.Text = Form1.toastNotificationsManager2.Notifications[a - 1].Body;
                    lbDA.Text = Form1.toastNotificationsManager2.Notifications[a - 1].Header;
                   // VoiceOX.GirlSpeak(label1.Text);
                    radioButton1.Text = radomTracNghiemViet(Form1.demTB)[ramdom[0]];
                    radioButton2.Text = radomTracNghiemViet(Form1.demTB)[ramdom[1]];
                    radioButton3.Text = radomTracNghiemViet(Form1.demTB)[ramdom[2]];
                    radioButton4.Text = radomTracNghiemViet(Form1.demTB)[ramdom[3]];
                }
                catch
                {
                    List<int> ramdom = new List<int>();
                    ramdom = UniqueRandomGenerator(0, 3);
                    int a = Form1.demTB;
                    label1.Text = Form1.toastNotificationsManager2.Notifications[a].Header;
                    lbDA.Text = Form1.toastNotificationsManager2.Notifications[a].Body;
                    //VoiceOX.GirlSpeak(label1.Text);
                    radioButton1.Text = radomTracNghiemViet(Form1.demTB)[ramdom[0]];
                    radioButton2.Text = radomTracNghiemViet(Form1.demTB)[ramdom[1]];
                    radioButton3.Text = radomTracNghiemViet(Form1.demTB)[ramdom[2]];
                    radioButton4.Text = radomTracNghiemViet(Form1.demTB)[ramdom[3]];
                    Form1.demTB++;
                }
            }
        }

        private void TracNghiem_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnTN.Enabled = true;
            AnTB.Checked = false;
            TatPA.Checked = true;
            DiemTracNghiem.CapNhat();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked && radioButton1.Text == lbDA.Text)
            {
                Sound.URL = string.Concat(Application.StartupPath, "\\True.mp3");
                Sound.controls.play();
                Form1.demTB++;
                radioButton1.Checked = true;
                DiemTracNghiem.ScoreDung++;

                return;
            }
            if (radioButton2.Checked && radioButton2.Text == lbDA.Text)
            {
                Sound.URL = string.Concat(Application.StartupPath, "\\True.mp3");
                Sound.controls.play();
                Form1.demTB++;
                radioButton1.Checked = true;
                DiemTracNghiem.ScoreDung++;

                return;
            }
            if (radioButton3.Checked && radioButton3.Text == lbDA.Text)
            {
                Sound.URL = string.Concat(Application.StartupPath, "\\True.mp3");
                Sound.controls.play();
                Form1.demTB++;
                radioButton1.Checked = true;
                DiemTracNghiem.ScoreDung++;

                return;
            }
            if (radioButton4.Checked && radioButton4.Text == lbDA.Text)
            {
                Sound.URL = string.Concat(Application.StartupPath, "\\True.mp3");
                Sound.controls.play();
                Form1.demTB++;
                radioButton1.Checked = true;
                DiemTracNghiem.ScoreDung++;
                return;
            }
            DiemTracNghiem.ScoreSai++;

            Sound.URL = string.Concat(Application.StartupPath, "\\Error.mp3");
            if(cbbSelect.SelectedIndex==0)
            VoiceOX.GirlSpeak(label1.Text);
            Sound.controls.play();
        }

        private void TracNghiem_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.D1)
            {
                radioButton1.Checked = true;
            }
            if (e.KeyCode == Keys.D2)
            {
                radioButton2.Checked = true;
            }
            if (e.KeyCode == Keys.D3)
            {
                radioButton3.Checked = true;
            }
            if (e.KeyCode == Keys.D4)
            {
                radioButton4.Checked = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void radioButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1)
            {
                radioButton1.Checked = true;
            }
            if (e.KeyCode == Keys.D2)
            {
                radioButton2.Checked = true;
            }
            if (e.KeyCode == Keys.D3)
            {
                radioButton3.Checked = true;
            }
            if (e.KeyCode == Keys.D4)
            {
                radioButton4.Checked = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void radioButton2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1)
            {
                radioButton1.Checked = true;
            }
            if (e.KeyCode == Keys.D2)
            {
                radioButton2.Checked = true;
            }
            if (e.KeyCode == Keys.D3)
            {
                radioButton3.Checked = true;
            }
            if (e.KeyCode == Keys.D4)
            {
                radioButton4.Checked = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void radioButton4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1)
            {
                radioButton1.Checked = true;
            }
            if (e.KeyCode == Keys.D2)
            {
                radioButton2.Checked = true;
            }
            if (e.KeyCode == Keys.D3)
            {
                radioButton3.Checked = true;
            }
            if (e.KeyCode == Keys.D4)
            {
                radioButton4.Checked = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void radioButton3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1)
            {
                radioButton1.Checked = true;
            }
            if (e.KeyCode == Keys.D2)
            {
                radioButton2.Checked = true;
            }
            if (e.KeyCode == Keys.D3)
            {
                radioButton3.Checked = true;
            }
            if (e.KeyCode == Keys.D4)
            {
                radioButton4.Checked = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void cbbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
