using System;

public class FFT
{
    // aSamples.Length need to be a power of two
    public static Complex[] CalculateFFT(Complex[] aSamples, bool aReverse)
    {
        int power = (int)Math.Log(aSamples.Length, 2);
        int count = 1;
        for (int i = 0; i < power; i++)
            count <<= 1;

        int mid = count >> 1; // mid = count / 2;
        int j = 0;
        for (int i = 0; i < count - 1; i++)
        {
            if (i < j)
            {
                var tmp = aSamples[i];
                aSamples[i] = aSamples[j];
                aSamples[j] = tmp;
            }
            int k = mid;
            while (k <= j)
            {
                j -= k;
                k >>= 1;
            }
            j += k;
        }
        Complex r = new Complex(-1, 0);
        int l2 = 1;
        for (int l = 0; l < power; l++)
        {
            int l1 = l2;
            l2 <<= 1;
            Complex r2 = new Complex(1, 0);
            for (int n = 0; n < l1; n++)
            {
                for (int i = n; i < count; i += l2)
                {
                    int i1 = i + l1;
                    Complex tmp = r2 * aSamples[i1];
                    aSamples[i1] = aSamples[i] - tmp;
                    aSamples[i] += tmp;
                }
                r2 = r2 * r;
            }
            r.img = Math.Sqrt((1d - r.real) / 2d);
            if (!aReverse)
                r.img = -r.img;
            r.real = Math.Sqrt((1d + r.real) / 2d);
        }
        if (!aReverse)
        {
            double scale = 1d / count;
            for (int i = 0; i < count; i++)
                aSamples[i] *= scale;
        }
        return aSamples;
    }


    #region float / double array conversion helpers
    public static Complex[] Double2Complex(double[] aData)
    {
        Complex[] data = new Complex[aData.Length];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = new Complex(aData[i], 0);
        }
        return data;
    }
    public static double[] Complex2Double(Complex[] aData, bool aReverse)
    {
        double[] result = new double[aData.Length];
        if (!aReverse)
        {
            for (int i = 0; i < aData.Length; i++)
            {
                result[i] = aData[i].magnitude;
            }
            return result;
        }
        for (int i = 0; i < aData.Length; i++)
        {
            result[i] = Math.Sign(aData[i].real) * aData[i].magnitude;
        }
        return result;
    }

    public static Complex[] Float2Complex(float[] aData)
    {
        Complex[] data = new Complex[aData.Length];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = new Complex(aData[i], 0);
        }
        return data;
    }
    public static float[] Complex2Float(Complex[] aData, bool aReverse)
    {
        float[] result = new float[aData.Length];
        if (!aReverse)
        {
            for (int i = 0; i < aData.Length; i++)
            {
                result[i] = (float)aData[i].magnitude;
            }
            return result;
        }
        for (int i = 0; i < aData.Length; i++)
        {
            result[i] = (float)(Math.Sign(aData[i].real) * aData[i].magnitude);
        }
        return result;
    }
    #endregion float / double array conversion helpers
}
