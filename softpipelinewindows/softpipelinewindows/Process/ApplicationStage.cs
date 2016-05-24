using System;

public class ApplicationStage : IApplicationStage
{
    public IDrawCall[] Process(IScene scene)
    {
        IDrawCall[] drawCalls = new DrawCall[scene.meshes.Length];
        for (int i = drawCalls.Length - 1; i >= 0; i--)
        {
            IMesh mesh = scene.meshes[i];
            IDrawCall drawCall = new DrawCall();
            drawCall.vertices = Array.ConvertAll<Vector3, Vector4>(mesh.vertices, (Vector3 each) => new Vector4(each.x, each.y, each.z, 1));
            drawCall.normals = new Vector3[mesh.normals.Length];
            mesh.normals.CopyTo(drawCall.normals, 0);
            drawCall.indices = new int[mesh.indices.Length];
            mesh.indices.CopyTo(drawCall.indices, 0);
            drawCall.M = mesh.M;
            drawCalls[i] = drawCall;
        }
        return drawCalls;
    }
}
