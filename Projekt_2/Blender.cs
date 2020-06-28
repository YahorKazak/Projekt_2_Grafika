using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;
using System.IO;

namespace Projekt_2
{
    //sczytuje dane figur z pliku
    class Blender
    {
        public List<trójkąt> Figures = new List<trójkąt>();
        List<Vector3> verticies = new List<Vector3>();
        List<int[]> indexOfVerticies = new List<int[]>();

        public string path = @"C:\Users\Egor\Desktop\GitHub\Projekt_2_Grafika\Projekt_2\Figures.obj";

        public void LoadFigure()
        {

            string[] lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                if (line[0] == 'v' && line[1] == ' ')
                {
                    string[] vertex = line.ToString().Split(' ');

                    Vector3 v = new Vector3(float.Parse(vertex[1], CultureInfo.InvariantCulture.NumberFormat),
                                              float.Parse(vertex[2], CultureInfo.InvariantCulture.NumberFormat),
                                              float.Parse(vertex[3], CultureInfo.InvariantCulture.NumberFormat));
                    verticies.Add(v);
                }

                if (line[0] == 'f' && line[1] == ' ')
                {
                    string[] points = line.Split(' ');

                    int tri1, tri2, tri3;

                    tri1 = int.Parse(points[1]) - 1;
                    tri2 = int.Parse(points[2]) - 1;
                    tri3 = int.Parse(points[3]) - 1;

                    int[] tempIDTriangle = { tri1, tri2, tri3 };

                    indexOfVerticies.Add(tempIDTriangle);

                }

            }
            foreach (var singleTriangleIndex in indexOfVerticies)
            {
                int v1Index = singleTriangleIndex[0];
                int v2Index = singleTriangleIndex[1];
                int v3Index = singleTriangleIndex[2];

                trójkąt tr = new trójkąt(new Vector4(verticies[v1Index], 0f), new Vector4(verticies[v2Index], 0f), new Vector4(verticies[v3Index], 0f));                
                Figures.Add(tr);

            }
        }
    }
}
