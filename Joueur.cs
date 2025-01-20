

using System.Data.SqlClient;
using aff;
using geo;

namespace terrain
{

    public class Joueur
    {

        public Color color { get; set; }
        public int iteration { get; set; }
        // public List<MyForm> myForms { get; set; }
        public int IdJoueur { get; set; }
        public string nom { get; set; }
        public List<MyBlock> myBlocks { get; set; }
        public List<List<MyBlock>> winBlocks { get; set; }




        public Score score { get; set; }
        public Joueur(int IdJoueur, string nom, int iteration, Color color)
        {
            this.IdJoueur = IdJoueur;
            this.nom = nom;
            this.myBlocks = new List<MyBlock>();
            this.color = color;
            this.iteration = iteration;
            this.winBlocks = new List<List<MyBlock>>();
        }
        public Joueur(int IdJoueur, string nom)
        {
            this.IdJoueur = IdJoueur;
            this.nom = nom;
            this.myBlocks = new List<MyBlock>();
            this.color = color;
            this.winBlocks = new List<List<MyBlock>>();

        }
        public Joueur()
        {

        }




        public List<Direction> tempDir()
        {
            List<Direction> directions = new List<Direction>();
            int iteration = this.iteration;
            if (myBlocks.Count != 0)
            {
                MyBlock myBlock = myBlocks.Last();
                float y = myBlock.Center.Y;
                float x = myBlock.Center.X;

                // Direction supérieure
                List<PointF> resp = new List<PointF>();
                for (int j = 0; j < iteration; j++)
                {
                    PointF pointF = new PointF(x, y);
                    resp.Add(pointF);
                    y -= myBlock.Rectangle.Height;
                }
                directions.Add(new Direction("vertical y-", resp));

                // Direction inférieure
                resp = new List<PointF>();
                y = myBlock.Center.Y;
                x = myBlock.Center.X;
                for (int j = 0; j < iteration; j++)
                {
                    PointF pointF = new PointF(x, y);
                    resp.Add(pointF);
                    y += myBlock.Rectangle.Height;
                }
                directions.Add(new Direction("vertical y+", resp));

                // Direction oblique supérieure-gauche
                resp = new List<PointF>();
                y = myBlock.Center.Y;
                x = myBlock.Center.X;
                for (int j = 0; j < iteration; j++)
                {
                    PointF pointF = new PointF(x, y);
                    resp.Add(pointF);
                    x -= myBlock.Rectangle.Width;
                    y -= myBlock.Rectangle.Height;
                }
                directions.Add(new Direction("oblique supérieur-gauche", resp));

                // Direction oblique supérieure-droite
                resp = new List<PointF>();
                y = myBlock.Center.Y;
                x = myBlock.Center.X;
                for (int j = 0; j < iteration; j++)
                {
                    PointF pointF = new PointF(x, y);
                    resp.Add(pointF);
                    x += myBlock.Rectangle.Width;
                    y -= myBlock.Rectangle.Height;
                }
                directions.Add(new Direction("oblique supérieur-droit", resp));

                // Direction oblique inférieure-gauche
                resp = new List<PointF>();
                y = myBlock.Center.Y;
                x = myBlock.Center.X;
                for (int j = 0; j < iteration; j++)
                {
                    PointF pointF = new PointF(x, y);
                    resp.Add(pointF);
                    x -= myBlock.Rectangle.Width;
                    y += myBlock.Rectangle.Height;
                }
                directions.Add(new Direction("oblique inférieur-gauche", resp));

                // Direction oblique inférieure-droite
                resp = new List<PointF>();
                y = myBlock.Center.Y;
                x = myBlock.Center.X;
                for (int j = 0; j < iteration; j++)
                {
                    PointF pointF = new PointF(x, y);
                    resp.Add(pointF);
                    x += myBlock.Rectangle.Width;
                    y += myBlock.Rectangle.Height;
                }
                directions.Add(new Direction("oblique inférieur-droit", resp));

                // Direction horizontale gauche
                resp = new List<PointF>();
                y = myBlock.Center.Y;
                x = myBlock.Center.X;
                for (int j = 0; j < iteration; j++)
                {
                    PointF pointF = new PointF(x, y);
                    resp.Add(pointF);
                    x -= myBlock.Rectangle.Width;
                }
                directions.Add(new Direction("horizontale x-", resp));

                // Direction horizontale droite
                resp = new List<PointF>();
                y = myBlock.Center.Y;
                x = myBlock.Center.X;
                for (int j = 0; j < iteration; j++)
                {
                    PointF pointF = new PointF(x, y);
                    resp.Add(pointF);
                    x += myBlock.Rectangle.Width;
                }
                directions.Add(new Direction("horizontale x+", resp));
            }
            return directions;
        }
    }
}

