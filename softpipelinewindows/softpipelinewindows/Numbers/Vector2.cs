public struct Vector2
{
    private const float Threshold = 1e-6f;

    public float x;

    public float y;

    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return string.Format("Vector2({0}, {1})", x, y);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return this == (Vector2)obj;
    }

    public float[] ToArray()
    {
        return new float[] { x, y };
    }

    public int size
    {
        get
        {
            return 2;
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
            }
        }
    }

    public void Normalize()
    {
        float len = magnitude;
        if (len <= Threshold)
        {
            x = y = 0;
        }
        else
        {
            x /= len;
            y /= len;
        }
    }

    public float Dot(Vector2 v)
    {
        return Vector2.Dot(this, v);
    }

    public float Cross(Vector2 v)
    {
        return Vector2.Cross(this, v);
    }

    public static Vector2 operator -(Vector2 a)
    {
        return new Vector2(-a.x, -a.y);
    }

    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y);
    }

    public static bool operator ==(Vector2 lhs, Vector2 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(Vector2 lhs, Vector2 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public static Vector2 operator *(float d, Vector2 a)
    {
        return a * d;
    }

    public static Vector2 operator *(Vector2 a, float d)
    {
        return new Vector2(a.x * d, a.y * d);
    }

    public static Vector2 operator /(Vector2 a, float d)
    {
        return new Vector2(a.x / d, a.y / d);
    }

    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }

    public static implicit operator Vector3(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }

    public static implicit operator Vector2(Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public float sqrMagnitude
    {
        get
        {
            return x * x + y * y;
        }
    }

    public float magnitude
    {
        get
        {
            return MathS.Sqrt(sqrMagnitude);
        }
    }

    public Vector2 normalized
    {
        get
        {
            Vector2 v = this;
            v.Normalize();
            return v;
        }
    }

    public static Vector2 one
    {
        get
        {
            return new Vector2(1, 1);
        }
    }

    public static Vector2 right
    {
        get
        {
            return new Vector2(1, 0);
        }
    }

    public static Vector2 up
    {
        get
        {
            return new Vector2(0, 1);
        }
    }

    public static Vector2 zero
    {
        get
        {
            return new Vector2(0, 0);
        }
    }

    public static float Dot(Vector2 lhs, Vector2 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y;
    }

    public static float Cross(Vector2 lhs, Vector2 rhs)
    {
        return lhs.x * rhs.y - lhs.y * rhs.x;
    }

    public static Vector2 Project(Vector2 vec, Vector2 on)
    {
        return vec.Dot(on) / on.sqrMagnitude * on;
    }

    public static float Angle(Vector2 lhs, Vector2 rhs)
    {
        lhs.Normalize();
        rhs.Normalize();
        return MathS.Acos(lhs.Dot(rhs)) * MathS.RadToDeg;
    }

    public static Vector2 Lerp(Vector2 from, Vector2 to, float t)
    {
        t = MathS.Clamp(t, 0, 1);
        return (1 - t) * from + t * to;
    }
}
