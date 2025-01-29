

using System.Data.SqlClient;
using System.Text;
using aff;
using geo;

namespace terrain
{

    public class Joueur
    {

        public Color color { get; set; }
        public Button suggest { get; set; }

        public PointF ghost { get; set; }
        public int iteration { get; set; }
        // public List<MyForm> myForms { get; set; }
        public int IdJoueur { get; set; }
        public string nom { get; set; }
        public List<MyBlock> myBlocks { get; set; }
        public List<List<PointF>> winPoints { get; set; }




        public Score score { get; set; }
        public Joueur(int IdJoueur, string nom, int iteration, Color color, Button button)
        {
            this.IdJoueur = IdJoueur;
            this.nom = nom;
            this.myBlocks = new List<MyBlock>();
            this.color = color;
            this.iteration = iteration;
            this.winPoints = new List<List<PointF>>();
            this.suggest = button;
            this.suggest.Click += this._jClick;
        }
        public void _jClick(object sender, EventArgs e)
        {

            PointF? pointNullable = this.sugAttack(Program.tempPlayerAd);
            if (pointNullable.HasValue)
            {
                MyConsole.addLine(" " + this.nom + " " + pointNullable);
                PointF point = pointNullable.Value;
                MyBlock myBlock = getBlock(TerrainPanel.myBlocks, point);
                List<PointF> winPoint = this.mandresy(myBlock, this.iteration);
                if (winPoint != null)
                {
                    this.myBlocks.Add(myBlock);
                    this.winPoints.Add(winPoint);
                    Program.winedPoint.AddRange(winPoint);
                }
                else
                {
                    int avant = this.myBlocks.Count;
                    this.myBlocks.Add(myBlock);
                }
                PointF? adversarySuggestion = Program.tempPlayerAd.sugAttack(this);
                if (adversarySuggestion.HasValue)
                {
                    Program.tempPlayerAd.suggest.Enabled = true;
                    Program.tempPlayerAd.suggest.Text = $"Sug {Program.tempPlayerAd.nom} at {adversarySuggestion}";
                    Program.tempPlayerAd = this;
                }
            }
            this.suggest.Enabled = false;
            this.suggest.Text = this.nom;
            Program.reversePlayer();
        }


        public Joueur(int IdJoueur, string nom)
        {
            this.IdJoueur = IdJoueur;
            this.nom = nom;
            this.myBlocks = new List<MyBlock>();
            this.color = color;
            this.winPoints = new List<List<PointF>>();
        }
        public Joueur()
        {

        }
        public PointF? deffense(Joueur adverse)
        {
            return this.sugAttack(adverse);
        }
        public PointF? sugAttack(Joueur adverse, bool isReversed = false)
        {
            List<MyBlock> terrainBlocks = TerrainPanel.myBlocks;
            List<PointF> adP = adverse.myBlocks.Select(block => block.Center).ToList();

            if (this.myBlocks.Count >= 3 && adverse.myBlocks.Count >= 3)
            {
                // Maka attaque finission
                foreach (var Tblock in terrainBlocks)
                {
                    if (!this.myBlocks.Contains(Tblock))
                    {
                        List<PointF> mandresyTest = this.mandresy(Tblock, this.iteration);
                        if (mandresyTest != null && !adP.Intersect(mandresyTest).Any())
                        {
                            MyConsole.addLine("Maka niveau 1");
                            MyConsole.addLine($"\n\nThe block {Tblock.Center}, Mandresy count: {mandresyTest.Count}");
                            return Tblock.Center;
                        }
                    }
                }
                // deffence
                MyConsole.addLine("Maka niveau 2 deffence");
                foreach (var Tblock in terrainBlocks)
                {
                    if (!isReversed && adverse.mandresy(Tblock, adverse.iteration) != null)
                    {
                        return adverse.sugAttack(this, true);
                    }
                }
                // Maka attaque
                foreach (var Tblock in terrainBlocks)
                {
                    if (!this.myBlocks.Contains(Tblock))
                    {
                        List<PointF> mandresyTest = this.mandresy(Tblock, this.iteration - 1);
                        if (mandresyTest != null && !adP.Intersect(mandresyTest).Any())
                        {
                            MyConsole.addLine("Maka niveau 3 attaque maivana");
                            MyConsole.addLine($"\n\nThe block {Tblock.Center}, Mandresy count: {mandresyTest.Count}");
                            return Tblock.Center;
                        }
                    }
                }


            }

            return null;
        }


        public static MyBlock? getBlock(List<MyBlock> myBlocks, PointF x)
        {
            foreach (var block in myBlocks)
            {
                if (Fonction.ArePointsClose(block.Center, x, 25))
                {
                    return block;
                }
            }
            return null;
        }
        public void initilizeWinPoint()
        {

        }


        public List<PointF>? mandresy(MyBlock myBlock, int iteration)
        {
            List<MyBlock> myBlocks = this.myBlocks;
            List<PointF> myLp = new List<PointF>();
            PointF P = myBlock.Center;
            foreach (var block in myBlocks)
            {
                myLp.Add(block.Center);
            }
            //verifier y+
            // MyConsole.addLine("\n\nverification y+ ");
            List<PointF> Ltp = new List<PointF>();
            Ltp.Add(myBlock.Center);
            float x = P.X;
            float y = P.Y;
            for (int i = 0; i < iteration; i++)
            {
                y += myBlock.Rectangle.Height;
                if (myLp.Contains(new PointF(x, y)) && !Program.winedPoint.Contains(new PointF(x, y)) && Ltp.Count != iteration)
                {
                    Ltp.Add(new PointF(x, y));
                }
                else
                {
                    break;
                }
            }
            // verifier y-
            // MyConsole.addLine("\n\nverification y- ");
            x = P.X;
            y = P.Y;
            for (int i = 0; i < iteration; i++)
            {
                y -= myBlock.Rectangle.Height;
                if (myLp.Contains(new PointF(x, y)) && !Program.winedPoint.Contains(new PointF(x, y)) && Ltp.Count != iteration)
                {
                    Ltp.Add(new PointF(x, y));
                }
                else
                {
                    break;
                }
            }
            if (Ltp.Count == iteration)
            {


                // throw new Exception("Y");
                return Ltp;

            }
            //verification de x+
            // MyConsole.addLine("\n\nverification x+ ");
            Ltp = new List<PointF>();
            Ltp.Add(myBlock.Center);
            x = P.X;
            y = P.Y;
            for (int i = 0; i < iteration; i++)
            {
                x += myBlock.Rectangle.Width;
                if (myLp.Contains(new PointF(x, y)) && !Program.winedPoint.Contains(new PointF(x, y)) && Ltp.Count != iteration)
                {
                    Ltp.Add(new PointF(x, y));
                }
                else
                {
                    break;
                }
            }
            // verifier x-
            // MyConsole.addLine("\n\nverification x- ");
            x = P.X;
            y = P.Y;
            for (int i = 0; i < iteration; i++)
            {
                x -= myBlock.Rectangle.Width;
                if (myLp.Contains(new PointF(x, y)) && !Program.winedPoint.Contains(new PointF(x, y)) && Ltp.Count != iteration)
                {
                    Ltp.Add(new PointF(x, y));
                }
                else
                {
                    break;
                }
            }
            // MyConsole.addLine("x " + Ltp.Count);
            if (Ltp.Count == iteration)
            {
                // throw new Exception("X " + Ltp.Count);
                return Ltp;

            }
            // //verification de od+
            // MyConsole.addLine("\n\nverification od+ ");
            Ltp = new List<PointF>();
            Ltp.Add(myBlock.Center);

            x = P.X;
            y = P.Y;
            for (int i = 0; i < iteration; i++)
            {
                x += myBlock.Rectangle.Width;
                y -= myBlock.Rectangle.Height;

                if (myLp.Contains(new PointF(x, y)) && !Program.winedPoint.Contains(new PointF(x, y)) && Ltp.Count != iteration)
                {
                    Ltp.Add(new PointF(x, y));
                }
                else
                {
                    break;
                }
            }
            // // verifier od-
            // MyConsole.addLine("\n\nverification od- ");
            x = P.X;
            y = P.Y;
            for (int i = 0; i < iteration; i++)
            {
                x -= myBlock.Rectangle.Width;
                y += myBlock.Rectangle.Height;
                if (myLp.Contains(new PointF(x, y)) && !Program.winedPoint.Contains(new PointF(x, y)) && Ltp.Count != iteration)
                {
                    Ltp.Add(new PointF(x, y));
                }
                else
                {
                    break;
                }
            }

            if (Ltp.Count == iteration)
            {

                return Ltp;
                // throw new Exception("D" + Ltp.Count);

            }
            // //verification de og+
            // MyConsole.addLine("\n\nverification og+ ");
            Ltp = new List<PointF>();
            Ltp.Add(myBlock.Center);
            x = P.X;
            y = P.Y;
            for (int i = 0; i < iteration; i++)
            {
                x -= myBlock.Rectangle.Width;
                y -= myBlock.Rectangle.Height;

                if (myLp.Contains(new PointF(x, y)) && !Program.winedPoint.Contains(new PointF(x, y)) && Ltp.Count != iteration)
                {
                    Ltp.Add(new PointF(x, y));
                }
                else
                {
                    break;
                }
            }


            // // verifier og-
            // MyConsole.addLine("\n\nverification og- ");
            x = P.X;
            y = P.Y;
            for (int i = 0; i < iteration; i++)
            {
                x += myBlock.Rectangle.Width;
                y += myBlock.Rectangle.Height;
                if (myLp.Contains(new PointF(x, y)) && !Program.winedPoint.Contains(new PointF(x, y)) && Ltp.Count != iteration)
                {
                    Ltp.Add(new PointF(x, y));
                }
                else
                {
                    break;
                }
            }
            if (Ltp.Count == iteration)
            {
                return Ltp;
                // throw new Exception("G" + Ltp.Count);
            }
            // MyConsole.addLine("\n\n liste finale " + Ltp.Count);

            return null;
        }


    }
}

