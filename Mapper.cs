using System;

namespace ImageToASCII
{
    public class Mapper
    {
        public int Map(int num, int start1, int stop1, int start2, int stop2)
        {
            return (int)Math.Round((stop2 - start2)*(num - start1)/((double)stop1 - start1) + start2);
        }
    }
}
