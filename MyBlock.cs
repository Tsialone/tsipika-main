using System.Drawing;
namespace aff
{
    public class MyBlock
    {
        private Point center;

        public static Boolean Config = true;
        public Boolean win {get;set;}
        public int idBlock { set; get; }
        public Rectangle Rectangle { set; get; }
        public PointF Center
        {
            get
            {

                return new PointF((this.Rectangle.X + this.Rectangle.Width / 2), (this.Rectangle.Y + this.Rectangle.Height / 2));
            }
            set
            {
                Center = value;
            }
        }
        public int Position { set; get; }
        public MyBlock(int id, int x, int y, int width, int height)
        {
            Rectangle = new Rectangle(x, y, width, height);
            this.idBlock = id;
            this.win = false;
        }
        public void draw(Graphics g)
        {

            g.DrawRectangle(Pens.Black, this.Rectangle);
            drawIdBlock(g);

        }
        public void drawIdBlock(Graphics g)
        {
            if (Config)
            {
                Font font = new Font("Arial", 7, FontStyle.Bold);
                Brush brush = Brushes.Black;
                string text = "" + (this.Rectangle.X + this.Rectangle.Width / 2) + ","+ (this.Rectangle.Y + this.Rectangle.Height / 2) ;
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Near
                };
                g.DrawString(text, font, brush, this.Rectangle, format);
            }

        }
    }
}
