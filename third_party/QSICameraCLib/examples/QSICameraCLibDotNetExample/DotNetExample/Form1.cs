using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DotNetExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ComboBoxCameras = new List<ComboBoxCamera>();
            temp = new Thread(UpdateTemperature);
            temp.Start();
            dll = new QSIDLLWrapper();
            handle = dll.CreateHandle();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            checkTemperature = false;
            appRunning = false;
            base.OnFormClosed(e);
            dll.ReleaseHandle(handle);
        }

        void UpdateTemperature()
        {
            while (appRunning)
            {
                while (checkTemperature)
                {
                    double tD = 0;
                    dll.GetCCDTemperature(handle, ref tD);
                    Temperature.BeginInvoke(new Action(() => Temperature.Text = tD.ToString()));
                    Thread.Sleep(1000);
                }
            }
        }

        private void Find_Click(object sender, EventArgs e)
        {
            ComboBoxCameras.Clear();
            string[] cameraList =  Enumerable.Repeat("               ", QSIPInvoke.QSI_LENGTH_SERIAL_NUMBER).ToArray();
            dll.AvailableCameras(handle, ref cameraList, QSIPInvoke.QSI_LENGTH_SERIAL_NUMBER);
            for (var i = 0; i < QSIPInvoke.QSI_LENGTH_SERIAL_NUMBER; ++i)
            {
                if (!string.IsNullOrWhiteSpace(cameraList[i]))
                {
                    ComboBoxCameras.Add(new ComboBoxCamera(cameraList[i]));
                }
            }

            CameraComboBox.Items.Clear();
            foreach (var camera in ComboBoxCameras)
                CameraComboBox.Items.Add(camera);
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            var selected = (ComboBoxCamera)CameraComboBox.SelectedItem;
            if (selected != null)
            {
                bool isOk = dll.SetSerialNumber(handle, selected.name, selected.name.Length);
                dll.Connect(handle);
                if (isOk)
                {
                    ConnectedLabel.Text = "Connected";
                    Disconnect.Enabled = true;
                    Connect.Enabled = false;
                    StartExposureButton.Enabled = true;

                    bool canSetTemp;
                    dll.GetCanSetCCDTemperature(handle, out canSetTemp);
                    if (canSetTemp)
                    {
                        StartCoolingButton.Enabled = true;
                        checkTemperature = true;
                    }
                    else
                    {
                        Temperature.Text = "No Cooling";
                    }
                }
            }
            else
            {
                MessageBox.Show("There is no selected camera");
            }
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            checkTemperature = false;
            // Stop cooling otherwise camera will continue 
            // to cool when disconnected
            if (StopCooling.Enabled)
            {
                dll.SetCoolerOn(handle, false);
                StopCooling.Enabled = false;
            }

            dll.StopExposure(handle);
            dll.Disconnect(handle);
            ConnectedLabel.Text = "Not Connected";
            Temperature.Text = "No Camera";

            Disconnect.Enabled = false;
            Connect.Enabled = true;
            StartCoolingButton.Enabled = false;
            StartExposureButton.Enabled = false;
        }

        private void StartExposureButton_Click(object sender, EventArgs e)
        {
            if (handle != 0)
            {
                if (dll.StartExposure(handle, (double)ExposureLength.Value, true))
                {
                    ExposureStatus.Text = "Starting Exposure";
                    Thread t1 = new Thread(WaitForImage);
                    t1.Start();

                    StopExposure.Enabled = true;
                    StartExposureButton.Enabled = false;
                }
                else
                {
                    ExposureStatus.Text = "Failed To Start Exposure";
                }
            }
        }

        private void WaitForImage()
        {
            SaveButton.BeginInvoke(new Action(() => SaveButton.Enabled = false));
            ExposureStatus.BeginInvoke(new Action(() => ExposureStatus.Text = "Waiting For Image"));
            bool imageReady = false;
            while (!imageReady)
            {
                dll.GetImageReady(handle, out imageReady);
                if (stopProcessing)
                {
                    stopProcessing = false;
                    return;
                }

                Thread.Sleep(100);
            }

            dll.GetImageNumX(handle, out int w);
            dll.GetImageNumY(handle, out int h);

            // Create memory to copy pixels into
            ushort[] pix = new ushort[w * h];

            if (dll.ReadImage(handle, ref pix, w * h))
            {
                ExposureStatus.BeginInvoke(new Action(() => ExposureStatus.Text = "Converting"));

                image = new Bitmap(w, h, PixelFormat.Format32bppArgb); // GDI+ does not work with 16 bit grayscale pixels, so in this example we use 32 bits per pixel and 8 bits per colour channel
                var rect = new Rectangle(0, 0, w, h);
                var picData = image.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                // create enough space for a 32 bit per pixel bitmap of the image
                byte[] bmpBytes = new byte[w * h * 4];
                for (int i = 0, j = 0; j < w * h; ++i, ++j)
                {
                    if (stopProcessing)
                    {
                        stopProcessing = false;
                        return;
                    }

                    var val = (byte)(pix[j] >> 8);
                    // we have a mono image so we place the same value into each bitmap byte
                    bmpBytes[i] = val;
                    bmpBytes[++i] = val;
                    bmpBytes[++i] = val;
                    bmpBytes[++i] = 255;
                }

                Marshal.Copy(bmpBytes, 0, picData.Scan0, w * h * 4);
                image.UnlockBits(picData);

                PictureBox.BeginInvoke(new Action(() => PictureBox.Image = image));
                ExposureStatus.BeginInvoke(new Action(() => ExposureStatus.Text = "Idle"));

                SaveButton.BeginInvoke(new Action(() => SaveButton.Enabled = true));

                StartExposureButton.BeginInvoke(new Action(() => StartExposureButton.Enabled = true));
                StopExposure.BeginInvoke(new Action(() => StopExposure.Enabled = false));
            }
            else
            {
                MessageBox.Show("Failed to read the image from the camera there may be a fault.\n Please check the LED on the camera\n Please try power cycling the camera and restarting the example program");
            }
        }

        private void StopExposure_Click(object sender, EventArgs e)
        {
            dll.StopExposure(handle);
            stopProcessing = true;

            StopExposure.Enabled = false;
            StartExposureButton.Enabled = true;
        }

        private void StartCoolingButton_Click(object sender, EventArgs e)
        {
            if (handle != 0)
            {
                dll.SetCCDTemperatureSetpoint(handle, (double)TargetTemp.Value);
                dll.SetCoolerOn(handle, true);

                StopCooling.Enabled = true;
                StartCoolingButton.Enabled = false;
            }
        }

        private void StopCooling_Click(object sender, EventArgs e)
        {
            dll.SetCoolerOn(handle, false);

            StopCooling.Enabled = false;
            StartCoolingButton.Enabled = true;
        }

        private void CameraComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connect.Enabled = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if(fbd.ShowDialog() ==DialogResult.OK)
                {
                    image.Save(Path.Combine(fbd.SelectedPath, "QSI_Image.tiff"), ImageFormat.Tiff);
                }
            }
        }

        QSIDLLWrapper dll;
        uint handle;

        List<ComboBoxCamera> ComboBoxCameras;

        bool stopProcessing;
        bool checkTemperature;
        bool appRunning = true;
        Thread temp;

        Bitmap image;
    }

    public class ComboBoxCamera
    {
        public ComboBoxCamera(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }

        public string name;
    }
}
