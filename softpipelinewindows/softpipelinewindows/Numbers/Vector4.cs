public struct Vector4
{
    private const float Threshold = 1e-6f;

    public float x;

    public float y;

    public float z;

    public float w;

    public Vector4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public override string ToString()
    {
        return string.Format("Vector4({0}, {1}, {2}, {3})", x, y, z, w);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return this == (Vector4)obj;
    }

    public Vector3 ToVector3()
    {
        if (MathS.Abs(w) <= Threshold)
        {
            return new Vector3(x, y, z);
        }
        else
        {
            return new Vector3(x / w, y / w, z / w);
        }
    }

    public float[] ToArray()
    {
        return new float[] { x, y, z, w };
    }

    public int size
    {
        get
        {
            return 4;
        }
    }

    public float this[int index]
    {
        get
        {
            return index >= 0 && index < size ? ToArray()[index] : 0;
        }
        set
        {
            switch (index)
            {
                case 0:
                    {
                        x = value;
                        break;
                    }
                case 1:
                    {
                        y = value;
                        break;
                    }
                case 2:
                    {
                        z = value;
                        break;
                    }
                case 3:
                    {
                        w = value;
                        break;
                    }
            }
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

    public float Dot(Vector4 v)
    {
        return Vector4.Dot(this, v);
    }

    public static Vector4 operator -(Vector4 a)
    {
        return new Vector4(-a.x, -a.y, -a.z, -a.w);
    }

    public static Vector4 operator -(Vector4 a, Vector4 b)
    {
        return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }

    public static bool operator ==(Vector4 lhs, Vector4 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w;
    }

    public static bool operator !=(Vector4 lhs, Vector4 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.w != rhs.w;
    }

    public static Vector4 operator *(float d, Vector4 a)
    {
        return a * d;
    }

    public static Vector4 operator *(Vector4 a, float d)
    {
        return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
    }

    public static Vector4 operator /(Vector4 a, float d)
    {
        return new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);
    }

    public static Vector4 operator +(Vector4 a, Vector4 b)
    {
        return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
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

    public Vector4 normalized
    {
        get
        {
            Vector4 v = this;
            v.Normalize();
            return v;
        }
    }

    public static Vector4 one
    {
        get
        {
            return new Vector4(1, 1, 1, 1);
        }
    }

    public static Vector4 zero
    {
        get
        {
            return new Vector4(0, 0, 0, 0);
        }
    }

    public static Vector4 identity
    {
        get
        {
            return new Vector4(0, 0, 0, 1);
        }
    }

    public static float Dot(Vector4 lhs, Vector4 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;
    }

    public static Vector4 Project(Vector4 vec, Vector4 on)
    {
        return vec.Dot(on) / on.sqrMagnitude * on;
    }

    public static float Angle(Vector4 lhs, Vector4 rhs)
    {
        lhs.Normalize();
        rhs.Normalize();
        return MathS.Acos(lhs.Dot(rhs)) * MathS.RadToDeg;
    }

    public static Vector4 Lerp(Vector4 from, Vector4 to, float t)
    {
        t = MathS.Clamp(t, 0, 1);
        return (1 - t) * from + t * to;
    }
}
