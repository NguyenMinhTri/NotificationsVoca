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
    public partial class AddVoca : Form
    {
        getDataSOHA meanVASOHA;
        string NganhGi = "Thông Dụng";
        public static string m_Mean = "";
        public static string m_Voca = "";
        Button btnTemp;
        public AddVoca(ref Button Btntemp)
        {
            Data = new DuLieuLichSu();
            btnTemp = Btntemp;
            btnTemp.Enabled = false;
            InitializeComponent();
            meanVASOHA = new getDataSOHA();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            this.textBox1.Focus();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                NganhGi = radioButton1.Text;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                NganhGi = radioButton2.Text;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                NganhGi = radioButton3.Text;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Document.ExecCommand("SelectAll", false, null);
            string tempCLIPBOARD = Clipboard.GetText();
            webBrowser1.Document.ExecCommand("Copy", false, null);
            webBrowser1.Document.ExecCommand("PASTE", false, null);
            m_Mean = Clipboard.GetText();
            Clipboard.SetText(tempCLIPBOARD);
            m_Voca = textBox1.Text;
            textBox2.Text = m_Mean;
            tempCLIPBOARD = "";
        }

        private void AddVoca_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnTemp.Enabled = true ;
        }
        DuLieuLichSu Data;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.Focus();
            if (m_Voca == ""  || m_Mean == "No data or error")
                return;
            // Lọc ra dòng cuối cùng để nạp vào lịch sử tra từ
           string adHisMean = m_Mean;
           int checkMean = m_Mean.IndexOf(m_Voca.ToLower().Trim());
           if (NganhGi != "Thông Dụng" || checkMean==0)
            {
                int isBING = m_Mean.IndexOf("\r\n");
                if (isBING != -1)
                {

                    string istempCut = adHisMean;
                    adHisMean = (m_Mean.Substring(isBING + 2));

                    if (adHisMean == "")
                        adHisMean = istempCut;
                    else
                    {
                        try
                        {
                            istempCut = adHisMean;
                            if (adHisMean.IndexOf("\r\n") != -1)
                            {
                                adHisMean = adHisMean.Substring(adHisMean.IndexOf("\r\n") + 2);
                                if (adHisMean == "")
                                    adHisMean = istempCut;
                                if (adHisMean.IndexOf("\r\n") != -1)
                                    adHisMean = adHisMean.Substring(0, adHisMean.IndexOf("\r\n") + 2);
                            }
                            else
                                adHisMean = istempCut;


                        }
                        catch
                        {
                            adHisMean = istempCut;
                        }
                    }
                }
                adHisMean=adHisMean.Replace("\r\n","");
                Data.Write(ref Form1.dataGridView1, m_Voca, adHisMean.Replace("\t", "").Replace("\r\n", "").Replace("\n", ""));
                textBox1.Text = "";
                this.textBox1.Focus();
            }
            else if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() &&  NganhGi=="Thông Dụng")
            {
                
                m_Mean = m_Mean.Replace("Danh từ\r\n", "[n] ");
                m_Mean = m_Mean.Replace("Trạng từ\r\n", "[adv] ");
                m_Mean = m_Mean.Replace("Tính từ\r\n", "[adj] ");
                m_Mean = m_Mean.Replace("Động từ\r\n", "[v] ");
                m_Mean = m_Mean.Replace("\r\n", " ");
                Data.Write(ref Form1.dataGridView1, m_Voca, m_Mean.Replace('\t', ' '));
                textBox1.Text = "";
                this.textBox1.Focus();
            }
            else
            {
                int isBING = m_Mean.IndexOf("\r\n");
                if (isBING != -1)
                {

                    string istempCut = adHisMean;
                    adHisMean = (m_Mean.Substring(isBING + 2));

                    if (adHisMean == "")
                        adHisMean = istempCut;
                    else
                    {
                        try
                        {
                            istempCut = adHisMean;
                            if (adHisMean.IndexOf("\r\n") != -1)
                            {
                                adHisMean = adHisMean.Substring(adHisMean.IndexOf("\r\n") + 2);
                                if (adHisMean == "")
                                    adHisMean = istempCut;
                                if (adHisMean.IndexOf("\r\n") != -1)
                                    adHisMean = adHisMean.Substring(0, adHisMean.IndexOf("\r\n") + 2);
                            }
                            else
                                adHisMean = istempCut;


                        }
                        catch
                        {
                            adHisMean = istempCut;
                        }
                    }
                }
                adHisMean = adHisMean.Replace("\r\n", "");
                Data.Write(ref Form1.dataGridView1, m_Voca.Replace("\t",""), adHisMean.Replace("\t", ""));
                textBox1.Text = "";
                this.textBox1.Focus();
            }
            this.textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string DocumentText = meanVASOHA.meanAV(NganhGi, false, textBox1.Text);
                this.textBox1.Focus();
                {
                    string cccc = "<p><font color=" + '"' + "red" + '"';
                    DocumentText = DocumentText.Replace("<h3>", "<h4>");
                    DocumentText = DocumentText.Replace("<h5>", "<h3>" + cccc);
                }
                try
                {
                    webBrowser1.DocumentText = DocumentText;
                    this.textBox1.Focus();
                }
                catch
                {
                    webBrowser1.DocumentText = "No data or error";
                    this.textBox1.Focus();
                }
                this.textBox1.Focus();
            }
        }
    }
}
