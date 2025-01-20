namespace geo
{
    public class Plot
{

    public Boolean IsMoving { get; set; }
    public int Score { get; set; }

    public double vitesse {get;set;}

    public string name {get;set;}

    public PointF Center { get; set; }
    public int VaVin { get; set; }
    public int height { get; set; }
    public int width { get; set; }
    public PointF Point { get; set; }

    public Plot(PointF point)
    {
        Point = point;
        IsMoving = false;
        VaVin = 0;

    }
    public Plot(int width, int height , int score , double vitesse , String name)
    {
        this.width = width;
        this.height = height;
        this.Score  =score;
        this.vitesse = vitesse;
        this.name = name;
    }
    public void draw(Graphics g     , Color color )
    {
        g.DrawArc(new Pen (color), Point.X, Point.Y, this.width, this.height, 0, 360);
        float centreX = Point.X + width / 2;
        float centreY = Point.Y + height / 2;
        Center = new PointF (centreX, centreY);
        drawIdCercle(g);

        
    }
     public void drawIdCercle(Graphics g)
        {
            Font font = new Font("Arial", 10, FontStyle.Bold);
            Brush brush = Brushes.Black;
            string text =  this.Score + " Pt";
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(text, font, brush, Center.X, Center.Y, format);
        }
    public async Task movePlotAsync(PointF[] points , double vitesse)
    {
        int i = 0;
        if (VaVin % 2 == 0)
        {
            for (i = 0; i < points.Count() - 1; i++)
            {
                List<PointF> p = Fonction.getPointRectiligne(points[i], points[i + 1], this.vitesse);
                foreach (var item in p)
                {
                    this.Point = item;
                    // MyConsole.addLine("movePlotAsync " + this.Point);
                    await Task.Delay(1);

                }
            }
        }
        else
        {
            for (i = points.Count() - 1; i > 0; i--)
            {
                List<PointF> p = Fonction.getPointRectiligne(points[i], points[i - 1], this.vitesse);
                foreach (var item in p)
                {
                    this.Point = item;
                    // MyConsole.addLine("movePlotAsync " + this.Point);
                    await Task.Delay(1);

                }
            }
        }
        IsMoving = false;
        VaVin = VaVin + 1;
    }
}

}
