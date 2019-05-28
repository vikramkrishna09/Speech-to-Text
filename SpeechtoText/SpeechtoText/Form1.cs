using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.Diagnostics;
using AudioSwitcher.AudioApi.CoreAudio;
namespace SpeechtoText
{
      
    public partial class Form1 : Form
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        SpeechRecognitionEngine sre2 = new SpeechRecognitionEngine();
        Choices choices = new Choices(new string[] { "hello" });
        public Form1()
        {
            InitializeComponent();
        }

        private void Startbutton(object sender, EventArgs e)
        {
            Start.Enabled = false;
            Stop.Enabled = true;
            choices.Add(new string[] { "how are you", "open Firefox",
                "close",});


            Choices secondchoices = new Choices();
            for(int i = 0; i < 101; i++)
            {
                secondchoices.Add(i.ToString());
            }
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append("Set Sound To");
            gb.Append(secondchoices);
            GrammarBuilder gbb = new GrammarBuilder();
            gbb.Append("Open");
            Choices thirdchoices = new Choices();
            thirdchoices.Add("Firefox");
            thirdchoices.Add("Twitter");
            thirdchoices.Add("Reddit");
            thirdchoices.Add("Amazon");
            gbb.Append(thirdchoices);
            Grammar gr = new Grammar(new GrammarBuilder(choices));
            Grammar gr2 = new Grammar(gb);
            Grammar gr3 = new Grammar(gbb);





            Choices numberchoices = new Choices();
            for (int i = 0; i < 500; i++)
            {
                numberchoices.Add(i.ToString());
            }

            GrammarBuilder gbbb = new GrammarBuilder();
            gbbb.Append("What's");
            gbbb.Append(numberchoices);
            gbbb.Append("Plus");
            gbbb.Append(numberchoices);

            GrammarBuilder gbbb_1 = new GrammarBuilder();
            gbbb_1.Append("What's");
            gbbb_1.Append(numberchoices);
            gbbb_1.Append("Minus");
            gbbb_1.Append(numberchoices);

            GrammarBuilder gbbb_2 = new GrammarBuilder();
            gbbb_2.Append("What's");
            gbbb_2.Append(numberchoices);
            gbbb_2.Append("Times");
            gbbb_2.Append(numberchoices);

            GrammarBuilder gbbb_3 = new GrammarBuilder();
            gbbb_3.Append("What's");
            gbbb_3.Append(numberchoices);
            gbbb_3.Append("Divided");
            gbbb_3.Append("By");
            gbbb_3.Append(numberchoices);




            Grammar gr4 = new Grammar(gbbb);
            Grammar gr4_1 = new Grammar(gbbb_1);
            Grammar gr4_2 = new Grammar(gbbb_2);
            Grammar gr4_3 = new Grammar(gbbb_3);
         
            try
            { 

                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.LoadGrammar(gr2);
                sre.LoadGrammar(gr3);
                sre.LoadGrammar(gr4);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);




                sre2.RequestRecognizerUpdate();
                sre2.LoadGrammar(gr4_1);
                sre2.LoadGrammar(gr4_2);
                sre2.LoadGrammar(gr4_3);
                sre2.SpeechRecognized += sre_SpeechRecognized;
                sre2.SetInputToDefaultAudioDevice();
                sre2.RecognizeAsync(RecognizeMode.Multiple);





            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message, "Error");
            }
        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch(e.Result.Text.ToString())
            {
                case "hello":
                    ss.SpeakAsync("whats good");
                     break;
                case "close":
                    Application.Exit();
                    break;
                
            }


            Feed.Text += e.Result.Text.ToString() + Environment.NewLine;


            string text = e.Result.Text.ToString();
            if(text.IndexOf("Set Sound To") >= 0)
                {
                string[] words = text.Split(' ');
                Debug.Print(text);
                int raise = int.Parse(words[3]);
                CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
                defaultPlaybackDevice.Volume = raise;
            }

            if(text.IndexOf("Open") >= 0)
            {
                string[] words = text.Split(' ');
                String word = words[1];
                String url = "http://www." + word + ".com";
                System.Diagnostics.Process.Start(url);
            }

            if(text.IndexOf("What") >= 0)
            {
                if(text.IndexOf("Plus")>= 0)
                {
                    string[] words = text.Split(' ');
                    double operand1 = Convert.ToDouble(words[1]);
                    double operand2 = Convert.ToDouble(words[3]);
                    double sum = operand1 + operand2;
                    ss.SpeakAsync(sum.ToString());
                }

                else if (text.IndexOf("Times") >= 0)
                {
                    string[] words = text.Split(' ');
                    double operand1 = Convert.ToDouble(words[1]);
                    double operand2 = Convert.ToDouble(words[3]);
                    double sum = operand1 * operand2;
                    ss.SpeakAsync(sum.ToString());
                }
            }




        }

        private void Endbutton(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            sre2.RecognizeAsyncStop();
            Start.Enabled = true;
            Stop.Enabled = false;
        }
    }
}
