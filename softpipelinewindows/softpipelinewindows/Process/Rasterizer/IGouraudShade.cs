public interface IGouraudShade
{
    /// <summary>
    /// 这儿只插值求屏幕坐标
    /// </summary>
    /// <param name="clip">多边形顶点(clip space)</param>
    /// <param name="screen">多边形顶点(screen space)</param>
    /// <param name="pixel">Scan-Line扫描之后的像素xy坐标</param>
    /// <returns>插值点</returns>
    Vector4 Triangle(Vector4[] clips, Vector2[] screens, int[] pixel);
}
