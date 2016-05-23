public class Scene : IScene
{
    private ICamera mCamera;

    private IMesh[] mMeshes;

    public ICamera camera
    {
        get
        {
            return mCamera;
        }
        set
        {
            mCamera = value;
        }
    }

    public IMesh[] meshes
    {
        get
        {
            return mMeshes;
        }
        set
        {
            mMeshes = value;
        }
    }
}
