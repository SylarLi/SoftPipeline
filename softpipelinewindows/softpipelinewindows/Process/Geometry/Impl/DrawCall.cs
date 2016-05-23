public class DrawCall : IDrawCall
{
    private Vector4[] mVertices;

    private int[] mIndices;

    private Matrix mM;

    public Vector4[] vertices
    {
        get
        {
            return mVertices;
        }
        set
        {
            mVertices = value;
        }
    }

    public int[] indices
    {
        get
        {
            return mIndices;
        }
        set
        {
            mIndices = value;
        }
    }

    public Matrix M
    {
        get
        {
            return mM;
        }
        set
        {
            mM = value;
        }
    }
}
