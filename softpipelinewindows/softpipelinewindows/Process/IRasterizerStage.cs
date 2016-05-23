public interface IRasterizerStage
{
    IFragment[] Process(ITriangle triangle);

    /// <summary>
    /// 屏幕像素宽度
    /// </summary>
    int screenWidth { get; set; }

    /// <summary>
    /// 屏幕像素高度
    /// </summary>
    int screenHeight { get; set; }
}
