using System;
using System.Collections.Generic;
using System.Text;

namespace MathRealize
{
    public class FourierTransform
    {
        /// <summary>
        /// 离散傅里叶变换（输入是复数）（书）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Complex[] DFT(Complex[] data)
        {
            var res = new Complex[data.Length];
            var N = data.Length;
            for (int v = 0; v < data.Length; v++)
            {
                Complex plural = new Complex(0, 0);
                for (int t = 0; t < N; t++)
                {
                    var tmp = data[t];
                    if (tmp.Re == 0 && tmp.Im == 0)
                    {
                        continue;
                    }
                    if (v != 0 && t != 0)
                    {
                        //y = -2 * Math.PI * v / N * t;
                        //real += data[t] * Math.Cos(y);
                        //im += data[t] * Math.Sin(y);

                        //优化
                        //y = 2 * Math.PI * v / N * t;                        
                        Complex baseE = Complex.ExpBase(v * t, N); //new Complex(Math.Cos(y), Math.Sin(y));
                        plural += tmp * baseE;
                    }
                    else
                    {
                        //y=0
                        //cos(y)=1 sin(y)=0
                        plural += tmp;
                    }
                }
                res[v] = plural;
            }
            return res;
        }
        /// <summary>
        /// 快速离散傅里叶
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Complex[] FFT(Complex[] data)
        {
            return FFT(data, data.Length);
        }
        /// <summary>
        /// 快速离散傅里叶
        /// </summary>
        /// <param name="data">数组</param>
        /// <param name="dataLen">数组长度</param>
        /// <returns></returns>
        private Complex[] FFT(Complex[] data, int dataLen)
        {
            int n = data.Length;
            if (n == 1)
            {
                return data;
            }
            int m = n / 2;
            Complex[] bufferEven = new Complex[m];
            Complex[] bufferOdd = new Complex[m];
            for (int i = 0; i < m; i++)
            {
                bufferEven[i] = data[i * 2];
                bufferOdd[i] = data[i * 2 + 1];
            }
            var newEven = FFT(bufferEven, dataLen);
            var newOdd = FFT(bufferOdd, dataLen);
            Complex[] outputArr = new Complex[n];
            int currentIndex = 0;
            var step = dataLen / n;
            var currentStep = 0;
            for (int i = 0; i < n; i++)
            {
                if (currentIndex >= newEven.Length)
                {
                    currentIndex = 0;
                }
                outputArr[i] = newEven[currentIndex] + newOdd[currentIndex] * Complex.ExpBase(currentStep, dataLen);
                currentStep += step;
                currentIndex++;
            }
            return outputArr;
        }
    }
}
