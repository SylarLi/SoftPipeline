public abstract class Camera : Transform, ICamera
{
    private float mNear;

    private float mFar;

    private float mAspect;

    public Camera() : base()
    {
        
    }

    public float near
    {
        get
        {
            return mNear;
        }
        set
        {
            mNear = value;
        }
    }

    public float far
    {
        get
        {
            return mFar;
        }
        set
        {
            mFar = value;
        }
    }

    public float aspect
    {
        get
        {
            return mAspect;
        }
        set
        {
            mAspect = value;
        }
    }

    public Matrix V
    {
        get 
        {
            return TransformUtil.GetV(position, rotation);
        }
    }

    public virtual Matrix P
    {
        get 
        {
            throw new System.NotImplementedException();
        }
    }
}
