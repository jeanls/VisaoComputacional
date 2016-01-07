using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InicioCV
{
    public partial class Form1 : Form
    {
        private Capture _capture = null;
        private bool _captureInProgress;
        private Mat frame;
        public Form1()
        {
            InitializeComponent();
            CvInvoke.UseOpenCL = false;
            try
            {
                _capture = new Capture();
                _capture.ImageGrabbed += ProcessFrame;
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }
        private void ProcessFrame(object sender, EventArgs arg)
        {
            frame = new Mat();
            _capture.Retrieve(frame, 0);
            captureImageBox.Image = frame;
        }

        private void takePicture()
        {
            Mat print = _capture.QueryFrame();
            Bitmap img = print.Bitmap;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Png Image (.png)|*.png";
            DateTime date = DateTime.Now;
            string fileName = date.Day + "-" + date.Month + "-" + date.Year + " " + date.Hour + "-" + date.Minute + "-" + date.Second;
            dialog.FileName = fileName;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                img.Save(dialog.FileName, ImageFormat.Png);
            }
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                if (_captureInProgress)
                {  //stop the capture
                    captureButton.Text = "Start Capture";
                    _capture.Pause();
                }
                else
                {
                    //start the capture
                    captureButton.Text = "Stop";
                    _capture.Start();
                }

                _captureInProgress = !_captureInProgress;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            takePicture();
        }
    }
}
