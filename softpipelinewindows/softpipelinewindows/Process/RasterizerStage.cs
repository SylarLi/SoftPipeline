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
        Vector4[] clips = triangle.points;
        Vector2[] screens = Array.ConvertAll<Vector4, Vector2>(
            clips,
            (Vector4 each) => new Vector2((each.x / each.w * 0.5f + 0.5f) * screenWidth, (each.y / each.w * 0.5f + 0.5f) * screenHeight)
        );
        int[][] iscreens = Array.ConvertAll<Vector2, int[]>(
            screens, 
            (Vector2 each) => new int[] { MathS.Min(mScreenWidth - 1, (int)each.x), MathS.Min(mScreenHeight - 1, (int)each.y) }
        );
        int[][] pixels = scan.ConvexFill(iscreens);
        if (pixels != null)
        {
            Vector4[] points = Array.ConvertAll<int[], Vector4>(
                pixels,
                (int[] each) => gour.Triangle(clips, screens, each)
            );
            IFragment[] frags = new Fragment[points.Length];
            for (int pIndex = 0; pIndex < points.Length; pIndex++)
            {
                IFragment frag = new Fragment();
                frag.clip = points[pIndex];
                frag.pixel = pixels[pIndex];
                frag.color = fragShade.Process(points[pIndex]);
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
