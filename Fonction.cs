public class Fonction
{
public static List<PointF> GetPointsByEquation(
    PointF center, 
    Func<float, float> equation, 
    float startX, 
    float endX, 
    float scaleFactorX = 5.5f, 
    float scaleFactorY = 20.0f, 
    float step = 0.1f,
    float rotationAngle = 89.5f)
{
    List<PointF> points = new List<PointF>();

    float cosAngle = (float)Math.Cos(rotationAngle);
    float sinAngle = (float)Math.Sin(rotationAngle);

    for (float x = startX; x <= endX; x += step)
    {
        float y = equation(x);

        float scaledX = x * scaleFactorX;
        float scaledY = y * scaleFactorY;

        float rotatedX = scaledX * cosAngle - scaledY * sinAngle;
        float rotatedY = scaledX * sinAngle + scaledY * cosAngle;

        float translatedX = center.X + rotatedX;
        float translatedY = center.Y - rotatedY;
        points.Add(new PointF(translatedX, translatedY));
    }

    return points;
}


    public static Func<float, float, PointF> GetCircle()
    {
        return (angle, radius) =>
        {
            float x = radius * (float)Math.Cos(angle);
            float y = radius * (float)Math.Sin(angle);
            return new PointF(x, -y);
        };
    }
    public static List<PointF> GetPointsOfSemiCircle(PointF center, float radius, float endAngle,  float rotationAngle = 180 , float startAngle = 0, float step = 0.05f)
    {
        List<PointF> points = new List<PointF>();
        var semiCircleFunc = GetCircle();
        for (float angle = startAngle; angle <= endAngle; angle += step)
        {
            PointF point = semiCircleFunc(angle, radius);
            float rotatedX = point.X * (float)Math.Cos(rotationAngle) - point.Y * (float)Math.Sin(rotationAngle);
            float rotatedY = point.X * (float)Math.Sin(rotationAngle) + point.Y * (float)Math.Cos(rotationAngle);
            points.Add(new PointF(center.X + rotatedX, center.Y + rotatedY));
        }

        return points;
    }

    public static bool ArePointsClose(PointF p1, PointF p2, double threshold)
    {
        double distance = GetDistance(p1, p2);
        return distance <= threshold;
    }
    public static double GetDistance(PointF p1, PointF p2)
    {
        return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
    }

    // Obtenir un point aléatoire à partir d'une liste de PointF
    public static PointF GetRandPoint(List<PointF> allPoints)
    {
        if (allPoints.Count == 0)
        {
            throw new ArgumentException("La liste des points est vide.");
        }

        Random rand = new Random();
        int index = rand.Next(allPoints.Count);
        return allPoints[index];
    }
    public static List<PointF> GeneratePoints(PointF pointA, PointF pointB)
    {
        // Calcul de la distance entre A et B
        double distance = Math.Sqrt(Math.Pow(pointB.X - pointA.X, 2) + Math.Pow(pointB.Y - pointA.Y, 2));

        // Déterminer le nombre de points (par exemple 1 point pour chaque unité de distance)
        int numPoints = (int)Math.Ceiling(distance);

        // Liste des points
        var points = new List<PointF>();

        // Génération des points
        for (int i = 0; i <= numPoints; i++)
        {
            float t = (float)i / numPoints; // Interpolation linéaire
            float x = pointA.X + t * (pointB.X - pointA.X);
            float y = pointA.Y + t * (pointB.Y - pointA.Y);
            points.Add(new PointF(x, y));
        }

        return points;
    }

    public static List<PointF> getPointRectiligne(PointF a, PointF b, double vitesse)
    {
        List<PointF> pointsRectiligne = new List<PointF>();
        pointsRectiligne.Add(a);
        double dx = b.X - a.X;
        double dy = b.Y - a.Y;
        double steps = Math.Max(Math.Abs(dx), Math.Abs(dy)) / vitesse;
        for (int i = 1; i <= steps; i++)
        {
            double newX = a.X + (dx * i) / steps;
            double newY = a.Y + (dy * i) / steps;
            pointsRectiligne.Add(new PointF((float)newX, (float)newY));
        }
        return pointsRectiligne;
    }
    public static Boolean estDansCetteDroite(PointF A, PointF B, PointF C) {
    double determinant = ((B.X - A.X) * (C.Y - A.Y)) - ((B.Y - A.Y) * (C.X - A.X));
    return Math.Abs(determinant) <= 0.0001;
}
}