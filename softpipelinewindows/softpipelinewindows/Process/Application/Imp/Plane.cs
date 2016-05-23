/// <summary>
/// 平面公式：ax + by + cz = d
/// (a, b, c)为单位法线, d代表原点到平面的距离
/// </summary>
public class Plane
{
    private const float Threshold = 1e-6f;

    private Vector3 mNormal;

    private float md;

    public Plane(Vector3 normal, float d)
    {
        mNormal = normal;
        md = d;
    }

    public Plane(Vector3 normal, Vector3 p)
    {
        mNormal = normal;
        md = mNormal.Dot(p);
    }

    public Plane(Vector3 a, Vector3 b, Vector3 c)
    {
        mNormal = (b - a).Cross(c - b).normalized;
        md = mNormal.Dot(a);
    }

    public Vector3 normal
    {
        get
        {
            return mNormal;
        }
    }

    public bool GetSide(Vector3 point)
    {
        return (point - mNormal * md).Dot(mNormal) >= 0;
    }

    public bool Contains(Vector3 point)
    {
        return Distance(point) <= Threshold;
    }

    public float Distance(Vector3 point)
    {
        return MathS.Abs(point.Dot(mNormal) - md);
    }
}
