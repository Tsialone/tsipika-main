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
            InitializeBlocks();

        }


        public void configurationTerrainLabel()
        {
            this.Location = new Point(570, 20);
            this.Size = new Size(600, 600);
            this.Controls.Add(label);
            this.BackColor = Color.White;

        }

        protected override async void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            drawBlocks(g);
            drawJBlocks(g);
            // checkWin();
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
                foreach (var jW in joueur.winPoints)
                {
                    List<PointF> winPoint = jW;
                    if (winPoint.Count >= 1)
                    {
                        foreach (var point in winPoint)
                        {
                            int haloSize = 30;
                            Color lightColor = Color.FromArgb(100, joueur.color);
                            SolidBrush lightBrush = new SolidBrush(lightColor);
                            g.FillEllipse(lightBrush, point.X - haloSize / 2, point.Y - haloSize / 2, haloSize, haloSize);
                            int pointSize = 6;
                            SolidBrush pointBrush = new SolidBrush(joueur.color);
                            g.FillEllipse(pointBrush, point.X - pointSize / 2, point.Y - pointSize / 2, pointSize, pointSize);
                        }
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


        }
        public void drawJBlocks(Graphics g)
        {
            List<Joueur> joueurs = Program.joueurs;
            foreach (var joueur in joueurs)
            {
                foreach (var block in joueur.myBlocks)

                {
                    //class dot a ne pas oublier
                    int ellipseWidth = block.Rectangle.Width / 4;
                    int ellipseHeight = block.Rectangle.Height / 4;
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
                joueur.initilizeWinPoint();
            }
        }

        public void InitializeBlocks()
        {
            int xMin = 0;
            int yMin = 0;

            int xMax = this.Width;
            int yMax = this.Height;

            int blockWidth = 50;
            int blockHeight = 50;

            for (int i = xMin; i + blockWidth <= xMax; i += blockWidth)
            {
                for (int j = yMin; j + blockHeight <= yMax; j += blockHeight)
                {
                    MyBlock theBlock = new MyBlock(myBlocks.Count, i, j, blockWidth, blockHeight);

                    myBlocks.Add(theBlock);
                }
            }
        }

    }
}