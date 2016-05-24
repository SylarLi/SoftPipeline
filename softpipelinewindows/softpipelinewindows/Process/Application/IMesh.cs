public interface IMesh : ITransform
{
    Vector3[] vertices { get; set; }

    Vector3[] normals { get; set; }

    int[] indices { get; set; }

    Matrix M { get; }
}
