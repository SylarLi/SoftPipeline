public class Mesh : Transform, IMesh
{
    private Vector3[] mVertices;

    private Vector3[] mNormals;

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
            return TransformUtil.GetTRS(position, rotation, scale);
        }
    }
}
