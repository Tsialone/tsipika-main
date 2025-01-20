using terrain;
using System.Drawing;
using geo;

namespace aff
{
    public static class Program
    {
        public static Joueur j1 = new Joueur(1, "Marc", 4 ,Color.Orange);
        public static Joueur j2 = new Joueur(2, "Lioka", 4 ,Color.SkyBlue);
        public static int tour = 0;

        public static List<MyBlock> placedPoint  = new List<MyBlock> ();

        public static List<Joueur> joueurs = new List<Joueur> {j1 ,j2};
        static void Main()
        {
            // Application.SetCompatibleTextRenderingDefault(true);
            Application.EnableVisualStyles();
            openFenetre();
        }
        static void openFenetre()
        {
            Application.Run(new Fenetre());
        }

    }

};

