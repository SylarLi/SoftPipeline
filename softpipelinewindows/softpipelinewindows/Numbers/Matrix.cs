using System;

/// <summary>
/// 列主序
/// 只能表示 N X N 矩阵
/// </summary>
public class Matrix
{
    private int mSize;

    private float[,] mValues;

    public Matrix(int size)
    {
        mSize = size;
        mValues = new float[mSize, mSize];
        for (int i = mValues.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = mValues.GetLength(1) - 1; j >= 0; j--)
            {
                mValues[i, j] = 0;
            }
        }
    }

    public Matrix(float[,] values)
    {
        mSize = values.GetLength(0);
        if (mSize != values.GetLength(1))
        {
            throw new NotSupportedException();
        }
        mValues = new float[mSize, mSize];
        for (int i = mValues.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = mValues.GetLength(1) - 1; j >= 0; j--)
            {
                mValues[i, j] = values[i, j];
            }
        }
    }

    public Matrix(Vector2[] values)
    {
        mSize = 2;
        if (values.Length != mSize)
        {
            throw new NotSupportedException();
        }
        mValues = new float[mSize, mSize];
        for (int i = mValues.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = mValues.GetLength(1) - 1; j >= 0; j--)
            {
                mValues[i, j] = values[i][j];
            }
        }
    }

    public Matrix(Vector3[] values)
    {
        mSize = 3;
        if (values.Length != mSize)
        {
            throw new NotSupportedException();
        }
        mValues = new float[mSize, mSize];
        for (int i = mValues.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = mValues.GetLength(1) - 1; j >= 0; j--)
            {
                mValues[i, j] = values[i][j];
            }
        }
    }

    public Matrix(Vector4[] values)
    {
        mSize = 4;
        if (values.Length != mSize)
        {
            throw new NotSupportedException();
        }
        mValues = new float[mSize, mSize];
        for (int i = mValues.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = mValues.GetLength(1) - 1; j >= 0; j--)
            {
                mValues[i, j] = values[i][j];
            }
        }
    }

    public int size
    {
        get
        {
            return mSize;
        }
    }

    public float this[int i, int j]
    {
        get
        {
            return mValues[i, j];
        }
        set
        {
            mValues[i, j] = value;
        }
    }

    public Matrix Transpose()
    {
        return Matrix.Transpose(this);
    }

    public static Matrix operator *(Matrix m1, Matrix m2)
    {
        if (m1.size != m2.size)
        {
            throw new NotSupportedException();
        }
        Matrix m = new Matrix(m1.size);
        for (int i = 0; i < m.size; i++)
        {
            for (int j = 0; j < m.size; j++)
            {
                for (int s = 0; s < m.size; s++)
                {
                    m[i, j] += m1[i, s] * m2[s, j];
                }
            }
        }
        return m;
    }

    public static Vector2 operator *(Matrix m, Vector2 v)
    {
        if (m.size != v.size)
        {
            throw new NotSupportedException();
        }
        Vector2 r = Vector2.zero;
        for (int i = 0; i < m.size; i++)
        {
            for (int j = 0; j < m.size; j++)
            {
                r[i] += m[i, j] * v[j];
            }
        }
        return r;
    }

    public static Vector3 operator *(Matrix m, Vector3 v)
    {
        if (m.size != v.size)
        {
            throw new NotSupportedException();
        }
        Vector3 r = Vector3.zero;
        for (int i = 0; i < m.size; i++)
        {
            for (int j = 0; j < m.size; j++)
            {
                r[i] += m[i, j] * v[j];
            }
        }
        return r;
    }

    public static Vector4 operator *(Matrix m, Vector4 v)
    {
        if (m.size != v.size)
        {
            throw new NotSupportedException();
        }
        Vector4 r = Vector4.zero;
        for (int i = 0; i < m.size; i++)
        {
            for (int j = 0; j < m.size; j++)
            {
                r[i] += m[i, j] * v[j];
            }
        }
        return r;
    }

    public static Matrix Transpose(Matrix m)
    {
        Matrix r = new Matrix(m.size);
        for (int i = 0; i < m.size; i++)
        {
            for (int j = 0; j < m.size; j++)
            {
                r[i, j] = m[j, i];
            }
        }
        return r;
    }
}
