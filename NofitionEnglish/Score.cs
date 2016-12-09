using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace NofitionEnglish
{
    class Score
    {
        int iSai;
        int iDung;
        StreamReader readScore;
        Label diem;
        public Score (ref Label diem)
        {
            this.diem = diem;
            readScore = new StreamReader(string.Concat(Application.StartupPath, "\\Score.txt"));
            iDung = int.Parse(readScore.ReadLine());
            iSai = int.Parse(readScore.ReadLine());
            readScore.Close();
        }
        public int ScoreSai
        {
            get { return iSai; }
            set 
            {
              
                iSai = value;
                diem.Text = "Score True/False : " + ScoreDung.ToString() + "/" + ScoreSai.ToString();
            }
        }
     

        public  int ScoreDung
        {
            get { return iDung; }
            set { iDung = value; diem.Text = "Score True/False : " + ScoreDung.ToString() + "/" + ScoreSai.ToString(); }
        }
        public void  CapNhat()
        {
            StreamWriter writeScore = new StreamWriter(string.Concat(Application.StartupPath, "\\Score.txt"));

            writeScore.WriteLine(iDung.ToString());
            writeScore.WriteLine(iSai.ToString());
            writeScore.Close();
       
        }
        
    }
}
