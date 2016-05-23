public interface IPerspectiveCamera : ICamera
{
    /// <summary>
    /// arctan2(height / 2, near) * MathS.RadToDeg
    /// </summary>
    float fov { get; set; }
}
