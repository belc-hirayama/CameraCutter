using OpenCvSharp;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace CameraCutter.camera
{
    internal class WebCamera
    {
        Mat frame;
        VideoCapture capture;
        public Bitmap bmp;
        public int Width;
        public int Height;
        public int Cols;
        public int Rows;
        public WebCamera()
        {
            capture = new(0);
            if (!capture.IsOpened())
                throw new Exception("camera was not found");
            Width = capture.FrameWidth;
            Height = capture.FrameHeight;

            frame = new Mat(Height, Width, MatType.CV_8UC3);

            Cols = frame.Cols;
            Rows = frame.Rows;
            bmp = new Bitmap(Cols, Rows, (int)frame.Step(), System.Drawing.Imaging.PixelFormat.Format24bppRgb, frame.Data);
        }

        public void Grab()
        {
            capture.Grab();
            OpenCvSharp.Internal.NativeMethods.videoio_VideoCapture_operatorRightShift_Mat(capture.CvPtr, frame.CvPtr);
        }
        public Bitmap GetImage()
        {
            using (MemoryStream ms = new())
            {
                bmp.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                return new Bitmap(ms);
            }
        }

    }
}
