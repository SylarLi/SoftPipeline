public class Triangle : ITriangle
{
    private Vector4[] mPoints;

    public Vector4[] points
    {
        get
        {
            return mPoints;
        }
        set
        {
            mPoints = value;
        }
    }

    public PrimitiveType type
    {
        get
        {
            return PrimitiveType.Triangle;
        }
    }

    public override string ToString()
    {
        return string.Format("Triangle[{0}, {1}, {2}]", mPoints[0], mPoints[1], mPoints[2]);
    }
}
