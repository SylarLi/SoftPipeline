public class OrthoGraphicCamera : Camera, IOrthoGraphicCamera
{
    private float mSize;

    public OrthoGraphicCamera() : base()
    { 
        
    }

    public float size
    {
        get
        {
            return mSize;
        }
        set
        {
            mSize = value;
        }
    }

    public override Matrix P
    {
        get
        {
            return TransformUtil.GetOrtho(size, aspect, near, far);
        }
    }
}
