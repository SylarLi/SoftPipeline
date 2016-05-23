public interface IFragmentShade
{
    /// <summary>
    /// 片元着色
    /// </summary>
    /// <param name="frag">本来应该时VertexShade的输出数据，为了简便仅传递了clip space coordinate</param>
    /// <returns>颜色值RGBA</returns>
    Vector4 Process(Vector4 clip);
}
