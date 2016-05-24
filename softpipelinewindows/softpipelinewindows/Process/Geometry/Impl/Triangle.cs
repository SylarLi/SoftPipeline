public class Triangle : ITriangle
{
    private IVertexOutputData[] mPoints;

    public IVertexOutputData[] points
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
