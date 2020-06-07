using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using System.Windows.Forms;

namespace Projekt_2
{
    class E3D
    {
       DrawL Line = new DrawL();
       Matrix4x4 matrixProj;
       Bitmap bm;
       PictureBox pb;

       Vector3 Camera;

           private void MatrixMnożenia(Matrix4x4 matrixProj,Vector3 x,Vector3 y)
           {
            y.X = (x.X * matrixProj.M11 + x.Y * matrixProj.M21 + x.Z * matrixProj.M31 + matrixProj.M41);
            y.Y = (x.X * matrixProj.M12 + x.Y * matrixProj.M22 + x.Z * matrixProj.M32 + matrixProj.M42);
            y.Z = x.X * matrixProj.M13 + x.Y * matrixProj.M23 + x.Z * matrixProj.M33 + matrixProj.M43;
            
            float width = x.X * matrixProj.M14 + x.Y * matrixProj.M24 + x.Z * matrixProj.M34 + matrixProj.M44;

            if (width != 0.0f)
            {
                y.X /= width;
                y.Y /= width;
                y.Z /= width;
            }

           }
           private void MatrixMnożenia1(Matrix4x4 matrixProj, trójkąt x, trójkąt y)
           {
            for (int i = 0; i < 3; i++)
            {
                y.tlist[i].X = x.tlist[i].X * matrixProj.M11 + x.tlist[i].Y * matrixProj.M21 + x.tlist[i].Z * matrixProj.M31 + matrixProj.M41;
                y.tlist[i].Y = x.tlist[i].X * matrixProj.M12 + x.tlist[i].Y * matrixProj.M22 + x.tlist[i].Z * matrixProj.M32 + matrixProj.M42;
                y.tlist[i].Z = x.tlist[i].X * matrixProj.M13 + x.tlist[i].Y * matrixProj.M23 + x.tlist[i].Z * matrixProj.M33 + matrixProj.M43;

                float w = x.tlist[i].X * matrixProj.M14 + x.tlist[i].Y * matrixProj.M24 + x.tlist[i].Z * matrixProj.M34 + matrixProj.M44;

                if (w != 0.0f)
                {
                    y.tlist[i].X /= w; y.tlist[i].Y /= w; y.tlist[i].Z /= w;
                }
            }
           }

            private void Scale(trójkąt x)
           {
             for (int i = 0; i < 3; i++)
             {
                x.tlist[i].X += 1.0f;
                x.tlist[i].Y += 1.0f;
                x.tlist[i].X *= 0.3f * (float)pb.Width;
                x.tlist[i].Y *= 0.3f * (float)pb.Height;
             }
           
           }

           protected   class trójkąt
           {
            public Vector3[] tlist;
            public trójkąt(Vector3 first, Vector3 second,Vector3 third)
            {
                tlist = new Vector3[3];
                tlist[0]=first;
                tlist[1]=second;
                tlist[2]=third;
            }
           }

        List<trójkąt> Cube;
        public E3D(PictureBox image)
        {
            this.pb = image;
            Camera = new Vector3(0, 0, 0);
            Cube = new List<trójkąt>();

            Cube.Add(new trójkąt( new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f),  new Vector3(1f, 1f, 0f)));
            Cube.Add(new trójkąt(new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 0f), new Vector3(1f, 0f, 0f)));

            Cube.Add(new trójkąt(new Vector3(1f, 0f, 0f), new Vector3(1f, 1f, 0f), new Vector3(1f, 1f, 1f)));
            Cube.Add(new trójkąt(new Vector3(1f, 0f, 0f), new Vector3(1f, 1f, 1f), new Vector3(1f, 0f, 1f)));

            Cube.Add(new trójkąt(new Vector3(1f, 0f, 1f), new Vector3(1f, 1f, 1f), new Vector3(0f, 1f, 1f)));
            Cube.Add(new trójkąt(new Vector3(1f, 0f, 1f), new Vector3(0f, 1f, 1f), new Vector3(0f, 0f, 1f)));

            Cube.Add(new trójkąt(new Vector3(0f, 0f, 1f), new Vector3(0f, 1f, 1f), new Vector3(0f, 1f, 0f)));
            Cube.Add(new trójkąt(new Vector3(0f, 0f, 1f), new Vector3(0f, 1f, 0f), new Vector3(0f, 0f, 0f)));

            Cube.Add(new trójkąt(new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 1f), new Vector3(1f, 1f, 1f)));
            Cube.Add(new trójkąt(new Vector3(0f, 1f, 0f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 0f)));

            Cube.Add(new trójkąt(new Vector3(1f, 0f, 1f), new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, 0f)));
            Cube.Add(new trójkąt(new Vector3(1f, 0f, 1f), new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f)));

            //matrixProj
            float fN = 0.1f;
            float fFr = 1000.0f;
            float fFv = 90.0f;
            float fAspctRat= (float)image.Height / (float)image.Width;
            float fFRad = 1.0f / (float)Math.Tan(fFv * 0.5f / 180.0f * (float)(Math.PI));

            matrixProj.M11 = fAspctRat * fFRad;
            matrixProj.M22 = fFRad;
            matrixProj.M33 = fFr /(fFr - fN);
            matrixProj.M43 = (-fFr * fN)/(fFr - fN);
            matrixProj.M34 = 1.0f;
            matrixProj.M44 = 0.0f;

        }


        public void UserUpdate(TimeSpan time)
        {
            bm = new Bitmap(pb.Width, pb.Height);

            foreach (trójkąt item in Cube)
            {
                
                trójkąt Projected = new trójkąt(new Vector3(0,0,0), new Vector3(0, 0, 0), new Vector3(0, 0, 0));
                
                MatrixMnożenia1(matrixProj, item, Projected);
              

                Scale(Projected);
               


                Line.Przyrostowy(bm, (int)Projected.tlist[0].X, (int)Projected.tlist[0].Y, (int)Projected.tlist[1].X, (int)Projected.tlist[1].Y);
                Line.Przyrostowy(bm, (int)Projected.tlist[1].X, (int)Projected.tlist[1].Y, (int)Projected.tlist[2].X, (int)Projected.tlist[2].Y);
                Line.Przyrostowy(bm, (int)Projected.tlist[2].X, (int)Projected.tlist[2].Y, (int)Projected.tlist[0].X, (int)Projected.tlist[0].Y);

               
            }
            pb.Image = bm;
           
        }

        
       
    }
}
