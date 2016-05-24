using System;
using System.Collections.Generic;

public class PrimitiveAssemble : IPrimitiveAssemble
{
    public ITriangle[] Process(IVertexOutputData[] vdatas, int[] indices)
    {
        Queue<ITriangle> primitives = new Queue<ITriangle>();
        for (int iIndex = 2; iIndex < indices.Length; iIndex += 3)
        {
            ITriangle t = new Triangle()
            {
                points = new IVertexOutputData[]
                    {
                        vdatas[indices[iIndex - 2]],
                        vdatas[indices[iIndex - 1]],
                        vdatas[indices[iIndex]],
                    }
            };
            primitives.Enqueue(t);
        }
        return primitives.ToArray();
    }
}
