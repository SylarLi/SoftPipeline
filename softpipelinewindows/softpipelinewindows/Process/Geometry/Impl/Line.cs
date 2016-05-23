public class Line : ILine
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
            return PrimitiveType.Line; 
        }
    }

    public override string ToString()
    {
        return string.Format("Line: [{0}, {1}]", points[0], points[1]);
    }
}
