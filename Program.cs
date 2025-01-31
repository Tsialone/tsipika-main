using terrain;
using System.Drawing;
using geo;

namespace aff
{
    public static class Program
    {
        public static Joueur j1 = new Joueur(1, "Marc", 5, Color.Orange, new Button
        {

            Text = "Marc",
            Location = new Point(450, 30),
            Width = 100,
            Height = 60,
            Enabled = false
        });
        public static Joueur tempPlayer;
        public static Joueur tempPlayerAd;

        public static List<PointF> winedPoint = new List<PointF>();
        public static Joueur j2 = new Joueur(2, "Lioka", 5, Color.SkyBlue, new Button
        {
            Text = "Lioka",
            Location = new Point(450, 90),
            Width = 100,
            Height = 60,
            Enabled = false

        });
        public static int tour = 0;

        public static List<PointF> placedPoint = new List<PointF>();

        public static List<Joueur> joueurs = new List<Joueur> { j1, j2 };
    

        public static void Main()
        {
            Application.EnableVisualStyles();
            openFenetre();
            // Exemple d'utilisation
                  }


        public static void reversePlayer()
        {
            List<Joueur> joueurs = Program.joueurs;
            for (int i = 0; i < joueurs.Count - 1; i++)
            {
                Joueur temp = joueurs[i];
                joueurs[i] = joueurs[i + 1];
                joueurs[i + 1] = temp;
            }
        }
        static void openFenetre()
        {
            Application.Run(new Fenetre());
        }
    }

};

