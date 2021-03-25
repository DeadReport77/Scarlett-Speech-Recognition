///Created by Justin Linwood Ross (United States-Maine)///
///Scarlett Centuri Model AI-7 (© Copyright 2021)///
///Black Star Research Facility////
using System;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;

namespace Scarlett
{


    public partial class Form1 : Form
    {
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);
        PerformanceCounter perfCPUCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        PerformanceCounter perfMemCounter = new PerformanceCounter("Memory", "Available MBytes");
        PerformanceCounter perfSystemCounter = new PerformanceCounter("System", "System Up Time");
        SpeechSynthesizer Scarlett = new SpeechSynthesizer();
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();

        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Ask (TELL ME WHO ARE YOU) and I will tell you how to operate me.");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblperfCPUCounter.Text = "CPU:" + "  " + (int)perfCPUCounter.NextValue() + "  " + "%";
            label2.Text = "Available Memory:" + "  " + (int)perfMemCounter.NextValue() + "  " + "MB";
            label3.Text = "System Up Time" + "  " + (int)perfSystemCounter.NextValue() / 60 / 60 + "Hours";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "scarlett what time is it", "scarlett mute volume", "scarlett volume up", "scarlett volume down", "stop listening", "open wiki", "show commands", "tell me who are you", "scarlett close", "scarlett what day is it", "scarlett out of the way", "scarlett come back", "disparta", "scarlett sign in",
            "page up", "page down", "new tab", "switch tab", "magnify", "less", "scarlett notepad", "scarlett visual studios", "open command", "scarlett play rammstein", "scarlett youtube",
            "scarlett movies", "scarlett open github", "scarlett open word", "scarlett run system scan", "commit to memory" });
            GrammarBuilder gramBuilder = new GrammarBuilder();
            gramBuilder.Append(commands);
            Grammar grammar = new Grammar(gramBuilder);
            Grammar gram = grammar;
            recEngine.LoadGrammarAsync(gram);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.InitialSilenceTimeout = TimeSpan.FromSeconds(3);
            recEngine.BabbleTimeout = TimeSpan.FromSeconds(2);
            recEngine.EndSilenceTimeout = TimeSpan.FromSeconds(1);
            recEngine.EndSilenceTimeoutAmbiguous = TimeSpan.FromSeconds(1.5);
            Choices c = new Choices();
            c.Add("one");
            c.Add("two");
            c.Add("three");
            var gb = new GrammarBuilder(c);
            var g = new Grammar(gb);
            recEngine.LoadGrammar(g);
            Console.WriteLine("BabbleTimeout: {0}", recEngine.BabbleTimeout);
            Console.WriteLine("InitialSilenceTimeout: {0}", recEngine.InitialSilenceTimeout);
            Console.WriteLine("EndSilenceTimeout: {0}", recEngine.EndSilenceTimeout);
            Console.WriteLine("EndSilenceTimeoutAmbiguous: {0}", recEngine.EndSilenceTimeoutAmbiguous);
            Console.WriteLine();
            recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            Scarlett.SelectVoiceByHints(VoiceGender.Female);
        }


        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "scarlett what time is it":
                    Scarlett.Speak(DateTime.Now.ToShortTimeString());
                    break;
                case "scarlett mute volume":
                    SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
                (IntPtr)APPCOMMAND_VOLUME_MUTE);
                    break;
                case "scarlett volume up":
                    int repeat = 10;
                    for (int i = 0; i < repeat; i++)
                        SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
                    (IntPtr)APPCOMMAND_VOLUME_UP);
                    break;
                case "scarlett volume down":
                    for (int iter = 0; iter < 10; iter++)
                        SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
               (IntPtr)APPCOMMAND_VOLUME_DOWN);
                    break;
                case "show commands":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\list.wav").Play();
                    Process.Start(@"C:\\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\Commands.txt");
                    break;
                case "tell me who are you":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\info.wav").Play();
                    break;
                case "scarlett youtube":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\youtube.wav").Play();
                    Process.Start("chrome", "Http://www.youtube.com");
                    break;
                case "scarlett movies":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\movie.wav").Play();
                    Process.Start("chrome", "http://www.tubitv.com");
                    break;
                case "scarlett open github":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\github.wav").Play();
                    Process.Start("chrome", "Http://www.github.com");
                    break;
                case "open wiki":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\wiki.wav").Play();
                    Process.Start("http://www.wikipedia.org/");
                    break;
                case "scarlett open word":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\word.wav").Play();
                    Process.Start("winword");
                    break;
                case "stop listening":
                    recEngine.RecognizeAsyncStop();
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\mic.wav").Play();
                    break;
                case "open command":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\prompt.wav").Play();
                    System.Diagnostics.Process.Start("cmd");
                    break;
                case "scarlett run system scan":
                    Scanning f = new Scanning();
                    f.Show();
                    break;
                case "commit to memory":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\storing.wav").Play();
                    break;
                case "scarlett close":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\shuttingdown.wav").Play();
                    Sleep(TimeSpan.FromSeconds(4));
                    Close();
                    break;
                case "scarlett what day is it":
                    Scarlett.SpeakAsync(DateTime.Now.ToShortDateString());
                    break;
                case "scarlett out of the way":
                    if (WindowState == FormWindowState.Normal)
                    {
                        WindowState = FormWindowState.Minimized;
                        new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\standby.wav").Play();
                    }
                    break;
                case "scarlett come back":
                    if (WindowState == FormWindowState.Minimized)
                    {
                        WindowState = FormWindowState.Normal;
                        new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\comeback.wav").Play();
                    }
                    break;
                case "disparta":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\closingapp.wav").Play();
                    SendKeys.Send("%{F4}");
                    break;
                case "scarlett play rammstein":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\video.wav").Play();
                    Process.Start("chrome", "https://www.youtube.com/watch?v=3b4szXcRFcs?autoplay=1&mute=1");
                    break;
                case "scarlett sign in":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\wish.wav").Play();
                    Process.Start("chrome", "https://www.dropbox.com");
                    SendKeys.Send("Blackstarproject@mail.com{TAB}");///Email goes here///
                    SendKeys.Send("123456{ENTER}");///Enter paasword here///I left example>>123456///
                    break;
                case "page up":
                    SendKeys.Send("{PGUP}");
                    break;
                case "page down":
                    SendKeys.Send("{PGDN}");
                    break;
                case "new tab":
                    SendKeys.Send("^{t}");
                    break;
                case "switch tab":
                    SendKeys.Send("^{TAB}");
                    break;
                case "magnify":
                    SendKeys.Send("^{+}");
                    break;
                case "less":
                    SendKeys.Send("^{-}");
                    break;
                case "scarlett notepad":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\notepad.wav").Play();
                    Process.Start("notepad++");
                    break;
                case "scarlett visual studios":
                    new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\visual.wav").Play();
                    Process.Start("devenv.exe");
                    break;
            }
        }

        private void Sleep(TimeSpan timeSpan)
        {
            Thread.Sleep(4000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            button1.Enabled = true;
            Thread.Sleep(5000);
        }
    }
}