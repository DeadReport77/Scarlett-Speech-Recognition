using System;
using System.Diagnostics;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace Scarlett
{
    public partial class Scanning : Form
    {
        public Scanning()
        {
            InitializeComponent();
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\scanning.wav");
            simpleSound.Play();
            Thread.Sleep(3000);
        }


        private void Scanning_Load(object sender, EventArgs e)
        {
            new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\searching.wav").Play();
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 1000;
            progressBar1.Maximum = 10;
            timer1.Tick += new EventHandler(timer1_Tick);
            Process.Start(@"C:\Users\Leeraoy.Jenkins\Desktop\Batch Projects\Cleanup");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != 10)
            {
                progressBar1.Value++;
            }
            else
            {
                timer1.Stop();
                new SoundPlayer(@"C:\Users\Leeraoy.Jenkins\source\repos\Scarlett\Resources\scancomplete.wav").Play();
                Thread.Sleep(3000);
                this.Close();
                Form1 f = new Form1();
                f.Show();

            }

        }
    }

}
