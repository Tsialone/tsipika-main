namespace geo
{
        public class Dot {
            public float avancement  = 50;
            public PointF his_position {get;set;}
            public List<Branche> probable_branches {get;set;}
            public List<Branche> his_branches {get;set;}
            public Dot (PointF position){
                this.his_branches = new List<Branche> ();
                this.his_position = position;
                this.probable_branches = new List<Branche> ();
            }
        }
}