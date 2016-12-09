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
using System.Timers;
namespace NofitionEnglish
{
    class VoiceOX
    {
        System.Timers.Timer tmr = new System.Timers.Timer();
        string[] tempInput;
        WindowsMediaPlayer SpeakOxFord;
        WindowsMediaPlayer SpeakOxFord2;
        SpeechSynthesizer readerEX;
        int DemTU=1;
        int SoTu = 0;
        public VoiceOX()
        {
            SpeakOxFord = new WindowsMediaPlayer();
            SpeakOxFord2 = new WindowsMediaPlayer();
            SpeakOxFord.PlayStateChange += new  _WMPOCXEvents_PlayStateChangeEventHandler(wplayer_PlayStateChange);
            
            readerEX = new SpeechSynthesizer();
            tmr.Elapsed += new ElapsedEventHandler(tmr_Tick);
            tmr.Interval = 10;
            readerEX.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(reader_SpeakCompleted);
        }
        private void reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            if (SoTu != 1 && DemTU <= SoTu &&  !Form1.checkBox4.Checked)
            {

                tmr.Start();
            }
            if (DemTU == SoTu && DemTU != 1 || (DemTU == SoTu && SoTu == 1 ))
            {
                if (!Form1.checkBox5.Checked)
                    return;
                if (SoTu != 1)
                    Form1.aTimer.Interval = 500;
                else
                    Form1.aTimer.Interval = 5000;
                
            }
            if(Form1.checkBox4.Checked ||!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                if (!Form1.checkBox5.Checked)
                    return;
                Form1.aTimer.Interval = 5000;
            }
        }
        void tmr_Tick(object sender, ElapsedEventArgs e)
        {
            try
            {
                tmr.Stop();
            }
            catch
            { 

            }
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() )
                try
                {

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.oxfordlearnersdictionaries.com/definition/english/" + tempInput[DemTU].Trim().ToLower());
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string hold = read.ReadToEnd();
                    hold = hold.Substring(hold.IndexOf(".mp3") + 5);

                    hold = hold.Substring(hold.IndexOf("mp3=") + 5, hold.IndexOf(".mp3") + 4 - (hold.IndexOf("mp3=") + 5));
                    SpeakOxFord.URL = hold;

                    // SpeakOxFord.controls.stop();
                    SpeakOxFord.controls.play();


                }
                catch
                {
                    try
                    {
                        if (tempInput[DemTU] != "")
                        {
                            readerEX.SpeakAsyncCancelAll();
                            readerEX.SpeakAsync(tempInput[DemTU]);
                          
                        }
                    }
                    catch
                    {

                    }
                }
            else
                try
                {
                    if (tempInput[DemTU] != "")
                    {
                        readerEX.SpeakAsyncCancelAll();
                        readerEX.Speak(tempInput[DemTU]);
                    }
                }
                catch
                {

                }
            DemTU++;


        }
        void wplayer_PlayStateChange(int NewState)
        {
            if (NewState == (int)WMPLib.WMPPlayState.wmppsMediaEnded && SoTu!=1 && DemTU<=SoTu )
            {

                tmr.Start();
                
            }
            if ((DemTU == SoTu && DemTU != 1 && NewState == (int)WMPLib.WMPPlayState.wmppsMediaEnded) || (DemTU == SoTu && SoTu == 1 && NewState == (int)WMPLib.WMPPlayState.wmppsMediaEnded))
            {
                if (!Form1.checkBox5.Checked)
                    return;
                if (SoTu != 1)

                    Form1.aTimer.Interval = 500;
                else
                    Form1.aTimer.Interval = 5000;
            }
          
           
            
        }

        public void StopSpeak()
        {
           
        }
        public void GirlSpeak(string inPut)
        {
            SoTu = 1;
            DemTU = 1;
            inPut = inPut.Split('|')[0];
            inPut = inPut.ToLower().TrimEnd();
            SoTu=inPut.Split(',').Length;
              tempInput=new string[SoTu];
            try
            {
                tempInput = inPut.Split(',');
            }
            catch
            {
                tempInput[1] = inPut;
            }
           // for (int i = 0; i < SoTu; i++)
            {


                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() && !Form1.checkBox4.Checked)
                {
                    inPut = tempInput[0].Trim();
                    try
                    {

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.oxfordlearnersdictionaries.com/definition/english/" + inPut);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                        string hold = read.ReadToEnd();
                        hold = hold.Substring(hold.IndexOf(".mp3") + 5);

                        hold = hold.Substring(hold.IndexOf("mp3=") + 5, hold.IndexOf(".mp3") + 4 - (hold.IndexOf("mp3=") + 5));
                        SpeakOxFord.URL = hold;
                        double hello = SpeakOxFord.controls.currentPosition;
                        // SpeakOxFord.controls.stop();
                        SpeakOxFord.controls.play();


                    }
                    catch
                    {
                        try
                        {
                            if (inPut != "")
                            {
                                readerEX.SpeakAsyncCancelAll();
                                readerEX.SpeakAsync(inPut);
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                else

                    try
                    {
                        if (inPut != "")
                        {
                            readerEX.SpeakAsyncCancelAll();
                            readerEX.SpeakAsync(inPut);
                          
                        }
                    }
                    catch
                    {

                    }
            

            
            
        }

        }
        public void BoySpeak(string inPut)
        {
            inPut = inPut.Trim();
            inPut = inPut.ToLower();

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.oxfordlearnersdictionaries.com/definition/english/" + inPut);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string hold = read.ReadToEnd();
                hold = hold.Substring(hold.IndexOf("mp3=") + 5, hold.IndexOf(".mp3") + 4 - (hold.IndexOf("mp3=") + 5));
                SpeakOxFord.URL = hold;
                SpeakOxFord.controls.play();
            }
            catch
            {
                try
                {
                    SpeakOxFord.URL = "http://translate.google.com/translate_tts?tl=en&q=" + inPut;
                    SpeakOxFord.controls.play();
                }
                catch
                {
                    try
                    {
                        if (inPut != "")
                            readerEX.Speak(inPut);
                    }
                    catch
                    {

                    }
                }
            }

        }
    }
}

