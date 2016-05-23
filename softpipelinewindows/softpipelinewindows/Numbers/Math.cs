using System;

public sealed class MathS
{
    public static readonly float PI = (float)Math.PI;

    public static readonly float DegToRad = (float)(Math.PI / 180);

    public static readonly float RadToDeg = (float)(180 / Math.PI);

    public static int Min(params int[] vals)
    {
        int min = int.MaxValue;
        foreach (int val in vals)
        {
            if (val < min) min = val;
        }
        return min;
    }

    public static int Max(params int[] vals)
    {
        int max = int.MinValue;
        foreach (int val in vals)
        {
            if (val > max) max = val;
        }
        return max;
    }

    public static float Min(params float[] vals)
    {
        float min = float.MaxValue;
        foreach (float val in vals)
        {
            if (val < min) min = val;
        }
        return min;
    }

    public static float Max(params float[] vals)
    {
        float max = float.MinValue;
        foreach (float val in vals)
        {
            if (val > max) max = val;
        }
        return max;
    }

    public static long Clamp(long value, long min, long max)
    {
        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        return (long)value;
    }

    public static float Clamp(float value, float min, float max)
    {
        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        return value;
    }

    public static float Sqrt(float value)
    {
        if (value < 0)
        {
            throw new NotSupportedException();
        }
        else if (value == 0)
        {
            return 0;
        }
        else
        {
            return (float)Math.Sqrt(value);
        }
    }

    public static float Abs(float value)
    {
        return value >= 0 ? value : -value;
    }

    public static float Sign(float value)
    {
        return value >= 0 ? 1 : -1;
    }

    public static float Sin(float value)
    {
        return (float)Math.Sin(value);
    }

    public static float Cos(float value)
    {
        return (float)Math.Cos(value);
    }

    public static float Tan(float value)
    {
        return (float)Math.Tan(value);
    }

    public static float Atan(float value)
    {
        return (float)Math.Atan(value);
    }

    public static float Atan2(float y, float x)
    {
        return (float)Math.Atan2(y, x);
    }

    public static float Asin(float value)
    {
        return (float)Math.Asin(value);
    }

    public static float Acos(float value)
    {
        return (float)Math.Acos(value);
    }
}
