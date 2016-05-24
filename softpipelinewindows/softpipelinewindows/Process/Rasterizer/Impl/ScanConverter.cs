using System.Collections.Generic;

public class ScanConverter : IScanConverter
{
    public int[][] ConvexFill(int[][] points)
    {
        // edge table
        EdgeTable et = new EdgeTable();
        int topx1 = -1, topx2 = -1, topy = -1;
        for (int pIndex = 0, len = points.GetLength(0); pIndex < len; pIndex++)
        {
            int[] p1 = points[pIndex];
            int[] p2 = points[pIndex + 1 >= len ? 0 : pIndex + 1];
            if (p1[1] != p2[1])
            {
                int yMin = 0;
                EdgeNode node = new EdgeNode();
                if (p1[1] > p2[1]) 
                {
                    yMin = p2[1];
                    node.yMax = p1[1];
                    node.xMin = p2[0];
                }
                else 
                {
                    yMin = p1[1];
                    node.yMax = p2[1];
                    node.xMin = p1[0];
                }
                node.m = (float)(p1[0] - p2[0]) / (p1[1] - p2[1]);
                et.AddNode(yMin, node);
            }
            else
            {
                // 记录top horizontal edge
                // 这种简单判断topEdge的方式只对Convex有效
                if (p1[1] > topy)
                {
                    topy = p1[1];
                    if (p1[0] > p2[0])
                    {
                        topx1 = p2[0];
                        topx2 = p1[0];
                    }
                    else
                    {
                        topx1 = p1[0];
                        topx2 = p2[0];
                    }
                }
                if (pIndex == len - 1 &&
                    topy < points[1][1])
                {
                    topy = -1;
                }
            }
        }
        
        // active edge table
        EdgeBucket aet = new EdgeBucket();

        if (et.Count == 0) return null;
        else
        {
            // loop
            Queue<int[]> result = new Queue<int[]>();
            while (et.Count > 0 || aet.Count > 0)
            {
                if (aet.Count == 0)
                {
                    aet.yMin = et[0].yMin;
                    et[0].ForEach((EdgeNode each) => aet.AddNode(each));
                    et.RemoveAt(0);
                }
                else
                {
                    if (et.Count > 0 &&
                        aet.yMin == et[0].yMin)
                    {
                        et[0].ForEach((EdgeNode each) => aet.AddNode(each));
                        et.RemoveAt(0);
                    }
                }
                for (int aIndex = aet.Count - 1; aIndex >= 0; aIndex--)
                {
                    if (aet[aIndex].yMax <= aet.yMin)
                    {
                        aet.RemoveAt(aIndex);
                    }
                }
                for (int aIndex = 0; aIndex < aet.Count; aIndex++)
                {
                    if (aIndex % 2 == 0 && 
                        aIndex < aet.Count - 1)
                    {
                        float left = aet[aIndex].xMin;
                        float right = aet[aIndex + 1].xMin;
                        if (topy == -1 || aet.yMin != topy || left >= topx2 || right <= topx1)
                        {
                            // 以上判断是为了排除top horizontal edge
                            int ileft = MathS.Floor(left);
                            int iright = MathS.Floor(right);
                            for (int ixMin = ileft; ixMin <= iright; ixMin += 1)
                            {
                                if (ixMin >= left && ixMin < right)
                                {
                                    // 以上判断是为了排除像素点刚好在rightEdge的情况
                                    result.Enqueue(new int[] { ixMin, aet.yMin });
                                }
                            }
                        }
                        aet[aIndex].xMin += aet[aIndex].m;
                        aet[aIndex + 1].xMin += aet[aIndex + 1].m;
                        if (aet[aIndex].xMin > aet[aIndex + 1].xMin)
                        {
                            EdgeNode temp = aet[aIndex];
                            aet[aIndex] = aet[aIndex + 1];
                            aet[aIndex + 1] = temp;
                        }
                    }
                }
                aet.yMin += 1;
            }
            return result.ToArray();
        }
    }

    private class EdgeNode
    {
        public int yMax;
        public float xMin;
        public float m;
    }

    private class EdgeBucket : List<EdgeNode>
    {
        public int yMin;

        public EdgeBucket()
        {
            yMin = 0;
        }

        public EdgeBucket(int yMin)
        {
            this.yMin = yMin;
        }

        public void AddNode(EdgeNode node)
        {
            if (Count == 0 ||
                node.xMin >= this[Count - 1].xMin)
            {
                Add(node);
            }
            else
            {
                for (int nIndex = 0; nIndex < Count; nIndex++)
                {
                    float cxMin = this[nIndex].xMin;
                    if (node.xMin < cxMin)
                    {
                        Insert(nIndex, node);
                        break;
                    }
                }
            }
        }

        public void RemoveNode(EdgeNode node)
        {
            this.Remove(node);
        }
    }

    private class EdgeTable : List<EdgeBucket>
    {
        public void AddNode(int yMin, EdgeNode node)
        {
            if (Count == 0 ||
                yMin > this[Count - 1].yMin)
            {
                EdgeBucket bucket = new EdgeBucket(yMin);
                bucket.AddNode(node);
                Add(bucket);
            }
            else
            {
                for (int tIndex = 0; tIndex < Count; tIndex++)
                {
                    int cyMin = this[tIndex].yMin;
                    if (yMin < cyMin)
                    {
                        EdgeBucket bucket = new EdgeBucket(yMin);
                        bucket.AddNode(node);
                        Insert(tIndex, bucket);
                        break;
                    }
                    else if (yMin == cyMin)
                    {
                        this[tIndex].AddNode(node);
                        break;
                    }
                }
            }
        }

        public void RemoveNode(int yMin, EdgeNode node)
        {
            for (int tIndex = 0; tIndex < Count; tIndex++)
            {
                if (this[tIndex].yMin == yMin)
                {
                    this[tIndex].RemoveNode(node);
                    break;
                }
            }
        }
    }
}
