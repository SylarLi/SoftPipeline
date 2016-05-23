public interface ICamera : ITransform
{
    /// <summary>
    /// near plane
    /// </summary>
    float near { get; set; }

    /// <summary>
    /// far plane
    /// </summary>
    float far { get; set; }

    /// <summary>
    /// width / height
    /// </summary>
    float aspect { get; set; }

    Matrix V { get; }

    Matrix P { get; }
}
