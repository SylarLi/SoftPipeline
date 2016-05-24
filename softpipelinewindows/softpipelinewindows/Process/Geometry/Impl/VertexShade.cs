public class VertexShade : IVertexShade
{
    private Matrix mMVP;

    private Matrix mN;

    public IVertexOutputData Process(IVertexInputData input)
    {
        IVertexOutputData output = new VertexOutputData();
        output.clip = MVP * input.vertex;
        output.viewNormal = N * input.normal;
        return output;
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


    public Matrix N
    {
        get
        {
            return mN;
        }
        set
        {
            mN = value;
        }
    }
}
