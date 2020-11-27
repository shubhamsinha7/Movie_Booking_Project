using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;


namespace Movie_Booking
{
    public partial class webcame : Form
    {

         string output = @"C:\Users\Dell\Desktop\images";
         public static string imp_path = null;
        
        VideoCaptureDevice freame;
        FilterInfoCollection device;
        public webcame()
        {
            InitializeComponent();
        }

        void start_cam()
        {
            device = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            freame = new VideoCaptureDevice(device[0].MonikerString);
            freame.NewFrame += new AForge.Video.NewFrameEventHandler(Newfreame_event);
            freame.Start();
        }
        void Newfreame_event(object send, NewFrameEventArgs e)
        {
            try
            {
                pictureBox1.Image = (Image)e.Frame.Clone();
            }
            catch
            {
                Exception ex;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text=="Camera")
            {
                start_cam();
                button1.Text = "Click";
            }
            if (button1.Text == "Click")
            {
                if (output != "" && pictureBox1.Image != null)
                {
                    pictureBox1.Image.Save(output+@"\\Image.jpg");
                }
               
            }
        }

        private void webcame_FormClosed(object sender, FormClosedEventArgs e)
        {
            freame.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            freame.Stop();
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "IMAGE FILE(*.jpg;*.jpeg)|*.jpg;*.jpeg";
            if (op.ShowDialog() == DialogResult.OK)
            {
                string imagelocation = op.FileName;
                pictureBox1.ImageLocation = imagelocation;
                imp_path = imagelocation;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            freame.Stop();
            this.Dispose();
            newuser ob_newuser = new newuser();
            ob_newuser.Show();

        }
    }
}
