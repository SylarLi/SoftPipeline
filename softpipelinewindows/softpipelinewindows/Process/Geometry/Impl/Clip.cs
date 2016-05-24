using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Algorithm: Sutherland-Hodgman
/// </summary>
public class Clip : IClip
{
    private const float Threshold = 1e-6f;

    public ITriangle[] Process(ITriangle triangle)
    {
        return TriangleFun(ClipPolygon(triangle.points));
    }

    public ILine Process(ILine line)
    {
        Vector4[] points = ClipLine(line.points);
        return points == null ? 
            null :
            new Line() { points = points };
    }

    public byte Process(Vector4 point)
    {
        return ClipPoint(point);
    }

    private ITriangle[] TriangleFun(IVertexOutputData[] polygon)
    {
        ITriangle[] result = null;
        if (polygon.Length >= 3)
        {
            result = new Triangle[polygon.Length - 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Triangle()
                {
                    points = new IVertexOutputData[] 
                    { 
                        polygon[0],
                        polygon[i + 1],
                        polygon[i + 2]
                    }
                };
            }
        }
        return result;
    }

    private IVertexOutputData[] ClipPolygon(IVertexOutputData[] polygon)
    {
        for (int planeIndex = 0; planeIndex < 6; planeIndex++)
        {
            List<IVertexOutputData> vdatas = new List<IVertexOutputData>();
            int vi = planeIndex / 2;
            int sign = planeIndex % 2 == 0 ? 1 : -1;
            for (int pointIndex = 0; pointIndex < polygon.Length; pointIndex++)
            {
                int index0 = pointIndex;
                int index1 = pointIndex == polygon.Length - 1 ? 0 : pointIndex + 1;
                IVertexOutputData v0 = polygon[index0];
                IVertexOutputData v1 = polygon[index1];
                Vector4 p0 = v0.clip;
                Vector4 p1 = v1.clip;
                Vector3 n0 = v0.viewNormal;
                Vector3 n1 = v1.viewNormal;
                float d0 = (p0[vi] + p0.w * sign) * p0.w;
                float d1 = (p1[vi] + p1.w * sign) * p1.w;
                float ds = d0 * d1;
                if (ds < 0)
                {
                    // 边与裁剪面相交
                    float u = d0 / (d0 - d1);
                    Vector4 p = p0 + (p1 - p0) * u;
                    Vector3 n = n0 + (n1 - n0) * u;
                    if (sign * d0 > 0)
                    {
                        vdatas.Add(v0);
                    }
                    IVertexOutputData data = new VertexOutputData();
                    data.clip = p;
                    data.viewNormal = n;
                    vdatas.Add(data);
                }
                else if (MathS.Abs(d0) < Threshold)
                {
                    // p0点在裁剪面上
                    vdatas.Add(v0);
                }
                else if (sign * d0 > 0 && sign * d1 >= 0)
                {
                    // 边完全位于裁剪面之内
                    vdatas.Add(v0);
                }
            }
            polygon = vdatas.ToArray();
        }
        return polygon;
    }

    private Vector4[] ClipLine(Vector4[] line)
    {
        Vector4[] result = null;
        Vector4 p1 = line[0];
        Vector4 p2 = line[1];
        byte b1 = Process(p1);
        byte b2 = Process(p2);
        if (b1 == 0 && b2 == 0)
        {
            result = line;
        }
        else if ((b1 & b2) == 0)
        {
            Vector4[] points = new Vector4[2];
            if (b1 == 0) points[0] = p1;
            else if (b2 == 0) points[1] = p2;
            byte b = (byte)(b1 ^ b2);
            for (int i = 0; i < 6; i++)
            {
                if ((b & (1 << i)) > 0)
                {
                    int vi = i / 2;
                    int sign = i % 2 == 0 ? 1 : -1;
                    float d1 = (p1[vi] + p1.w * sign) * p1.w;
                    float d2 = (p2[vi] + p2.w * sign) * p2.w;
                    float u = d1 / (d1 - d2);
                    Vector4 p = p1 + (p2 - p1) * u;
                    if (Process(p) == 0)
                    {
                        if (b1 != 0)
                        {
                            b1 = 0;
                            points[0] = p;
                        }
                        else if (b2 != 0)
                        {
                            b2 = 0;
                            points[1] = p;
                        }
                        if (b1 == 0 && b2 == 0)
                        {
                            result = points;
                            break;
                        }
                    }
                }
            }
        }
        return result;
    }

    private byte ClipPoint(Vector4 point)
    {
        byte result = 0;
        if (point.x < -point.w) result |= 1;
        else if (point.x > point.w) result |= 2;
        if (point.y < -point.w) result |= 4;
        else if (point.y > point.w) result |= 8;
        if (point.z < -point.w) result |= 16;
        else if (point.z > point.w) result |= 32;
        return result;
    }
}
