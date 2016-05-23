public interface IVertexShade
{
    Vector4 Process(Vector4 vertex);

    Matrix MVP { get; set; }
}
