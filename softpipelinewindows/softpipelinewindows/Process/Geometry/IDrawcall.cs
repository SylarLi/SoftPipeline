public interface IDrawCall
{
    Vector4[] vertices { get; set; }

    int[] indices { get; set; }

    Matrix M { get; set; }
}
