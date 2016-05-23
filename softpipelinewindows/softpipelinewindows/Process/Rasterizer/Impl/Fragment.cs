﻿public class Fragment : IFragment
{
    private Vector4 mClip;

    private int[] mPixel;

    private Vector4 mColor;

    public Vector4 clip
    {
        get
        {
            return mClip;
        }
        set
        {
            mClip = value;
        }
    }

    public Vector4 color
    {
        get
        {
            return mColor;
        }
        set
        {
            mColor = value;
        }
    }


    public int[] pixel
    {
        get
        {
            return mPixel;
        }
        set
        {
            mPixel = value;
        }
    }

    public override string ToString()
    {
        return string.Format("Frament[{0}, {1}, {2}]", mPixel[0], mPixel[1], mColor);
    }
}
