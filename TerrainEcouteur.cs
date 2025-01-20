using System;
using aff;
using terrain;

public class TerrainEcouteur
{
    public TerrainEcouteur()
    {

    }
    public void graph_Mod(object sender, EventArgs e)
    {
        if (Fenetre.graphMod.Checked)
        {
            MyBlock.Config = true;
            // MessageBox.Show("Vous avez passe graph mod");
        }
        else
        {
            MyBlock.Config = false;

            // MessageBox.Show("Vous devez accepter les termes et conditions.");
        }
    }
    public void mouseClick(object sender, MouseEventArgs e)
    {
        PointF mousePoint = new PointF(e.X, e.Y);
        Joueur j1 = Program.j1;
        Joueur j2 = Program.j2;
        List<Joueur> joueurs = Program.joueurs;
        List<MyBlock> placedPoint = Program.placedPoint;
        int tour = Program.tour;
        MyConsole.addLine(" " + TerrainPanel.myBlocks.Count);
        if (e.Button.Equals(MouseButtons.Left))
        {



            // MyConsole.addLine(joueurs[0].nom + " " + joueurs[0].winBlocks.Count);

            foreach (var block in TerrainPanel.myBlocks)
            {
                if (Fonction.ArePointsClose(block.Center, mousePoint, 25))
                {
                    MyConsole.addLine("block " + block.Center);
                    if (!placedPoint.Contains(block))
                    {
                        Joueur tempF = joueurs.First();
                        Joueur tempL = joueurs.Last();
                        joueurs[0] = tempL;
                        joueurs[1] = tempF;
                        joueurs[0].myBlocks.Add(block);
                       
                        placedPoint.Add(block);
                    }
                    return;
                }
            }
        }

    }
}