using System;
using System.Collections.Generic;

public class Pipeline
{
    private IGeometryStage geometryStage;

    private IRasterizerStage rasterizerStage;

    private FrameBuffer mFrameBuffer;

    public Pipeline(int screenWidth, int screenHeight)
    {
        geometryStage = new GeometryStage();
        rasterizerStage = new RasterizerStage();
        rasterizerStage.screenWidth = screenWidth;
        rasterizerStage.screenHeight = screenHeight;
        mFrameBuffer = new FrameBuffer();
        mFrameBuffer.renderBuffer = new Vector4[screenWidth, screenHeight];
        mFrameBuffer.displayBuffer = new Vector4[screenWidth, screenHeight];
    }

    public Vector4[,] Process(IDrawCall[] drawCalls, ICamera camera)
    {
        // Clear
        for (int y = 0; y < mFrameBuffer.renderBuffer.GetLength(1); y++)
        {
            for (int x = 0; x < mFrameBuffer.renderBuffer.GetLength(0); x++)
            {
                mFrameBuffer.renderBuffer[x, y] = Vector4.identity;
            }
        }
        // Render
        for (int dcIndex = 0; dcIndex < drawCalls.Length; dcIndex++)
        {
            ITriangle[] triangles = geometryStage.Process(drawCalls[dcIndex], camera);
            for (int tIndex = 0; tIndex < triangles.Length; tIndex++)
            {
                IFragment[] fragments = rasterizerStage.Process(triangles[tIndex]);
                if (fragments != null)
                {
                    for (int fIndex = 0; fIndex < fragments.Length; fIndex++)
                    {
                        IFragment fragment = fragments[fIndex];
                        mFrameBuffer.renderBuffer[fragment.pixel[0], fragment.pixel[1]] = fragment.color;
                    }
                }
            }
        }
        return mFrameBuffer.renderBuffer;
    }
}
