public class Cull : ICull
{
    public ITriangle Process(ITriangle triangle)
    {
        IVertexOutputData[] points = triangle.points;
        if (points[0].viewNormal.z >= 0)
        {
            return null;
        }
        return triangle;
    }
}
