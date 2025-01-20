using geo;
using terrain;
using Timer = System.Windows.Forms.Timer;
using System.Drawing;


namespace aff
{
    class TerrainPanel : Panel
    {
        private Label label;
        public Boolean repaint = true;
        private Timer timer; // Déclarer le Timer

        public static List<MyBlock> myBlocks = new List<MyBlock>();
        public TerrainPanel()
        {
            this.DoubleBuffered = true;
            configurationTerrainLabel();
            InitializeTimer();
            initilizeBlocks();

        }


        public void configurationTerrainLabel()
        {
            this.Location = new Point(65, 85);
            this.Size = new Size(1000, 442);
            this.Controls.Add(label);
            this.BackColor = Color.White;

        }

        protected override async void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            drawBlocks(g);
            drawJBlocks(g);
            checkWin();
            drawWin(g);
            drawJTempBlock(g);
        }
        private async Task Scored(Joueur joueur, int point)
        {
            // await Task.Delay(2000);
            // Joueur j = new Joueur();
            // // j.insertScore(joueur.IdJoueur, point);
            // Fenetre.scorePanel.configurationTerrainLabel();
            // repaint = true;
        }

        public void drawWin(Graphics g)
        {
            List<Joueur> joueurs = Program.joueurs;
            foreach (var joueur in joueurs)
            {
                foreach (var winBlock in joueur.winBlocks)
                {
                    List<PointF> winPoint = new List<PointF>();
                    foreach (var block in winBlock)
                    {
                        winPoint.Add(block.Center);

                    }
                    Pen pen = new Pen(joueur.color, 3);
                    PointF[] point = winPoint.ToArray();
                    pen.Color = joueur.color;
                    if (point.Count() >= 1)
                    {
                        g.DrawLines(pen, point);
                    }
                }

            }
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (repaint == true)
            {
                this.Invalidate();
            }
        }


        public void drawJTempBlock(Graphics g)
        {
            List<Joueur> joueurs = Program.joueurs;
            foreach (var joueur in joueurs)
            {
                foreach (var block in myBlocks)
                {
                    foreach (Direction item in joueur.tempDir())
                    {
                        foreach (var temp_point in item.points)
                        {
                            if (Fonction.ArePointsClose(block.Center, temp_point, 25))
                            {
                                int ellipseWidth = block.Rectangle.Width / 2;
                                int ellipseHeight = block.Rectangle.Height / 2;
                                int ellipseX = block.Rectangle.X + (block.Rectangle.Width - ellipseWidth) / 2;
                                int ellipseY = block.Rectangle.Y + (block.Rectangle.Height - ellipseHeight) / 2;
                                // g.FillEllipse(new SolidBrush(Color.LightGray), ellipseX, ellipseY, ellipseWidth, ellipseHeight);
                            }
                        }
                    }
                }
            }
        }
        public void drawJBlocks(Graphics g)
        {
            List<Joueur> joueurs = Program.joueurs;
            foreach (var joueur in joueurs)
            {
                foreach (var block in joueur.myBlocks)

                {
                    //class dot a ne pas oublier
                    int ellipseWidth = block.Rectangle.Width / 2;
                    int ellipseHeight = block.Rectangle.Height / 2;
                    int ellipseX = block.Rectangle.X + (block.Rectangle.Width - ellipseWidth) / 2;
                    int ellipseY = block.Rectangle.Y + (block.Rectangle.Height - ellipseHeight) / 2;
                    g.FillEllipse(new SolidBrush(joueur.color), ellipseX, ellipseY, ellipseWidth, ellipseHeight);

                }

            }

        }

        public void drawBlocks(Graphics g)
        {
            foreach (var block in myBlocks)
            {
                block.draw(g);
            }
        }

        public void checkWin()
        {
            List<Joueur> joueurs = Program.joueurs;
            foreach (var joueur in joueurs)
            {
                List<Direction> tempDir = joueur.tempDir();
                List<List<MyBlock>> tempBlock = new List<List<MyBlock>>();
                foreach (var dir in tempDir)
                {
                    int count = 0;
                    List<MyBlock> currentWinBlocks = new List<MyBlock>();
                    foreach (var point in dir.points)
                    {
                        foreach (var Jblock in joueur.myBlocks)
                        {
                            if (Jblock.Center.Equals(point))
                            {
                                count++;
                                currentWinBlocks.Add(Jblock);
                            }
                        }
                    }
                    if (count == joueur.iteration)
                    {
                        joueur.winBlocks.Add(currentWinBlocks);
                        dir.win = true;
                    }
                }
            }
        }

        public void initilizeBlocks()
        {


            int xMax = this.Location.X + this.Width;
            int xMin = this.Location.X;

            int yMax = this.Location.Y + this.Height;
            int yMin = this.Location.Y;

            int blockWidth = 30;
            int blockHeight = 30;


            for (int i = 0; i <= xMax; i += blockWidth)
            {
                for (int j = 0; j <= yMax; j += blockHeight)
                {
                    MyBlock theBlock = new MyBlock(myBlocks.Count, i, j, blockWidth, blockHeight);
                    myBlocks.Add(theBlock);
                }
            }
        }
    }
}