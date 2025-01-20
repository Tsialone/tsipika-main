
namespace geo
{
    public class Direction
    {
        public List<PointF> points { get; set; }
        public Boolean win {get;set;}
        public string nom { set; get; }

        public Direction(String nom, List<PointF> pointF)
        {
            this.points = pointF;
            this.nom = nom;
        }
    }
}
