public class Mesh : Transform, IMesh
{
    private Vector3[] mVertices;

    private int[] mIndices;

    public Mesh() : base()
    {

    }

    public Vector3[] vertices
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
            return TransformUtil.GetTRS(position, rotation, scale);
        }
    }
}
