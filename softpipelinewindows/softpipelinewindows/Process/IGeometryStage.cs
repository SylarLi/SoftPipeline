public interface IGeometryStage
{
    ITriangle[] Process(IDrawCall drawCall, ICamera camera);
}
