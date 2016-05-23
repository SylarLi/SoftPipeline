public sealed class GeometryUtil
{
    /// <summary>
    /// 线段与平面的交点
    /// </summary>
    /// <param name="line"></param>
    /// <param name="plane"></param>
    /// <param name="intersect"></param>
    /// <returns></returns>
    public static bool LinePlane(Vector3[] line, Plane plane, out Vector3 intersect)
    {
        bool result = false;
        intersect = Vector3.zero;
        if (plane.GetSide(line[0]) != plane.GetSide(line[1]))
        {
            float d0 = plane.Distance(line[0]);
            float d1 = plane.Distance(line[1]);
            float u = d0 / (d0 + d1);
            intersect = line[0] + (line[1] - line[0]) * u;
            result = true;
        }
        return result;
    }

    /// <summary>
    /// 线段与三角形的交点
    /// </summary>
    /// <param name="line"></param>
    /// <param name="triangle"></param>
    /// <param name="intersect"></param>
    /// <returns></returns>
    public static bool LineTriangle(Vector3[] line, Vector3[] triangle, out Vector3 intersect)
    {
        Plane plane = new Plane(triangle[0], triangle[1], triangle[2]);
        bool result = false;
        if (LinePlane(line, plane, out intersect))
        {
            Vector3 c1 = (intersect - triangle[0]).Cross(triangle[1] - triangle[0]);
            Vector3 c2 = (intersect - triangle[1]).Cross(triangle[2] - triangle[1]);
            Vector3 c3 = (intersect - triangle[2]).Cross(triangle[0] - triangle[2]);
            if ((c1.Dot(c2) >= 0) == (c2.Dot(c3) >= 0))
            {
                result = true;
            }
        }
        return result;
    }
}
