using System.Collections.Generic;

public class GeometryStage : IGeometryStage
{
    private IVertexShade vertexShade;

    private IPrimitiveAssemble primitiveAssemble;

    private IClip clip;

    public GeometryStage()
    {
        vertexShade = new VertexShade();
        primitiveAssemble = new PrimitiveAssemble();
        clip = new Clip();
    }

    public ITriangle[] Process(IDrawCall drawCall, ICamera camera)
    {
        Queue<ITriangle> triangles = new Queue<ITriangle>();

        // vertex shade
        vertexShade.MVP = camera.P * camera.V * drawCall.M;
        Vector4[] vertices = drawCall.vertices;

        for (int vIndex = 0; vIndex < vertices.Length; vIndex++)
        {
            vertices[vIndex] = vertexShade.Process(vertices[vIndex]);
        }

        // primitive assembly
        ITriangle[] primitives = primitiveAssemble.Process(drawCall);

        // clipping
        for (int pIndex = 0; pIndex < primitives.Length; pIndex++)
        {
            ITriangle[] ts = clip.Process(primitives[pIndex]);
            if (ts != null)
            {
                foreach (ITriangle t in ts)
                {
                    triangles.Enqueue(t);
                }
            }
        }

        return triangles.ToArray();
    }
}
