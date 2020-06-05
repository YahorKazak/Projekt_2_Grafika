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
      
       Matrix4x4 matrixProj;
       Bitmap bm;
       PictureBox pb;

       Vector3 Camera;

           private void MatrixMnożenia(Matrix4x4 matrixProj,Vector3 x,Vector3 y)
           {
            y.X = x.X * matrixProj.M11 + x.Y * matrixProj.M21 + x.Z * matrixProj.M31 + matrixProj.M41;
            y.Y = x.X * matrixProj.M12 + x.Y * matrixProj.M22 + x.Z * matrixProj.M32 + matrixProj.M42;
            y.Z = x.X * matrixProj.M13 + x.Y * matrixProj.M23 + x.Z * matrixProj.M33 + matrixProj.M43;
            
            float width = x.X * matrixProj.M14 + x.Y * matrixProj.M24 + x.Z * matrixProj.M34 + matrixProj.M44;

            if (width != 0.0f)
            {
                y.X /= width;
                y.Y /= width;
                y.Z /= width;
            }

           }

           protected class trójkąt
           {
            List<Vector3> tlist;
            public trójkąt(Vector3 first, Vector3 second,Vector3 third)
            {
                tlist = new List<Vector3>();
                tlist.Add(first);
                tlist.Add(second);
                tlist.Add(third);
            }
           }

        List<trójkąt> Cube;
        public E3D(PictureBox image)
        {
            this.pb = image;
            Camera = new Vector3(0, 0, 0);
            Cube = new List<trójkąt>();

            Cube.Add(new trójkąt( new Vector3(0, 0, 0), new Vector3(0, 1, 0),  new Vector3(1, 1, 0)));
            Cube.Add(new trójkąt(new Vector3(0, 0, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0)));

            Cube.Add(new trójkąt(new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 1)));
            Cube.Add(new trójkąt(new Vector3(1, 0, 0), new Vector3(1, 1, 1), new Vector3(1, 0, 1)));

            Cube.Add(new trójkąt(new Vector3(1, 0, 1), new Vector3(1, 1, 1), new Vector3(0, 1, 1)));
            Cube.Add(new trójkąt(new Vector3(1, 0, 1), new Vector3(0, 1, 1), new Vector3(0, 0, 1)));

            Cube.Add(new trójkąt(new Vector3(0, 0, 1), new Vector3(0, 1, 1), new Vector3(0, 1, 0)));
            Cube.Add(new trójkąt(new Vector3(0, 0, 1), new Vector3(0, 1, 0), new Vector3(0, 0, 0)));

            Cube.Add(new trójkąt(new Vector3(0, 1, 0), new Vector3(0, 1, 1), new Vector3(1, 1, 1)));
            Cube.Add(new trójkąt(new Vector3(0, 1, 0), new Vector3(1, 1, 1), new Vector3(1, 1, 0)));

            Cube.Add(new trójkąt(new Vector3(1, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 0)));
            Cube.Add(new trójkąt(new Vector3(1, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 0, 0)));
        }


        public void UserUpdate(TimeSpan time)
        {
            bm = new Bitmap(pb.Width, pb.Height);

            foreach (trójkąt item in Cube)
            {

            }
        }


       
    }
}
