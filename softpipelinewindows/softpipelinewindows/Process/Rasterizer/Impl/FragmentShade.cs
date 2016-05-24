public class FragmentShade : IFragmentShade
{
    public Vector4 Process(IVertexOutputData v2f)
    {
        return new Vector4(1, 0, 0, 1);
    }
}
