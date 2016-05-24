public class GouraudShade : IGouraudShade
{
    public IVertexOutputData Triangle(IVertexOutputData[] clips, Vector2[] screens, int[] pixel)
    {
        Vector2 center = new Vector2(pixel[0], pixel[1]);
        float a = MathS.Abs(Vector2.Cross(screens[1] - screens[0], screens[2] - screens[0]) * 0.5f);
        // 重心法求插值系数
        float a0 = MathS.Abs(Vector2.Cross(screens[1] - screens[0], center - screens[0]) * 0.5f) / a;
        float a1 = MathS.Abs(Vector2.Cross(screens[2] - screens[1], center - screens[1]) * 0.5f) / a;
        float a2 = MathS.Abs(Vector2.Cross(screens[0] - screens[2], center - screens[2]) * 0.5f) / a;
        // 插值
        IVertexOutputData lerp = new VertexOutputData();
        lerp.clip = a0 * clips[0].clip + a1 * clips[1].clip + a2 * clips[2].clip;
        lerp.viewNormal = a0 * clips[0].viewNormal + a1 * clips[1].viewNormal + a2 * clips[2].viewNormal;
        return lerp;
    }
}
