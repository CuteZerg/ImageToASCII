using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ImageToASCII
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Bitmap image;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.BMP;*.JPG;*.GIF;*.PNG|All files|*.*";
            int iteration = 0;
 
            while(true)
            {    
                Console.ReadLine();
                Console.Clear();

                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    image = new Bitmap(ofd.FileName);
                }
                else
                {
                    continue;
                }
                iteration++;
                ImageConverter converter = new ImageConverter(image);
                converter.Scale();
                converter.ToGreyGradient();
                File.WriteAllText("image" + iteration.ToString() + ".txt", converter.ToASCII("Negative"));
                Console.WriteLine(converter.ToASCII("Positive"));
                Console.SetCursorPosition(0, 0);
            }
        }
    }
}
