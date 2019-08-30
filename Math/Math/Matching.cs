using System;
using System.Collections.Generic;
using System.Text;

namespace MathRealize
{
    public class Matching
    {
        /// <summary>
        /// 拟合直线
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="callback"></param>
        public static void FitStraightLine(int[] x, int[] y, Action<double, double, double> callback)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("x的长度不等于y的长度");
            }
            double xsum = 0;//x的和
            double ysum = 0;//y的和
            double xAvg = 0;//x的平均数
            double yAvg = 0;//y的平均数
            int count = x.Length;
            foreach (var item in x)
            {
                xsum += item;
            }
            xAvg = xsum / count;
            foreach (var item in y)
            {
                ysum += item;
            }
            yAvg = ysum / count;
            double k = 0;
            double b = 0;
            double a1 = 0;
            double a2 = 0;
            for (int i = 0; i < count; i++)
            {
                a1 += (x[i] - xAvg) * (y[i] - yAvg);
                a2 += (x[i] - xAvg) * (x[i] - xAvg);
            }
            k = a1 / a2;
            b = yAvg - k * xAvg;
            double sst = 0;
            double ssr = 0;
            for (int i = 0; i < y.Length; i++)
            {
                var ytmp = y[i];
                var xtmp = x[i];
                sst += (ytmp - yAvg) * (ytmp - yAvg);
                var tmp = k * xtmp + b - yAvg;
                ssr += tmp * tmp;
            }
            double r2 = ssr / sst;
            callback(k, b, r2);
        }

        /// <summary>
        /// 平面拟合 a0,a1,a2  z=a0x+a1y+a2
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="callback">a0 a1 a2 f</param>
        public static void FitPlane(double[] x, double[] y, double[] z, Action<double[]> callback)
        {
            double xxSum = 0;
            double yySum = 0;
            double xSum = 0;
            double ySum = 0;
            double zSum = 0;
            double xySum = 0;
            double xzSum = 0;
            double yzSum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                var xItem = x[i];
                var yItem = y[i];
                var zItem = z[i];
                xxSum += xItem * xItem;
                yySum += yItem * yItem;
                xSum += xItem;
                ySum += yItem;
                zSum += zItem;
                xySum += xItem * yItem;
                xzSum += xItem * zItem;
                yzSum += yItem * zItem;
            }
            double[][] a = new double[][]
            {
                new double[]{xxSum,xySum,xSum },
                new double[]{xySum,yySum,ySum },
                new double[]{xSum,ySum,x.Length },
            };
            double[][] b = new double[][]
            {
                 new double[]{xzSum},
                 new double[]{yzSum},
                 new double[]{zSum},
            };
            var res = b.DivL(a);
            double[] aRes = new double[res.Length + 1];
            for (int i = 0; i < res.Length; i++)
            {
                aRes[i] = res[i][0];
            }
            double min = double.MaxValue;
            double max = double.MinValue;
            for (int i = 0; i < x.Length; i++)
            {
                var distance = PointToPlaneLength(x[i], y[i], z[i], aRes[0], aRes[1], -1, aRes[2]);
                if (distance > max)
                {
                    max = distance;
                    continue;
                }
                if (distance < min)
                {
                    min = distance;
                    continue;
                }
            }
            aRes[3] = max - min;
            callback(aRes);
        }

        /// <summary>
        /// 点到线的距离  ax+by+c=0
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double PointToLineLength(double x, double y, double a, double b, double c)
        {
            double d = (a * x + b * y + c)
                 / Math.Sqrt(a * a + b * b);
            return d;
        }
        /// <summary>
        /// 点到面的距离   ax+by+cz+d=0
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double PointToPlaneLength(double x, double y, double z, double a, double b, double c, double d)
        {
            double distance = (a * x + b * y + c * z + d)
                 / Math.Sqrt(a * a + b * b + c * c);
            return distance;
        }
    }
}
