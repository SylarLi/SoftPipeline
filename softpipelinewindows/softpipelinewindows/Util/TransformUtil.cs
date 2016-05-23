public sealed class TransformUtil
{
    /// <summary>
    /// 标准正交相机投影矩阵
    /// </summary>
    /// <param name="size">height / 2</param>
    /// <param name="aspect">width / height</param>
    /// <param name="near"></param>
    /// <param name="far"></param>
    /// <returns></returns>
    public static Matrix GetOrtho(float size, float aspect, float near, float far)
    {
        return new Matrix(new float[4, 4] 
        {
            { 1 / (size * aspect), 0, 0, 0 },
            { 0, 1 / size, 0, 0 },
            { 0, 0, 2 / (far - near), -(far + near) / (far - near) },
            { 0, 0, 0, 1 }
        });
    }

    /// <summary>
    /// 标准透视相机投影矩阵
    /// </summary>
    /// <param name="fov">arctan2(height / 2, near) * MathS.RadToDeg</param>
    /// <param name="aspect">width / height</param>
    /// <param name="near"></param>
    /// <param name="far"></param>
    /// <returns></returns>
    public static Matrix GetPersp(float fov, float aspect, float near, float far)
    {
        float h2 = MathS.Tan(fov * MathS.DegToRad) * near;
        float w2 = h2 * aspect;
        return new Matrix(new float[4, 4]
        {
            { near / w2, 0, 0, 0 },
            { 0, near / h2, 0, 0 },
            { 0, 0, (far + near) / (far - near), -2 * far * near / (far - near) },
            { 0, 0, 1, 0 }
        });
    }

    public static Matrix GetT(Vector3 T)
    {
        return new Matrix(new float[4, 4] 
        {
            { 1, 0, 0, T.x },
            { 0, 1, 0, T.y },
            { 0, 0, 1, T.z },
            { 0, 0, 0, 1 }
        });
    }

    public static Matrix GetR(Quaternion R)
    {
        return R.ToMatrix();
    }

    public static Matrix GetS(Vector3 S)
    {
        return new Matrix(new float[4, 4] 
        {
            { S.x, 0, 0, 0 },
            { 0, S.y, 0, 0 },
            { 0, 0, S.z, 0 },
            { 0, 0, 0, 1 }
        });
    }

    public static Matrix GetTRS(Vector3 T, Quaternion R, Vector3 S)
    {
        return GetT(T) * GetR(R) * GetS(S);
    }

    public static Matrix GetV(Vector3 T, Quaternion R)
    {
        return GetR(new Quaternion(-R.x, -R.y, -R.z, R.w)) * GetT(-T);
    }
}
