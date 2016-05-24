public interface IVertexShade
{
    IVertexOutputData Process(IVertexInputData input);

    Matrix MVP { get; set; }

    Matrix N { get; set; }
}
