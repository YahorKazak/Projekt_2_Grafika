using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projekt_2
{
    class DrawL
    {
        public Bitmap Przyrostowy(Bitmap n, int x1, int y1, int x2, int y2)
        {
            int x;
            int y;
            float deltax, deltay, Y, X, m;
            deltax = x2 - x1;
            deltay = y2 - y1;
            m = deltay / deltax;
            Y = y1;
            X = x1;
            if (Math.Abs(m) >= 1)
            {
                for (y = y1; y <= y2; y++)
                {
                    n.SetPixel((int)Math.Floor(X + 0.5), (int)y, Color.White);
                    X += (1 / m);
                }
            }
            else
            {
                for (x = x1; x <= x2; x++)
                {
                    n.SetPixel((int)x, (int)Math.Floor(Y + 0.5), Color.White);
                    Y += m;
                }
            }
            if (deltay < 0 || deltax < 0)
            {
                if (Math.Abs(m) >= 1)
                {
                    X = x2;
                    for (y = y2; y <= y1; y++)
                    {
                        n.SetPixel((int)Math.Floor(X + 0.5), (int)y, Color.White);
                        X += (1 / m);
                    }

                }
                else
                {
                    Y = y2;
                    for (x = x2; x <= x1; x++)
                    {
                        n.SetPixel((int)x, (int)Math.Floor(Y + 0.5), Color.White);
                        Y += m;
                    }
                }
            }



            return n;

        }
    }
}
