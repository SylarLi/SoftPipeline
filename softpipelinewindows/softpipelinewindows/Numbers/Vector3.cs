public struct Vector3
{
    private const float Threshold = 1e-6f;

    public float x;

    public float y;

    public float z;

    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override string ToString()
    {
        return string.Format("Vector3({0}, {1}, {2})", x, y, z);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return this == (Vector3)obj;
    }

    public float[] ToArray()
    {
        return new float[] { x, y, z };
    }

    public int size
    {
        get
        {
            return 3;
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
            }
        } 
    }

    public void Normalize()
    {
        float len = magnitude;
        if (len <= Threshold)
        {
            x = y = z = 0;
        }
        else
        {
            x /= len;
            y /= len;
            z /= len;
        }
    }

    public float Dot(Vector3 v)
    {
        return Vector3.Dot(this, v);
    }

    public Vector3 Cross(Vector3 v)
    {
        return Vector3.Cross(this, v);
    }

    public Vector3 Perpendicular()
    {
        float ax = MathS.Abs(x);
        float ay = MathS.Abs(y);
        float az = MathS.Abs(z);
        if (ax < ay && ax < az)
        {
            return new Vector3(0, -z, y);
        }
        else if (ay < ax && ay < az)
        {
            return new Vector3(-z, 0, x);
        }
        else
        {
            return new Vector3(-y, x, 0);
        }
    }

    public static Vector3 operator -(Vector3 a)
    {
        return new Vector3(-a.x, -a.y, -a.z);
    }

    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static bool operator !=(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
    }

    public static Vector3 operator *(float d, Vector3 a)
    {
        return a * d;
    }

    public static Vector3 operator *(Vector3 a, float d)
    {
        return new Vector3(a.x * d, a.y * d, a.z * d);
    }

    public static Vector3 operator /(Vector3 a, float d)
    {
        return new Vector3(a.x / d, a.y / d, a.z / d);
    }

    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static bool operator ==(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
    }

    public float sqrMagnitude
    {
        get
        {
            return x * x + y * y + z * z;
        }
    }

    public float magnitude
    {
        get
        {
            return MathS.Sqrt(sqrMagnitude);
        }
    }

    public Vector3 normalized
    {
        get
        {
            Vector3 v = this;
            v.Normalize();
            return v;
        }
    }

    public static Vector3 one
    {
        get
        {
            return new Vector3(1, 1, 1);
        }
    }

    public static Vector3 right
    {
        get
        {
            return new Vector3(1, 0, 0);
        }
    }

    public static Vector3 up
    {
        get
        {
            return new Vector3(0, 1, 0);
        }
    }

    public static Vector3 forward
    {
        get
        {
            return new Vector3(0, 0, 1);
        }
    }

    public static Vector3 zero
    {
        get
        {
            return new Vector3(0, 0, 0);
        }
    }

    public static float Dot(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    }

    public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
    {
        return new Vector3(
            lhs.y * rhs.z - lhs.z * rhs.y, 
            lhs.z * rhs.x - lhs.x * rhs.z, 
            lhs.x * rhs.y - lhs.y * rhs.x
        );
    }

    public static Vector2 Project(Vector3 vec, Vector3 on)
    {
        return vec.Dot(on) / on.sqrMagnitude * on;
    }

    public static float Angle(Vector3 lhs, Vector3 rhs)
    {
        lhs.Normalize();
        rhs.Normalize();
        return MathS.Acos(lhs.Dot(rhs)) * MathS.RadToDeg;
    }

    public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
    {
        t = MathS.Clamp(t, 0, 1);
        return (1 - t) * from + t * to;
    }

    public static Vector3 Slerp(Vector3 from, Vector3 to, float t)
    {
        return Quaternion.Slerp(Quaternion.Eulers(from), Quaternion.Eulers(to), t).ToEulers();
    }
}
