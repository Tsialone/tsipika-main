namespace geo
{
    public class Branche
    {

        public string his_type { get; set; }
        public List<PointF> hisPoints { get; set; }
        public Dot his_dot { get; set; }
        public List<PointF> to_verify { get; set; }


        public Branche(string type, Dot dot, List<PointF> to_verify )
        {
            this.his_type = type;
            this.his_dot = dot;
            this.hisPoints = new List<PointF>();
            this.to_verify = to_verify;
            this.initHisPointsByHisDot();
        }
        public void initHisPointsByHisDot()
        {
            PointF his_dot_point = this.his_dot.his_position;
            float avancement = this.his_dot.avancement;
            float temp_x = his_dot_point.X;
            float temp_y = his_dot_point.Y;

            if (this.his_type.Equals("x+"))
            {
                temp_x += avancement;
                while (this.to_verify.Contains(new PointF(temp_x, temp_y)))
                {
                    this.hisPoints.Add(new PointF(temp_x, temp_y));
                    temp_x += avancement;
                }
            }
            else if (this.his_type.Equals("x-"))
            {
                temp_x -= avancement;
                while (this.to_verify.Contains(new PointF(temp_x, temp_y)))
                {
                    this.hisPoints.Add(new PointF(temp_x, temp_y));
                    temp_x -= avancement;
                }
            }
            else if (this.his_type.Equals("y+"))
            {
                temp_y += avancement;
                while (this.to_verify.Contains(new PointF(temp_x, temp_y)))
                {
                    this.hisPoints.Add(new PointF(temp_x, temp_y));
                    temp_y += avancement;
                }
            }
            else if (this.his_type.Equals("y-"))
            {
                temp_y -= avancement;
                while (this.to_verify.Contains(new PointF(temp_x, temp_y)))
                {
                    this.hisPoints.Add(new PointF(temp_x, temp_y));
                    temp_y -= avancement;
                }
            }
        }
    }
}