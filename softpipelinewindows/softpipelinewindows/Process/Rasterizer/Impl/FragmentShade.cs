public class FragmentShade : IFragmentShade
{
    public Vector4 Process(Vector4 clip)
    {
        return new Vector4(1, 0, 0, 1);
    }
}
