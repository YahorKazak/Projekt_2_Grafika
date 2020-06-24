using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace Projekt_2
{
    //создание треугольника
    public class trójkąt : IComparable<trójkąt>
    {
        internal float m;
        public Vector4[] tlist;
        public trójkąt(Vector4 first, Vector4 second, Vector4 third)
        {
            tlist = new Vector4[3];
            tlist[0] = first;
            tlist[1] = second;
            tlist[2] = third;



        }

        public int CompareTo(trójkąt other)
        {
            float first = (tlist[0].Z + tlist[1].Z + tlist[2].Z) / 3.0f;
            float second = (other.tlist[0].Z + other.tlist[1].Z + other.tlist[2].Z) / 3.0f;

            return second.CompareTo(first);

            throw new NotImplementedException();
        }
    }
    class E3D
    {
       Functions functions = new Functions();
       DrawL Line = new DrawL();
       Matrix4x4 matrixProj,MRotZ, MRotX;
       
       Bitmap bm;
       PictureBox pb;
       float Theta;
       Vector4 Camera;
       Vector4 Look;
        float fPlayer;


        //private void MatrixMnożenia(Matrix4x4 matrixProj, Vector4 x, Vector4 y)
        //{
        //    y.X = (x.X * matrixProj.M11 + x.Y * matrixProj.M21 + x.Z * matrixProj.M31 + matrixProj.M41);
        //    y.Y = (x.X * matrixProj.M12 + x.Y * matrixProj.M22 + x.Z * matrixProj.M32 + matrixProj.M42);
        //    y.Z = x.X * matrixProj.M13 + x.Y * matrixProj.M23 + x.Z * matrixProj.M33 + matrixProj.M43;

        //    float width = x.X * matrixProj.M14 + x.Y * matrixProj.M24 + x.Z * matrixProj.M34 + matrixProj.M44;

        //    if (width != 0.0f)
        //    {
        //        y.X /= width;
        //        y.Y /= width;
        //        y.Z /= width;
        //    }

        //}

        //konwertowanie trójkąta na ekran za pomocą Mproj 
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
           
           //Przyblizenie
           private void TranslatedZ(trójkąt y,trójkąt x)
           {
            for (int i = 0; i < 3; i++)
            {
                y.tlist[i].Z = x.tlist[i].Z + 5.0f;
            }
           }

            //Coordinates x i y
            private void Scale(trójkąt x)
           {
             for (int i = 0; i < 3; i++)
             {
                x.tlist[i].X += 2.0f;
                x.tlist[i].Y += 2.0f;
                x.tlist[i].X *= 0.3f * (float)pb.Width;
                x.tlist[i].Y *= 0.3f * (float)pb.Height;
             }
           
           }



           
           


        //фигура из треугольников
        List<trójkąt> Cube;
        public E3D(PictureBox image)
        {
            this.pb = image;
            Camera = new Vector4(0,0,0,1);
            Blender blender = new Blender();
            blender.LoadFigure();        
            Cube = blender.Figures;
        
            //matrixProj
            float fN = 0.1f;
            float fFr = 1000.0f;
            float fFv = 90.0f;
            float fAspctRat= (float)image.Height / (float)image.Width;

            matrixProj =  functions.MProjection(fFv, fAspctRat, fN, fFr);
           
        }
        public void FillFigure(trójkąt trójkąt, Bitmap bm)
        {
            float m = trójkąt.m;
            float R = m * 255;
            float G = m * 255;
            float B = m * 255;
            if (R < 0)
            {
                R = 0;
            }
            if (G < 0)
            {
                G = 0;
            }
            if (B < 0)
            {
                B = 0;
            }

            SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int)R, (int)G, (int)B));

            Point point1 = new Point((int)trójkąt.tlist[0].X, (int)trójkąt.tlist[0].Y);
            Point point2 = new Point((int)trójkąt.tlist[1].X, (int)trójkąt.tlist[1].Y);
            Point point3 = new Point((int)trójkąt.tlist[2].X, (int)trójkąt.tlist[2].Y);

            Point[] Points = { point1, point2, point3 };



            using (var graphics = Graphics.FromImage(bm))
            {
                graphics.FillPolygon(solidBrush, Points);
            }
        }




        public void UserUpdate(TimeSpan time)
        {
            bm = new Bitmap(pb.Width, pb.Height);

            double Etime = time.TotalMilliseconds / 1000;


            Theta += 0.5f * (float)Etime;

            if ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
            {
                Camera.Y += 0.01f + (float)Etime;
            }
            if ((Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0)
            {
                Camera.Y -= 0.01f + (float)Etime;
            }

            if ((Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0)
            {
                Camera.X += 0.01f + (float)Etime;
            }
            if ((Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0)
            {
                Camera.X -= 0.01f + (float)Etime;
            }

            Vector4 Moving = Look * (8.0f * (float)Etime);

            if ((Keyboard.GetKeyStates(Key.W) & KeyStates.Down) > 0)
            {
                Camera = functions.addVector(Camera, Moving);
            }
            if ((Keyboard.GetKeyStates(Key.S) & KeyStates.Down) > 0)
            {
                Camera = functions.subVector(Camera, Moving);
            }


            if ((Keyboard.GetKeyStates(Key.A) & KeyStates.Down) > 0)
            {
                fPlayer += 0.05f + (float)Etime;
            }
            if ((Keyboard.GetKeyStates(Key.D) & KeyStates.Down) > 0)
            {
                fPlayer -= 0.05f + (float)Etime;
            }





            MRotX = functions.MRotateX(Theta);

            MRotZ = functions.MRotateZ(Theta);

            Vector4 Up =  new Vector4(0, 1, 0, 1);
            Vector4 Target = new Vector4(0, 0, 1, 1);
            Matrix4x4 CameraR = functions.MRotateY(fPlayer);
            Look = functions.MMultVector(Target, CameraR);
            Target = functions.addVector(Camera, Look);

            Matrix4x4 mCamera = functions.MPointAt(Camera, Target, Up);

            Matrix4x4 mView = functions.M_QuickInverse(mCamera);

            List<trójkąt> Rastr = new List<trójkąt>();

            //rysowanie trójkątów
            foreach (trójkąt item in Cube)
            {
                trójkąt Projected, Translated ,RotZX, RotZ, Viewed;
                RotZ = new trójkąt(new Vector4(0, 0, 0,0), new Vector4(0, 0, 0,0), new Vector4(0, 0, 0,0));
                RotZX = new trójkąt(new Vector4(0, 0, 0,0), new Vector4(0, 0, 0,0), new Vector4(0, 0, 0,0));
                Viewed = new trójkąt(new Vector4(0, 0, 0,0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0));

                MatrixMnożenia1(MRotZ, item, RotZ);

                MatrixMnożenia1(MRotX, RotZ, RotZX);

                //przyblizenie zeby widzeć fuguru
                Translated = RotZX;
                TranslatedZ(Translated, RotZX);

                Projected = new trójkąt(new Vector4(0,0,0,0), new Vector4(0, 0, 0,0), new Vector4(0, 0, 0,0));


                //uleprzenie widoka figury
                Vector4 norm, linefirst, linesecond;
                 linefirst = new Vector4(0, 0, 0, 0);
                 linesecond = new Vector4(0, 0, 0, 0);

                linefirst.X = Translated.tlist[1].X - Translated.tlist[0].X;
                linefirst.Y = Translated.tlist[1].Y - Translated.tlist[0].Y;
                linefirst.Z = Translated.tlist[1].Z - Translated.tlist[0].Z;

                linesecond.X = Translated.tlist[2].X - Translated.tlist[0].X;
                linesecond.Y = Translated.tlist[2].Y - Translated.tlist[0].Y;
                linesecond.Z = Translated.tlist[2].Z - Translated.tlist[0].Z;

                norm = functions.VectorCrossProd(linefirst,linesecond);

                norm = functions.VNormalise(norm);

                float scalar = norm.X * (Translated.tlist[0].X - Camera.X) + norm.Y * (Translated.tlist[0].Y - Camera.Y) + norm.Z * (Translated.tlist[0].Z - Camera.Z);

                if (scalar < 0.0f)
                {
                    Vector4 light = new Vector4(0.0f, -1.0f, -1.0f, 1.0f);
                    light = functions.VNormalise(light);

                    float m = norm.X * light.X + norm.Y * light.Y + norm.Z * light.Z;


                    MatrixMnożenia1(mView, Translated, Viewed);


                    int ClippedT = 0;
                    trójkąt[] cliped = new trójkąt[2] { new trójkąt(new Vector4(0, 0, 0, 1), new Vector4(0, 0, 0, 1), new Vector4(0, 0, 0, 1)), new trójkąt(new Vector4(0, 0, 0, 1), new Vector4(0, 0, 0, 1), new Vector4(0, 0, 0, 1)) }; ;
                    Vector4 p_p = new Vector4(0.0f, 0.0f, 0.1f, 1.0f);
                    Vector4 p_n = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
                    ClippedT = functions.TClipAgainstPlane(p_p, p_n, Viewed, ref cliped[0], ref cliped[1]);
                    for (int i = 0; i < ClippedT; i++)
                    {

                        MatrixMnożenia1(matrixProj, cliped[i], Projected);


                        Scale(Projected);

                        Projected.m = m;

                        Rastr.Add(Projected);
                    }
                        
                    
                  
                }
               
            }
            Rastr.Sort();
            foreach (var tri in Rastr)
            {


                FillFigure(tri, bm);
               


            }
            pb.Image = bm;
            pb.Refresh();
           
        }

        
       
    }
}
