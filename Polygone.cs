namespace geo
{
    public class Polygone
{
    public PointF[] Points { get; set; }

    public Polygone(PointF[] points)
    {
        Points = points;
    }

    public bool Contains(PointF point)
    {
        int n = Points.Length;
        bool result = false;

        for (int i = 0, j = n - 1; i < n; j = i++)
        {
            if (((Points[i].Y > point.Y) != (Points[j].Y > point.Y)) &&
                (point.X < (Points[j].X - Points[i].X) * (point.Y - Points[i].Y) / (Points[j].Y - Points[i].Y) + Points[i].X))
            {
                result = !result;
            }
        }
        return result;
    }
}
}
