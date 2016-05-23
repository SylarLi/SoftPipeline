/// <summary>
/// Quaternion: ( xi + yj + zk + w )
/// Unit Quaternion 单位四元数: ( sin(r/2)(ai + bj + ck) + cos(r/2) ) 其中(a, b, c)为单位向量 sin(r/2)*a == x 以此类推
/// Euler Order:  z/x/y
/// </summary>
public struct Quaternion
{
    private const float Threshold = 1e-6f;

    public float x;

    public float y;

    public float z;

    public float w;

    public Quaternion(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public override string ToString()
    {
        return string.Format("Quaternion({0}, {1}, {2}, {3})", x, y, z, w);
    }

    public float sqrMagnitude
    {
        get
        {
            return x * x + y * y + z * z + w * w;
        }
    }

    public float magnitude
    {
        get
        {
            return MathS.Sqrt(sqrMagnitude);
        }
    }

    public void Normalize()
    {
        float len = magnitude;
        if (len <= Threshold)
        {
            x = y = z = w = 0;
        }
        else
        {
            x /= len;
            y /= len;
            z /= len;
            w /= len;
        }
    }

    public Quaternion normalized
    {
        get
        {
            Quaternion q = this;
            q.Normalize();
            return q;
        }
    }

    /// <summary>
    /// 对应的旋转矩阵(单位四元数有效)
    /// </summary>
    /// <returns></returns>
    public Matrix ToMatrix()
    {
        return new Matrix(new float[4, 4] {
            { 1 - 2 * (y * y + z * z), 2 * (x * y - z * w), 2 * (x * z + y * w), 0 },
            { 2 * (x * y + z * w), 1 - 2 * (x * x + z * z), 2 * (y * z - x * w), 0 },
            { 2 * (x * z - y * w), 2 * (y * z + x * w), 1 - 2 * (x * x + y * y), 0 },
            { 0, 0, 0, 1 }
        });
    }

    /// <summary>
    /// 欧拉角(单位四元数有效)
    /// </summary>
    /// <returns></returns>
    public Vector3 ToEulers()
    {
        Vector3 euler = Vector3.zero;
        Matrix m = ToMatrix();
        if (MathS.Abs(m[1, 0]) <= Threshold && 
            MathS.Abs(m[1, 1]) <= Threshold)
        {
            euler.x = m[1, 2] <= 0 ? 90 : -90;
            euler.y = 0;
            euler.z = MathS.Atan2(-m[0, 1], m[0, 0]) * MathS.RadToDeg;
        }
        else
        {
            euler.x = MathS.Asin(-m[1, 2]);
            euler.y = MathS.Atan2(m[0, 2], m[2, 2]);
            euler.z = MathS.Atan2(m[1, 0], m[1, 1]);
            euler *= MathS.RadToDeg;
        }
        return euler;
    }

    /// <summary>
    /// 旋转轴和旋转角(单位四元数有效)
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="axisNormal"></param>
    public void ToAngleAxis(out float angle, out Vector3 axisNormal)
    {
        Vector3 axis = new Vector3(
            MathS.Abs(x) <= Threshold ? 0f : x,
            MathS.Abs(y) <= Threshold ? 0f : y,
            MathS.Abs(z) <= Threshold ? 0f : z
        );
        if (axis == Vector3.zero)
        {
            angle = 0;
            axisNormal = Vector3.zero;
        }
        else
        {
            float length = axis.magnitude;
            angle = MathS.Atan2(length, w) * 2 * MathS.RadToDeg;
            axisNormal = axis / length;
        }
    }

    /// <summary>
    /// 根据欧拉角生成单位四元数
    /// </summary>
    /// <param name="euler"></param>
    /// <returns>单位四元数</returns>
    public static Quaternion Eulers(Vector3 eulers)
    {
        return Eulers(eulers.x, eulers.y, eulers.z);
    }

    /// <summary>
    /// 根据欧拉角生成单位四元数
    /// </summary>
    /// <param name="eulerX"></param>
    /// <param name="eulerY"></param>
    /// <param name="eulerZ"></param>
    /// <returns></returns>
    public static Quaternion Eulers(float eulerX, float eulerY, float eulerZ)
    {
        return Quaternion.AngleAxis(eulerY, Vector3.up) *
               Quaternion.AngleAxis(eulerX, Vector3.right) *
               Quaternion.AngleAxis(eulerZ, Vector3.forward); 
    }

    /// <summary>
    /// 根据旋转轴和旋转角生成单位四元数
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="axisNormal"></param>
    /// <returns>单位四元数</returns>
    public static Quaternion AngleAxis(float angle, Vector3 axisNormal)
    {
        float radian = MathS.DegToRad * angle * 0.5f;
        float real = MathS.Cos(radian);
        Vector3 imag = MathS.Sin(radian) * axisNormal;
        return new Quaternion(imag.x, imag.y, imag.z, real);
    }
    
    /// <summary>
    /// 旋转单位向量From到另外一个单位向量To
    /// </summary>
    /// <param name="from">单位向量</param>
    /// <param name="to">单位向量</param>
    /// <returns>单位四元数</returns>
    public static Quaternion FromTo(Vector3 from, Vector3 to)
    {
        Quaternion uq = Quaternion.identity;
        float squar = 2 * (1 + Vector3.Dot(from, to));
        if (MathS.Abs(squar) <= Threshold) 
        {
            Vector3 perp = from.Perpendicular().normalized;
            uq = new Quaternion(perp.x, perp.y, perp.z, 0);
        }
        else
        {
            float root = MathS.Sqrt(squar);
            Vector3 imag = from.Cross(to) / root;
            float real = root * 0.5f;
            uq = new Quaternion(imag.x, imag.y, imag.z, real);
        }
        return uq;
    }

    /// <summary>
    /// 线性插值
    /// </summary>
    /// <param name="q1"></param>
    /// <param name="q2"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Quaternion Lerp(Quaternion q1, Quaternion q2, float t)
    {
        t = MathS.Clamp(t, 0, 1);
        return q1 * (1 - t) + q2 * t;
    }

    /// <summary>
    /// 球面插值
    /// </summary>
    /// <param name="q1">单位四元数</param>
    /// <param name="q2">单位四元数</param>
    /// <param name="t">0--1</param>
    /// <returns>单位四元数</returns>
    public static Quaternion Slerp(Quaternion q1, Quaternion q2, float t)
    {
        Quaternion uq = q1;
        t = MathS.Clamp(t, 0, 1);
        float dot = Quaternion.Dot(q1, q2);
        if ((1 - dot) > Threshold)
        {
            float radian = MathS.Acos(dot);
            if (radian < 0) radian = MathS.PI - radian;
            float sin = MathS.Sin(radian);
            float t1 = MathS.Sin((1 - t) * radian) / sin;
            float t2 = MathS.Sin(t * radian) / sin;
            uq = q1 * t1 + q2 * t2;
        }
        return uq;
    }

    public static float Dot(Quaternion q1, Quaternion q2)
    {
        return q1.x * q2.x + q1.y * q2.y + q1.z * q2.z + q1.w * q2.w;
    }

    public static Quaternion operator *(Quaternion q, float f)
    {
        return new Quaternion(q.x * f, q.y * f, q.z * f, q.w * f);
    }

    /// <summary>
    /// 使用单位四元数旋转向量
    /// </summary>
    /// <param name="q">单位四元数</param>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 operator *(Quaternion q, Vector3 v)
    {
        Vector4 v4 = q.ToMatrix() * new Vector4(v.x, v.y, v.z, 1);
        return new Vector3(v4.x, v4.y, v4.z);
    }

    /// <summary>
    /// 使用单位四元数旋转向量
    /// </summary>
    /// <param name="q">单位四元数</param>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector4 operator *(Quaternion q, Vector4 v)
    {
        return q.ToMatrix() * v;
    }

    public static Quaternion operator *(Quaternion q1, Quaternion q2)
    {
        return new Quaternion(
            q1.y * q2.z - q1.z * q2.y + q1.x * q2.w + q1.w * q2.x,
            q1.z * q2.x - q1.x * q2.z + q1.y * q2.w + q1.w * q2.y,
            q1.x * q2.y - q1.y * q2.x + q1.z * q2.w + q1.w * q2.z,
            q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z
        );
    }

    public static Quaternion operator +(Quaternion q1, Quaternion q2)
    {
        return new Quaternion(
            q1.x + q2.x,
            q1.y + q2.y,
            q1.z + q2.z,
            q1.w + q2.w
        );
    }

    public static Quaternion identity
    {
        get
        {
            return new Quaternion(0, 0, 0, 1);
        }
    }
}
