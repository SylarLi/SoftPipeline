public interface IClip
{
    /// <summary>
    /// 裁剪三角形
    /// </summary>
    /// <param name="triangle"></param>
    /// <returns></returns>
    ITriangle[] Process(ITriangle triangle);

    /// <summary>
    /// 裁剪线段
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    ILine Process(ILine line);

    /// <summary>
    /// 检测是否剔除点(w必须为正值)
    /// </summary>
    /// <param name="point"></param>
    /// <returns>非0时剔除，低位六位为1时分别代表(x > w)(x < -w)(y > w)(y < -w)(z > w)(z < -w)</returns>
    byte Process(Vector4 point);
}
