using System.Collections.Generic;

public class GeometryStage : IGeometryStage
{
    private IVertexShade vertexShade;

    private IPrimitiveAssemble primitiveAssemble;

    private IClip clip;

    private ICull cull;

    public GeometryStage()
    {
        vertexShade = new VertexShade();
        primitiveAssemble = new PrimitiveAssemble();
        clip = new Clip();
        cull = new Cull();
    }

    public ITriangle[] Process(IDrawCall drawCall, ICamera camera)
    {
        // vertex shade
        vertexShade.MVP = camera.P * camera.V * drawCall.M;
        vertexShade.N = (camera.V * drawCall.M).Inverse().Transpose().Minor(3, 3);
        Vector4[] vertices = drawCall.vertices;
        Vector3[] normals = drawCall.normals;
        IVertexOutputData[] outputs = new VertexOutputData[vertices.Length];
        for (int vIndex = 0; vIndex < vertices.Length; vIndex++)
        {
            IVertexInputData input = new VertexInputData();
            input.vertex = vertices[vIndex];
            input.normal = normals[vIndex];
            outputs[vIndex] = vertexShade.Process(input);
        }

        // primitive assembly
        ITriangle[] primitives = primitiveAssemble.Process(outputs, drawCall.indices);

        Queue<ITriangle> triangles = new Queue<ITriangle>();
        for (int pIndex = 0; pIndex < primitives.Length; pIndex++)
        {
            // back-face culling
            ITriangle t = cull.Process(primitives[pIndex]);
            if (t != null)
            {
                // clipping
                ITriangle[] ts = clip.Process(t);
                if (ts != null)
                {
                    foreach (ITriangle each in ts)
                    {
                        triangles.Enqueue(each);
                    }
                }
            }
        }
        return triangles.ToArray();
    }
}
