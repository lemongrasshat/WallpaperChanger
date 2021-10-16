using System;
using System.Drawing;
using System.Runtime.InteropServices;



namespace Wallpaper_changer
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;
        public double ReturnDateDifference(DateTime Date)
        {
            DateTime Todaysdate = DateTime.Today;
            return (Date -Todaysdate).TotalDays;
        }
        static void Main(string[] args)
        {
            var bitmap = new Bitmap(1920, 1080);

            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Height; y++)
                {
                    bitmap.SetPixel(x, y, Color.Black);
                }
            }

            Color StringColor = System.Drawing.Color.Red;//direct color adding   
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
            stringformat.LineAlignment = StringAlignment.Center;

            Graphics graphicsImage = Graphics.FromImage(bitmap);
            string Str_TextOnImage = "";
            string pattern = "dd/MM/yyyy";
            DateTime inputdate = DateTime.ParseExact("01/02/2023", pattern, null);
            Program obj = new Program();
            Str_TextOnImage+=obj.ReturnDateDifference(inputdate).ToString();

            graphicsImage.DrawString(Str_TextOnImage, new Font("arial", 200,FontStyle.Bold), new SolidBrush(StringColor), new Point(960, 540),stringformat);
            bitmap.Save("D:\\test.bmp");
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, "D:\\test.bmp", SPIF_UPDATEINIFILE);
            Console.WriteLine("Wallpaper Changed!");
            Console.ReadKey();
        }
        
    }
    
}
