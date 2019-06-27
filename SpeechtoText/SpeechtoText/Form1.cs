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
using System.Data.SqlClient;

namespace SpeechtoText
{
      
    public partial class Form1 : Form
    {
      private  SpeechSynthesizer ss = new SpeechSynthesizer();
      private  PromptBuilder pb = new PromptBuilder();
      private  SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
      private  SpeechRecognitionEngine sre2 = new SpeechRecognitionEngine();
       private SpeechRecognitionEngine usernamespeechre = new SpeechRecognitionEngine();
        private String username;
        private String password;
        string Path = Environment.CurrentDirectory;
        String ConnectionString = "Data Source=" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Database.sdf";

        private SqlConnection sqlCon;

        Choices choices = new Choices(new string[] { "hello" });
        public Form1()
        {

            InitializeComponent();
            Choices newchoices = new Choices(new string[] { "No Username" });
            GrammarBuilder gb = new GrammarBuilder();

           sqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\vikra\Documents\Speech-to-Text\SpeechtoText\SpeechtoText\Database1.mdf;Integrated Security=True");
            ss.SpeakAsync(Environment.CurrentDirectory);
        Debug.Print("|DataDirectory|");
            Debug.Print(ConnectionString);
            Debug.Print(System.Environment.CurrentDirectory);
            Debug.Print(Path);
            gb.Append(newchoices);
            Grammar gbb = new Grammar(gb);
            try
            {
                usernamespeechre.RequestRecognizerUpdate();
                usernamespeechre.LoadGrammar(gbb);
                usernamespeechre.SpeechRecognized += sre_User_SpeechRecognized;
                usernamespeechre.SetInputToDefaultAudioDevice();
                usernamespeechre.RecognizeAsync(RecognizeMode.Multiple);




            }

            catch (Exception exe)
            {
                MessageBox.Show(exe.Message, "Error");
            }

            ss.SpeakAsync("Hello, enter your username please and terminate with an enter, if you do not have one, please say, no username");






        }


        private void sre_User_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch(e.Result.Text.ToString())
            {
                case "No Username":
                    ss.SpeakAsync("Please enter your new username into the white box");
                    Flag = 4;

                    break;
            }
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
                    ss.SpeakAsync("hello " + name);
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Feed_TextChanged(object sender, EventArgs e)
        {

        }

        int Flag = 0;
        int Flag2 = -1;
        String name;
        private void Feed2_KeyDown(object sender, KeyEventArgs e)
        {


           if(sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }


            if(e.KeyCode == Keys.Enter && (Flag == 0))
            {
                
                username = Feed2.Text.ToString();
                Feed2.Clear();
                                ss.SpeakAsync("Please enter your password and terminate with an Enter key");

                Flag = 1;
                Flag2 = 1;
                return;
            }
            

            if(Flag == 4 && e.KeyCode == Keys.Enter)
            {
               
                username = Feed2.Text.ToString();
                Feed2.Clear();
                ss.SpeakAsync("Please enter your name and terminate with an Enter key");
                Flag = 7;
                return;
            }

            if(Flag == 7 && e.KeyCode == Keys.Enter)
            {
                name = Feed2.Text.ToString();
                name = name.TrimStart('\r', '\n');
                Debug.Print(name.Length.ToString());
                Feed2.Clear();
                ss.SpeakAsync("Please enter your password and terminate with an Enter key");

                Flag = 1;
                return;
            }

            if (e.KeyCode == Keys.Enter && Flag == 1 && Flag2 == -1)
            {
                password = Feed2.Text.ToString();
                password = password.TrimStart('\r', '\n');
                Debug.Print(password + " tghis is the password");
                Feed2.Clear();
                usernamespeechre.RecognizeAsyncStop();
                Debug.Print("This has been reached");
                SqlCommand sqlcmd = new SqlCommand("CreateAccount", sqlCon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Name", name);
                sqlcmd.Parameters.AddWithValue("@Username", username);
                sqlcmd.Parameters.AddWithValue("@Password", password);
                sqlcmd.ExecuteNonQuery();
                ss.SpeakAsync("Greetings!" + name + " Thank you for setting up an account with us");
                ss.SpeakAsync("Please press the Start Button to activate the voice controlled assistant");

                return;

            }

            else if (e.KeyCode == Keys.Enter && Flag == 1 && Flag2 == 1)
            {
                password = Feed2.Text.ToString();
                password = password.TrimStart('\r', '\n');

                Feed2.Clear();
                usernamespeechre.RecognizeAsyncStop();
                Debug.Print("This has been reached2");
                Debug.Print(password + " tghis is the password");

                SqlCommand sqlcmd = new SqlCommand("VerifyAccount", sqlCon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                int OutputFlag = 0;
                String usernameee = " ";

                sqlcmd.Parameters.AddWithValue("@Username", username);
                sqlcmd.Parameters.AddWithValue("@Password", password);
                sqlcmd.Parameters.AddWithValue("@OutputFlag", OutputFlag).Direction=ParameterDirection.Output ;
                sqlcmd.Parameters.Add("@OutputName", SqlDbType.VarChar,50).Direction = ParameterDirection.Output;

                
                sqlcmd.ExecuteNonQuery();
                Debug.Print(usernameee);

                int returntype =  Convert.ToInt32(sqlcmd.Parameters["@OutputFlag"].Value);
                String usernameeeu = Convert.ToString((sqlcmd.Parameters["@OutputName"].Value));
                Debug.Print(usernameeeu);
                
                Debug.Print(returntype.ToString());
                if (returntype == -1)
                { ss.SpeakAsync("The password and username are invalid"); }
                else if (returntype == 1)
                {
                    ss.SpeakAsync("The password and username are valid");
                    ss.SpeakAsync("Welcome back!" + usernameeeu);
                    ss.SpeakAsync("Please press the Start Button to activate the voice controlled assistant");
                }



                return;
            }


            
           







        }
    }
}
