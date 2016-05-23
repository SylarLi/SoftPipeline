public interface IScanConverter
{
    /// <summary>
    /// https://www2.cs.uic.edu/~jbell/CourseNotes/ComputerGraphics/PolygonFilling.html
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    int[][] ConvexFill(int[][] points);
}
