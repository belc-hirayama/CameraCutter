using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCutter.imaeg
{
    internal class ImageEditor
    {
        public static Bitmap Trim(Bitmap srcBitmap, int left, int top, int right, int bottom)
        {

            Rectangle srcRect = new Rectangle(left, top, right - left, bottom - top);   //  right > leftなど非考慮
            Rectangle dstRect = new Rectangle(0, 0, srcRect.Width, srcRect.Height);
            Bitmap dst = new(srcRect.Width, srcRect.Height);
            Graphics dstGraphic = Graphics.FromImage(dst);

            dstGraphic.DrawImage(srcBitmap, dstRect, srcRect, GraphicsUnit.Pixel);
            dstGraphic.Dispose();
            return dst;
        }

        public static void Save(Bitmap bitmap)
        {
            bitmap.Save(@"./capture.png");
        }
    }
}
