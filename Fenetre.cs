using System;
using System.Drawing;
using System.Windows.Forms;

namespace aff
{
    public class Fenetre : Form
    {

        public static CheckBox graphMod;
        private TerrainPanel terrainPanel;
        public Button save;
        public Button load;
        private MyConsole console;
        public static ScorePanel scorePanel;

        // Ajout des champs vitesse et point
        private Label vitesseLabel;
        public static TextBox vitesseInput;

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
            save.Click += terrainEcouteur._save;
            load.Click += terrainEcouteur._load;

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
                Checked = false
            };
            save = new Button
            {
                Text = "Save",
                Location = new Point(450, 160),
                Width = 100,
                Height = 60,
            };
             load = new Button
            {
                Text = "Load",
                Location = new Point(450, 240),
                Width = 100,
                Height = 60,
            };

        }

        private void configurationFenetre()
        {
            Text = "tsipika";
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
            this.Controls.Add(Program.j1.suggest);
            this.Controls.Add(Program.j2.suggest);
            this.Controls.Add(save);
            this.Controls.Add(load);



        }
    }
}
