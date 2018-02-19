using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace OrphanageService.Utilities
{
    public class ImageAdapter
    {
        public static byte[] Resize(byte[] img, Size newSize, long compertion)
        {
            byte[] retData = null;
            try
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, compertion);
                myEncoderParameters.Param[0] = myEncoderParameter;
                Image image = Image.FromStream(new MemoryStream(img));
                Image retImage = new Bitmap(image, newSize);
                using (MemoryStream mem = new MemoryStream())
                {
                    retImage.Save(mem, jpgEncoder, myEncoderParameters);
                    retData = mem.ToArray();
                }
            }
            catch
            {
                return null;
            }
            return retData;
        }

        public static byte[] Resize(byte[] img, int height, int width)
        {
            return Resize(img, new Size(width, height), 100);
        }

        public static byte[] Resize(byte[] img, int height, int width, long compertion)
        {
            return Resize(img, new Size(width, height), compertion);
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}