using System;
using System.Collections.Generic;

public class RasterizerStage : IRasterizerStage
{
    private int mScreenWidth;

    private int mScreenHeight;

    private IScanConverter scan;

    private IGouraudShade gour;

    private IFragmentShade fragShade;

    public RasterizerStage()
    {
        scan = new ScanConverter();
        gour = new GouraudShade();
        fragShade = new FragmentShade();
    }

    public IFragment[] Process(ITriangle triangle)
    {
        IVertexOutputData[] clips = triangle.points;
        Vector2[] screens = Array.ConvertAll<IVertexOutputData, Vector2>(
            clips,
            (IVertexOutputData each) => new Vector2((each.clip.x / each.clip.w * 0.5f + 0.5f) * screenWidth, (each.clip.y / each.clip.w * 0.5f + 0.5f) * screenHeight)
        );
        int[][] iscreens = Array.ConvertAll<Vector2, int[]>(
            screens,
            (Vector2 each) => new int[] { MathS.Clamp((int)each.x, 0, mScreenWidth - 1), MathS.Clamp((int)each.y, 0, mScreenHeight - 1) }
        );
        int[][] pixels = scan.ConvexFill(iscreens);
        if (pixels != null)
        {
            IVertexOutputData[] v2fs = Array.ConvertAll<int[], IVertexOutputData>(
                pixels,
                (int[] each) => gour.Triangle(clips, screens, each)
            );
            IFragment[] frags = new Fragment[v2fs.Length];
            for (int pIndex = 0; pIndex < v2fs.Length; pIndex++)
            {
                IFragment frag = new Fragment();
                frag.pixel = pixels[pIndex];
                frag.color = fragShade.Process(v2fs[pIndex]);
                frags[pIndex] = frag;
            }
            return frags;
        }
        return null;
    }

    public int screenWidth
    {
        get
        {
            return mScreenWidth;
        }
        set
        {
            mScreenWidth = value;
        }
    }

    public int screenHeight
    {
        get
        {
            return mScreenHeight;
        }
        set
        {
            mScreenHeight = value;
        }
    }
}
