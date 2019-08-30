using System;
using System.Collections.Generic;
using System.Text;

namespace MathRealize
{
    public static class ThreeDimensionaltransform
    {
        public static double[][] CreateVector(double x, double y, double z)
        {
            return new double[][]
            {
                new double[]{x},
                new double[]{y},
                new double[]{z},
                new double[]{1},
            };
        }
        public static double[][] RotateX(double angle)
        {
            return new double[][]
            {
                new double[]{1 ,0,0,0 },
                new double[]{0,Math.Cos(angle),-Math.Sin(angle),0 },
                new double[]{0,Math.Sin(angle),Math.Cos(angle),0 },
                new double[]{ 0,0,0,1},
            };
        }
        public static double[][] RotateY(double angle)
        {
            return new double[][]
            {
                new double[]{Math.Cos(angle),0,Math.Sin(angle),0 },
                new double[]{0 ,1,0,0 },
                new double[]{-Math.Sin(angle),0,Math.Cos(angle),0 },
                new double[]{ 0,0,0,1},
            };
        }
        public static double[][] RotateZ(double angle)
        {
            return new double[][]
            {
                new double[]{Math.Cos(angle),-Math.Sin(angle),0,0 },
                new double[]{Math.Sin(angle),Math.Cos(angle),0,0 },
                new double[]{0 ,0,1,0 },
                new double[]{ 0,0,0,1},
            };
        }

        public static double[][] GetTransformMatrix(double x, double y, double z)
        {
            return new double[][]
            {
                new double[]{1,0,0,x },
                new double[]{0,1,0,y},
                new double[]{0 ,0,1,z },
                new double[]{ 0,0,0,1},
            };
        }
        public static double[][] RotateXFollowZero(this double[][] data, double angle)
        {
            return RotateX(angle).Mul(data);
        }
        public static double[][] RotateYFollowZero(this double[][] data, double angle)
        {
            return RotateY(angle).Mul(data);
        }
        public static double[][] RotateZFollowZero(this double[][] data, double angle)
        {
            return RotateZ(angle).Mul(data);
        }
        public static double[][] Transform(this double[][] data, double x, double y, double z)
        {
            return new double[][]
            {
                new double[]{1,0,0,x},
                new double[]{0,1,0,y},
                new double[]{0,0,1,z},
                new double[]{0,0,0,1},
            }.Mul(data);
        }
        public static double[][] Transform(this double[][] data, params double[][][] value)
        {
            double[][] res = value[0];
            for (int i = 1; i < value.Length; i++)
            {
                res = res.Mul(value[i]);
            }
            return res.Mul(data);
        }
        public static double[] BackOrigin(double x, double y, double z)
        {
            double[] result = new double[]
            {
                Math.Atan(y/z),
                -Math.Atan(x/Math.Sqrt(y*y+z*z))
            };
            return result;
        }
    }
}
