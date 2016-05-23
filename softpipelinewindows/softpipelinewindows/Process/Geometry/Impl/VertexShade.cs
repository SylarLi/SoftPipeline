public class VertexShade : IVertexShade
{
    private Matrix mMVP;

    public Vector4 Process(Vector4 vertex)
    {
        return MVP * vertex;
    }

    public Matrix MVP
    {
        get
        {
            return mMVP;
        }
        set
        {
            mMVP = value;
        }
    }
}
