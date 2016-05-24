using System;

/// <summary>
/// 列主序
/// 只能表示 N X N 矩阵
/// </summary>
public class Matrix
{
    private const float Threshold = 1e-6f;

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

    public Matrix Inverse()
    {
        return Matrix.Inverse(this);
    }

    public Matrix Minor(int row, int column)
    {
        return Matrix.Minor(this, row, column);
    }

    public static Matrix operator *(Matrix m, float value)
    {
        Matrix mm = new Matrix(m.size);
        for (int i = 0; i < m.size; i++)
        {
            for (int j = 0; j < m.size; j++)
            {
                mm[i, j] = m[i, j] * value;
            }
        }
        return mm;
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

    public static Matrix Inverse(Matrix m)
    {
        float d = Determinant(m);
        if (d <= Threshold)
        {
            throw new InvalidOperationException();
        }
        return Adjoint(m) * (1 / d);
    }

    public static Matrix Adjoint(Matrix m)
    {
        return Matrix.Transpose(Matrix.Confactor(m));
    }

    public static Matrix Confactor(Matrix m)
    {
        Matrix c = new Matrix(m.size);
        for (int x = 0; x < m.size; x++)
        {
            for (int y = 0; y < m.size; y++)
            {
                c[x, y] = ((x + y) % 2 == 0 ? 1 : -1) * Determinant(Minor(m, x, y));
            }
        }
        return c;
    }

    public static float Determinant(Matrix m)
    {
        float value = 0;
        switch (m.size)
        {
            case 1:
                {
                    value = m[0, 0];
                    break;
                }
            case 2:
                {
                    value = m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
                    break;
                }
            default:
                {
                    for (int y = 0; y < m.size; y++)
                    {
                        value += (y % 2 == 0 ? 1 : -1) * m[0, y] * Determinant(Minor(m, 0, y));
                    }
                    break;
                }
        }
        return value;
    }

    public static Matrix Minor(Matrix m, int row, int column)
    {
        Matrix minor = new Matrix(m.size - 1);
        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < column; y++)
            {
                minor[x, y] = m[x, y];
            }
            for (int y = column + 1; y < m.size; y++)
            {
                minor[x, y - 1] = m[x, y];
            }
        }
        for (int x = row + 1; x < m.size; x++)
        {
            for (int y = 0; y < column; y++)
            {
                minor[x - 1, y] = m[x, y];
            }
            for (int y = column + 1; y < m.size; y++)
            {
                minor[x - 1, y - 1] = m[x, y];
            }
        }
        return minor;
    }
}
