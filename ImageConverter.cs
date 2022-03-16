using System;
using System.Drawing;

namespace ImageToASCII
{
    public class ImageConverter
    {
        private Bitmap image;
        private const int MAX_WIDTH = 600;
        private const float WIDTH_OFFSET = 2.0f;
        private const string ASCII_TABLE = " .:-=+*#%@";           // " .:-=+*#%@"; " .\'`^\",:; Il!i><~+_-?][}{1)(|\\/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$"; "====+++++++++********#########%%%%%%%%@@@@@@@@@@@@";  
        private const string ASCII_TABLE_NEGATIVE = "@%#*+=-:. ";  // "@%#*+=-:. "; "$@B%8&WM#*oahkbdpqwmZO0QLCJUYXzcvunxrjft/\\|()1{}[]?-_+~<>i!lI;:,\" ^`\'. "; "@@@@@@@@@@@@%%%%%%%%#########********+++++++++====";
        public ImageConverter(Bitmap image)
        {
            this.image = image;
        }
        private Bitmap ResizeBitmap(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }

        public Bitmap Scale()
        {
            int width = image.Width;
            int height = image.Height;
            int newHeight;
            float scale;
            scale = MAX_WIDTH / (float)width;
            newHeight = (int)Math.Round(height / WIDTH_OFFSET * scale);

            if (width > MAX_WIDTH || height > newHeight)
            {
                width = MAX_WIDTH;
                height = newHeight;
            }
            image = ResizeBitmap((Image)image, new Size(width, height));
            return image;
        }

        public Bitmap ToGreyGradient()
        {
            for(int j = 0; j < image.Height; j++)
            {
                for(int i = 0; i < image.Width; i++)
                {
                    int averageColor = (int)Math.Round((image.GetPixel(i, j).R + image.GetPixel(i, j).G + image.GetPixel(i, j).B) / 3.0);
                    Color color = Color.FromArgb(averageColor, averageColor, averageColor);
                    image.SetPixel(i, j, color);
                }
            }

            return image;
        }

        public string ToASCII(string state)
        {
            Mapper mapper = new Mapper();
            string result = "";
            for (int j = 0; j < image.Height; j++)
            {
                for (int i = 0; i < image.Width; i++)
                {
                    if (state == "Negative")
                        result += ASCII_TABLE_NEGATIVE[mapper.Map(image.GetPixel(i, j).R, 0, 255, 0, ASCII_TABLE_NEGATIVE.Length - 1)];
                    else
                        result += ASCII_TABLE[mapper.Map(image.GetPixel(i, j).R, 0, 255, 0, ASCII_TABLE.Length - 1)];
                    
                }
                result += "\n";
            }
            return result;
        }
    }
}
