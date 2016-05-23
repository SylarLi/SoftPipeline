public interface IMesh : ITransform
{
    Vector3[] vertices { get; set; }

    int[] indices { get; set; }

    Matrix M { get; }
}
