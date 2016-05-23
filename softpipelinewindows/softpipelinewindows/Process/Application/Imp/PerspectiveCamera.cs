public class PerspectiveCamera : Camera, IPerspectiveCamera
{
    private float mFov;

    public PerspectiveCamera() : base()
    { 
        
    }

    public float fov
    {
        get
        {
            return mFov;
        }
        set
        {
            mFov = value;
        }
    }

    public override Matrix P
    {
        get
        {
            return TransformUtil.GetPersp(fov, aspect, near, far);
        }
    }
}
