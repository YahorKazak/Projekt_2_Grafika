using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Projekt_2
{
    class Functions
    {
        public Vector4 addVector(Vector4 v1, Vector4 v2)
        {
            Vector4 v = new Vector4();
            v.X = v1.X + v2.X;
            v.Y = v1.Y + v2.Y;
            v.Z = v1.Z + v2.Z;
            return v;
        }

        public Vector4 subVector(Vector4 v1, Vector4 v2)
        {
            Vector4 v = new Vector4();
            v.X = v1.X - v2.X;
            v.Y = v1.Y - v2.Y;
            v.Z = v1.Z - v2.Z;
            return v;
        }

        public float VectordotProduct(Vector4 v1, Vector4 v2)
        {

            return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
        }

        public float VLength(Vector4 v)
        {
            return (float)Math.Sqrt(VectordotProduct(v, v));
        }
        public Vector4 VNormalise(Vector4 v)
        {

            float l = VLength(v);
            return new Vector4(v.X / l, v.Y / l, v.Z / l, 1);
        }

        public Vector4 VectorCrossProd(Vector4 v1, Vector4 v2)
        {
            Vector4 v = new Vector4();
            v.X = v1.Y * v2.Z - v1.Z * v2.Y;
            v.Y = v1.Z * v2.X - v1.X * v2.Z;
            v.Z = v1.X * v2.Y - v1.Y * v2.X;
            return v;
        }

        public Matrix4x4 MatrixIdentity()
        {
            Matrix4x4 mat = new Matrix4x4();
            mat.M11 = 1.0f;
            mat.M22 = 1.0f;
            mat.M33 = 1.0f;
            mat.M44 = 1.0f;
            return mat;
        }

        public Matrix4x4 MRotateX(float Theta)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat.M11 = 1.0f;
            mat.M22 = (float)Math.Cos(Theta * 0.5f);
            mat.M23 = (float)Math.Sin(Theta * 0.5f);
            mat.M32 = -(float)Math.Sin(Theta * 0.5f);
            mat.M33 = (float)Math.Cos(Theta * 0.5f);
            mat.M44 = 1.0f;
            return mat;
        }

        public Matrix4x4 MRotateY(float Theta)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat.M11 = (float)Math.Cos(Theta);
            mat.M13 = (float)Math.Sin(Theta);
            mat.M31 = -(float)Math.Sin(Theta);
            mat.M22 = 1.0f;
            mat.M33 = (float)Math.Cos(Theta); ;
            mat.M44 = 1.0f;
            return mat;
        }

        public Matrix4x4 MRotateZ(float Theta)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat.M11 = (float)Math.Cos(Theta);
            mat.M12 = (float)Math.Sin(Theta);
            mat.M21 = -(float)Math.Sin(Theta);
            mat.M22 = (float)Math.Cos(Theta);
            mat.M33 = 1.0f;
            mat.M44 = 1.0f;
            return mat;
        }

        public Matrix4x4 MTranslation(float x, float y, float z)
        {
            Matrix4x4 mat = new Matrix4x4(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
            mat.M11 = 1.0f;
            mat.M22 = 1.0f;
            mat.M33 = 1.0f;
            mat.M44 = 1.0f;
            mat.M41 = x;
            mat.M42 = y;
            mat.M43 = z;

            return mat;
        }

        public Matrix4x4 MProjection(float fFv, float fAspctRat, float fN, float fFr)
        {
            Matrix4x4 mat = new Matrix4x4(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
            float fFRad = 1.0f / (float)Math.Tan(fFv * 0.5f / 180.0f * (float)(Math.PI));
            mat.M11 = fAspctRat * fFRad;
            mat.M22 = fFRad;
            mat.M33 = fFr / (fFr - fN);
            mat.M43 = (-fFr * fN) / (fFr - fN);
            mat.M34 = 1.0f;
            mat.M44 = 0.0f;

            return mat;
        }


        public Vector4 MMultVector(Vector4 v1, Matrix4x4 M)
        {

            Vector4 v2 = new Vector4();
            v2.X = (v1.X * M.M11) + (v1.Y * M.M21) + (v1.Z * M.M31) + M.M41;
            v2.Y = (v1.X * M.M12) + (v1.Y * M.M22) + (v1.Z * M.M32) + M.M42;
            v2.Z = (v1.X * M.M13) + (v1.Y * M.M23) + (v1.Z * M.M33) + M.M43;
            v2.W = (v1.X * M.M14) + (v1.Y * M.M24) + (v1.Z * M.M34) + M.M44;

            return v2;

        }

       public  Matrix4x4 MPointAt(Vector4 pos, Vector4 target, Vector4 up)
        {
            Vector4 newForward = target - pos;
            newForward = VNormalise(newForward);

            Vector4 a = newForward * VectordotProduct(up, newForward);
            Vector4 newUp = up - a;
            newUp = VNormalise(newUp);

            Vector4 newRight = VectorCrossProd(newUp, newForward);

            Matrix4x4 matrix;

            matrix.M11 = newRight.X;
            matrix.M12 = newRight.Y;
            matrix.M13 = newRight.Y;

            matrix.M11 = newRight.X;
            matrix.M12 = newRight.Y;
            matrix.M13 = newRight.Z;
            matrix.M14 = 0.0f;
            matrix.M21 = newUp.X;
            matrix.M22 = newUp.Y;
            matrix.M23 = newUp.Z;
            matrix.M24 = 0.0f;
            matrix.M31 = newForward.X;
            matrix.M32 = newForward.Y;
            matrix.M33 = newForward.Z;
            matrix.M34 = 0.0f;
            matrix.M41 = pos.X;
            matrix.M42 = pos.Y;
            matrix.M43 = pos.Z;
            matrix.M44 = 1.0f;
            return matrix;
        }

       public Matrix4x4 M_QuickInverse(Matrix4x4 m) 
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.M11 = m.M11;
            matrix.M12 = m.M21;
            matrix.M13 = m.M31;
            matrix.M14 = 0.0f;
            matrix.M21 = m.M12;
            matrix.M22 = m.M22;
            matrix.M23 = m.M32;
            matrix.M24 = 0.0f;
            matrix.M31 = m.M13;
            matrix.M32 = m.M23;
            matrix.M33 = m.M33;
            matrix.M34 = 0.0f;
            matrix.M41 = -(m.M41 * matrix.M11 + m.M42 * matrix.M21 + m.M43 * matrix.M31);
            matrix.M42 = -(m.M41 * matrix.M12 + m.M42 * matrix.M22 + m.M43 * matrix.M32);
            matrix.M43 = -(m.M41 * matrix.M13 + m.M42 * matrix.M23 + m.M43 * matrix.M33);
            matrix.M44 = 1.0f;
            return matrix;
        }


    }
}
