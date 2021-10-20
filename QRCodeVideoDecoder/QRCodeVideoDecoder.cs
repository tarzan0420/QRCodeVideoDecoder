using QRCodeDecoderLibrary;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using DirectShowLib;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing;
using System.Text;
using QRCodeEncoderLibrary;

namespace QRCodeVideoDecoder
{
    public partial class QRCodeVideoDecoder : Form
    {
        private FrameSize FrameSize = new FrameSize(640, 480);
        private Camera VideoCamera;
        private Timer QRCodeTimer;
        private QRDecoder Decoder;
        private QREncoder QRCodeEncoder;

        public QRCodeVideoDecoder()
        {
            InitializeComponent();
        }

        protected void OnLoad(object sender, EventArgs e)
        {
            // program title
            Text = "QRCodeVideoDecoder - " + QRDecoder.VersionNumber + " \u00a9 2013-2018 Uzi Granot. All rights reserved.";

#if DEBUG
            // current directory
            string CurDir = Environment.CurrentDirectory;
            //string WorkDir = CurDir.Replace("bin\\Debug", "Work");
            //if (WorkDir != CurDir && Directory.Exists(WorkDir)) Environment.CurrentDirectory = WorkDir;

            // open trace file
            QRCodeTrace.Open("QRCodeVideoDecoderTrace.txt");
            QRCodeTrace.Write(Text);
#endif

            // create encoder object
            QRCodeEncoder = new QREncoder();

            // disable reset button
            DecodeButton.Enabled = false;

            // get an array of web camera devices
            DsDevice[] CameraDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            // make sure at least one is available
            if (CameraDevices == null || CameraDevices.Length == 0)
            {
                MessageBox.Show("No video cameras in this computer");
                Close();
                return;
            }

            // select the first camera
            DsDevice CameraDevice = CameraDevices[0];

            // Device moniker
            IMoniker CameraMoniker = CameraDevice.Moniker;

            // get a list of frame sizes available
            FrameSize[] FrameSizes = Camera.GetFrameSizeList(CameraMoniker);

            // make sure there is at least one frame size
            if (FrameSizes == null || FrameSizes.Length == 0)
            {
                MessageBox.Show("No video cameras in this computer");
                Close();
                return;
            }

            // test if our frame size is available
            int Index;
            for (Index = 0; Index < FrameSizes.Length &&
                (FrameSizes[Index].Width != FrameSize.Width || FrameSizes[Index].Height != FrameSize.Height); Index++) ;

            // select first frame size
            if (Index == FrameSizes.Length) FrameSize = FrameSizes[0];

            // Set selected camera to camera control with default frame size
            // Create camera object
            VideoCamera = new Camera(PreviewPanel, CameraMoniker, FrameSize);

            // create QR code decoder
            Decoder = new QRDecoder();

            // resize window
            OnResize(e);

            // create timer
            QRCodeTimer = new Timer();
            QRCodeTimer.Interval = 200;
            QRCodeTimer.Tick += QRCodeTimer_Tick;
            QRCodeTimer.Enabled = true;
        }

        private void QRCodeTimer_Tick(object sender, EventArgs e)
        {
            QRCodeTimer.Enabled = false;
            Bitmap QRCodeImage;
            try
            {
                QRCodeImage = VideoCamera.SnapshotSourceImage();

                // trace
                #if DEBUG
                QRCodeTrace.Format("Image width: {0}, Height: {1}", QRCodeImage.Width, QRCodeImage.Height);
                #endif
            }

            catch (Exception EX)
            {
                DataTextBox.Text = "Decode exception.\r\n" + EX.Message;
                QRCodeTimer.Enabled = true;
                return;
            }

            // decode image
            byte[][] DataByteArray = Decoder.ImageDecoder(QRCodeImage);
            string Text = QRCodeResult(DataByteArray);

            // dispose bitmap
            QRCodeImage.Dispose();

            // we have no QR code
            if (Text.Length == 0)
            {
                QRCodeTimer.Enabled = true;
                return;
            }
           
            VideoCamera.PauseGraph();

            DataTextBox.Text = Text;
            DecodeButton.Enabled = true;

            return;
        }

        private static string QRCodeResult(byte[][] DataByteArray)
        {
            // no QR code
            if (DataByteArray == null) return string.Empty;

            // image has one QR code
            if (DataByteArray.Length == 1) return QRDecoder.ByteArrayToStr(DataByteArray[0]);

            // image has more than one QR code
            StringBuilder Str = new StringBuilder();
            for (int Index = 0; Index < DataByteArray.Length; Index++)
            {
                if (Index != 0) Str.Append("\r\n");
                Str.AppendFormat("QR Code {0}\r\n", Index + 1);
                Str.Append(QRDecoder.ByteArrayToStr(DataByteArray[Index]));
            }
            return Str.ToString();
        }

        protected void OnClosing(object sender, CancelEventArgs e)
        {
            if (VideoCamera != null) VideoCamera.Dispose();
            return;
        }

        protected void OnResize(object sender, EventArgs e)
        {
            // minimize
            if (ClientSize.Width == 0) return;

            // put reset button at bottom center
            DecodeButton.Left = ClientSize.Width / 2 - DecodeButton.Width - 8;
            DecodeButton.Top = ClientSize.Height - DecodeButton.Height - 8;
            EncodeButton.Left = DecodeButton.Right + 16;
            EncodeButton.Top = DecodeButton.Top;

            // data text box
            DataTextBox.Left = 8;
            DataTextBox.Top = DecodeButton.Top - DataTextBox.Height - 8;
            DataTextBox.Width = ClientSize.Width - 8 - SystemInformation.VerticalScrollBarWidth;

            // decoded data label
            DecodedDataLabel.Left = 8;
            DecodedDataLabel.Top = DataTextBox.Top - DecodedDataLabel.Height - 4;

            //preview area
            int AreaWidth = ClientSize.Width - 4;
            int AreaHeight = DecodedDataLabel.Top - 4;
            if (AreaHeight > FrameSize.Height * AreaWidth / FrameSize.Width)
                AreaHeight = FrameSize.Height * AreaWidth / FrameSize.Width;
            else
                AreaWidth = FrameSize.Width * AreaHeight / FrameSize.Height;

            // preview panel
            PreviewPanel.Left = (ClientSize.Width - AreaWidth) / 2;
            PreviewPanel.Top = (DecodedDataLabel.Top - 4 - AreaHeight) / 2;
            PreviewPanel.Width = AreaWidth;
            PreviewPanel.Height = AreaHeight;
        }

        private void OnDecodeButtonClick(object sender, EventArgs e)
        {
            VideoCamera.RunGraph();
            QRCodeTimer.Enabled = true;
            DecodeButton.Enabled = false;
            DataTextBox.Text = string.Empty;
        }

        private void OnEncodeButtonClick(object sender, EventArgs e)
        {
            string decode_str = DataTextBox.Text;         

            if (decode_str == string.Empty )
            {
                MessageBox.Show("Please type decode text data.");
                return;
            }

            if (decode_str.IndexOf('|') >= 0)
            {
                string[] Segments = decode_str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                // encode data
                QRCodeEncoder.EncodeQRcode(Segments);
            }

            // single segment
            else
            {
                // encode data
                QRCodeEncoder.EncodeQRcode(decode_str);
            }

        }
    }
}
