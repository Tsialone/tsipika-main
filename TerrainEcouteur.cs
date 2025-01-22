using System;
using aff;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        }
        else
        {
            MyBlock.Config = false;
        }
    }

    public void _load(object sender, EventArgs e)
    {

        string json = File.ReadAllText("save.json");
        var loadedData = JArray.Parse(json);
        foreach (var item in loadedData)
        {
            string nom = item["Nom"].ToString();
            if (nom == "J1")
            {
                var points = item["points"];
                var winpoint = item["winpoint"];
                List<PointF> listJ = points.ToObject<List<PointF>>();
                List<MyBlock> myBlocks = new List<MyBlock>();
                foreach (var P in listJ)
                {
                    myBlocks.Add(Joueur.getBlock(TerrainPanel.myBlocks, P));
                }
                Program.j1.myBlocks = myBlocks;
                Program.j1.winPoints = winpoint.ToObject<List<List<PointF>>>();
            }
            if (nom == "J2")
            {
                var points = item["points"];
                var winpoint = item["winpoint"];
                List<PointF> listJ = points.ToObject<List<PointF>>();
                List<MyBlock> myBlocks = new List<MyBlock>();
                foreach (var P in listJ)
                {
                    myBlocks.Add(Joueur.getBlock(TerrainPanel.myBlocks, P));
                }
                Program.j2.myBlocks = myBlocks;
                Program.j2.winPoints = winpoint.ToObject<List<List<PointF>>>();
            }
            if (nom == "program")
            {
                var placedPoint = item["placedPoint"];
                var winedpoint = item["winedpoint"];
                Program.placedPoint = placedPoint.ToObject<List<PointF>>(); ;
                Program.winedPoint = winedpoint.ToObject<List<PointF>>();
            }
        }
    }


    public void _save(object sender, EventArgs e)
    {
        List<PointF> J1Pf = new List<PointF>();
        List<PointF> J2Pf = new List<PointF>();
        foreach (var item in Program.j1.myBlocks)
        {
            J1Pf.Add(item.Center);
        }
        foreach (var item in Program.j2.myBlocks)
        {
            J2Pf.Add(item.Center);
        }
        var data = new object[]
        {
        new {
            Nom = "J1",
            points =  J1Pf,
            winpoint = Program.j1.winPoints

        },
          new {
            Nom = "J2",
            points =  J2Pf,
            winpoint = Program.j2.winPoints

        },
          new {
                Nom = "program",
                placedPoint = Program.placedPoint,
                winedpoint = Program.winedPoint
            }
        };
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText("save.json", json);
        MessageBox.Show("Sauvegarde réussie :)", "Sauvegarde", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public void mouseClick(object sender, MouseEventArgs e)
    {
        PointF mousePoint = new PointF(e.X, e.Y);
        List<Joueur> joueurs = Program.joueurs;
        List<PointF> placedPoint = Program.placedPoint;
        if (e.Button.Equals(MouseButtons.Left))
        {

            foreach (var block in TerrainPanel.myBlocks)
            {
                if (Fonction.ArePointsClose(block.Center, mousePoint, 25))
                {
                    MyConsole.addLine("taille" + block.Center);
                    if (!placedPoint.Contains(block.Center))
                    {
                        foreach (var j in joueurs)
                        {
                            j.suggest.Text = j.nom;
                            j.suggest.Enabled = false;
                        }
                        Program.reversePlayer();
                        try
                        {
                            int nbr = int.Parse(Fenetre.nbr.Text);
                            if (joueurs[0].myBlocks.Count > nbr)
                            {
                                var removedBlock = joueurs[0].myBlocks[0];
                                joueurs[0].myBlocks.RemoveAt(0);

                                placedPoint.Remove(removedBlock.Center);
                                Program.winedPoint.Remove(removedBlock.Center);

                                joueurs[0].winPoints = joueurs[0].winPoints
                                    .Where(p => !p.Contains(removedBlock.Center))
                                    .ToList();

                            }
                        }
                        catch (System.Exception)
                        {

                        }
                        joueurs[0].myBlocks.Add(block);
                        placedPoint.Add(block.Center);
                        if (joueurs[0].mandresy(block, joueurs[0].iteration) != null)
                        {
                            List<PointF> winPoint = joueurs[0].mandresy(block, joueurs[0].iteration);
                            joueurs[0].winPoints.Add(winPoint);
                            Program.winedPoint.AddRange(winPoint);


                        }
                        if (joueurs[1].sugAttack(joueurs[0]) != null)
                        {

                            joueurs[1].suggest.Enabled = true;
                            joueurs[1].suggest.Text = "Sug " + joueurs[1].nom + " at " + joueurs[1].sugAttack(joueurs[0]);
                            Program.tempPlayerAd = joueurs[0];

                        }
                    }
                    return;
                }
            }
        }

    }
}