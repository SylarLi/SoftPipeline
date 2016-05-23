using System;
using System.Collections.Generic;

public class PrimitiveAssemble : IPrimitiveAssemble
{
    public ITriangle[] Process(IDrawCall drawCall)
    {
        Queue<ITriangle> primitives = new Queue<ITriangle>();
        Vector4[] vertices = drawCall.vertices;
        int[] indices = drawCall.indices;
        for (int iIndex = 2; iIndex < indices.Length; iIndex += 3)
        {
            ITriangle t = new Triangle()
            {
                points = new Vector4[]
                    {
                        vertices[indices[iIndex - 2]],
                        vertices[indices[iIndex - 1]],
                        vertices[indices[iIndex]],
                    }
            };
            primitives.Enqueue(t);
        }
        return primitives.ToArray();
    }
}
