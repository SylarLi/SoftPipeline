public class VertexOutputData : IVertexOutputData
{
    private Vector4 mClip;

    private Vector3 mViewNormal;


    public Vector4 clip
    {
        get
        {
            return mClip;
        }
        set
        {
            mClip = value;
        }
    }

    public Vector3 viewNormal
    {
        get
        {
            return mViewNormal;
        }
        set
        {
            mViewNormal = value;
        }
    }
}
