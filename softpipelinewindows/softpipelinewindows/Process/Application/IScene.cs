public interface IScene
{
    ICamera camera { get; set; }

    IMesh[] meshes { get; set; }
}
