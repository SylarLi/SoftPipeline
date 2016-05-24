public class DrawCall : IDrawCall
{
    private Vector4[] mVertices;

    private Vector3[] mNormals;

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

    public Vector3[] normals
    {
        get
        {
            return mNormals;
        }
        set
        {
            mNormals = value;
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
