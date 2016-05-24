public class VertexInputData : IVertexInputData
{
    private Vector4 mVertex;

    private Vector3 mNormal;

    public Vector4 vertex
    {
        get
        {
            return mVertex;
        }
        set
        {
            mVertex = value;
        }
    }

    public Vector3 normal
    {
        get
        {
            return mNormal;
        }
        set
        {
            mNormal = value;
        }
    }
}
