using System;
using System.Drawing;
using System.Windows.Forms;

namespace aff
{
    public class Fenetre : Form
    {
        private Button button;
        public static CheckBox graphMod;
        private TerrainPanel terrainPanel;

        private MyConsole console;
        public static ScorePanel scorePanel;

        // Ajout des champs vitesse et point
        private Label vitesseLabel;
        public static TextBox  vitesseInput;
        private Label pointLabel;
        public  static TextBox pointInput;

        public void initializeConsole()
        {
        }

        public Fenetre()
        {
            configurationExtension();
            configurationFenetre();
            addEcouteur();
        }

        public void addEcouteur()
        {
            TerrainEcouteur terrainEcouteur = new TerrainEcouteur();
            terrainPanel.MouseClick += terrainEcouteur.mouseClick;
            graphMod.CheckedChanged += terrainEcouteur.graph_Mod;
        }

        public static void repaintScore()
        {
            scorePanel.Invalidate();
        }

        public void configurationExtension()
        {
            // Configuration du CheckBox graphMod
            graphMod = new CheckBox
            {
                Text = "graph mod",
                Location = new Point(300, 20),
                Checked = true
            };
        }

        private void configurationFenetre()
        {
            Text = "Tennis Family";
            Width = 1200;
            Height = 670;
            terrainPanel = new TerrainPanel();

            console = new MyConsole();
            scorePanel = new ScorePanel();

            // Ajout des composants à la fenêtre
            this.Controls.Add(terrainPanel);
            this.Controls.Add(console);
            this.Controls.Add(scorePanel);
            this.Controls.Add(graphMod);
            this.Controls.Add(vitesseLabel);
            this.Controls.Add(vitesseInput);
            this.Controls.Add(pointLabel);
            this.Controls.Add(pointInput);
        }
    }
}
