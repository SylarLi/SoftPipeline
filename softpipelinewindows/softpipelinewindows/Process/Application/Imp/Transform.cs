public abstract class Transform : ITransform
{
    private Vector3 mPosition;

    private Quaternion mRotation;

    private Vector3 mScale;

    public Transform()
    {
        mPosition = Vector3.zero;
        mRotation = Quaternion.identity;
        mScale = Vector3.one;
    }

    public Vector3 position
    {
        get
        {
            return mPosition;
        }
        set
        {
            mPosition = value;
        }
    }

    public Quaternion rotation
    {
        get
        {
            return mRotation;
        }
        set
        {
            mRotation = value;
        }
    }

    public Vector3 scale
    {
        get
        {
            return mScale;
        }
        set
        {
            mScale = value;
        }
    }
}
