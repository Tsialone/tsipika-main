

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

        public int score { get; set; }




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
            this.score = 0;
        }
        public void _jClick(object sender, EventArgs e)
        {

            PointF? pointNullable = this.sugAttack(Program.tempPlayerAd);
            if (pointNullable.HasValue)
            {
                // MyConsole.addLine(" " + this.nom + " " + pointNullable);
                PointF point = pointNullable.Value;
                MyBlock myBlock = getBlock(TerrainPanel.myBlocks, point);
                this.myBlocks.Add(myBlock);
                List<PointF> winPoint = this.mandresy(myBlock, this.iteration);
                if (winPoint != null)
                {
                    this.myBlocks.Add(myBlock);
                    this.winPoints.Add(winPoint);
                    MessageBox.Show("Le joueur " + this.nom + " a gagner");
                    this.score += 1;
                    ScorePanel.configurationTerrainLabel();
                    for (int i = 0; i < 2; i++)
                    {
                        Program.joueurs[i].myBlocks.Clear();
                        Program.joueurs[i].winPoints.Clear();
                        Program.winedPoint.Clear();
                        Program.placedPoint.Clear();
                    }
                    Program.winedPoint.AddRange(winPoint);
                }
                else
                {
                    // this.myBlocks.Add(myBlock);
                    // this.winPoints.Add(winPoint);
                    // MessageBox.Show("Le joueur " + this.nom + " a gagner");
                    // this.score += 1;
                    // ScorePanel.configurationTerrainLabel();
                    // for (int i = 0; i < 2; i++)
                    // {
                    //     Program.joueurs[i].myBlocks.Clear();
                    //     Program.joueurs[i].winPoints.Clear();
                    //     Program.winedPoint.Clear();
                    //     Program.placedPoint.Clear();
                    // }
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
                Console.WriteLine("Verification attaque de finission de " + this.nom);
                foreach (var Tblock in terrainBlocks)
                {
                    if (!this.myBlocks.Contains(Tblock))
                    {
                        this.myBlocks.Add(Tblock);
                        List<PointF> mandresyTest = this.mandresy(Tblock, this.iteration);
                        this.myBlocks.Remove(Tblock);
                        if (mandresyTest != null)
                        {
                            // throw new Exception("tayyyyyyyy at " + Tblock.Center);
                            // Console.WriteLine($"Attaque pour finition de {this.nom} at {Tblock.Center}");
                            // this.myBlocks.Remove(Tblock);  // Assurez-vous de retirer le bloc si l'opération échoue
                            return Tblock.Center;  // Retourne directement le centre du bloc trouvé
                        }

                    }
                }
                // deffence
                // MyConsole.addLine("Maka niveau 2 deffence");
                Console.WriteLine("Verification deffence " + this.nom + "....");
                foreach (var Tblock in terrainBlocks)
                {
                    this.myBlocks.Add(Tblock);
                    List<PointF> test = adverse.mandresy(Tblock, adverse.iteration);
                    this.myBlocks.Remove(Tblock);
                    if (!isReversed && test != null)
                    {
                        throw new Exception("deffence de " + this.nom + " at " + Tblock.Center);
                        // this.myBlocks.Remove(Tblock);  // Assurez-vous de retirer le bloc si l'opération échoue
                        return adverse.sugAttack(this, true);
                    }
                    else
                    {
                        // this.myBlocks.Remove(Tblock);  
                        Console.WriteLine("Pas de deffence sug " + this.nom);

                    }
                }
                // Maka attaque
                // Console.WriteLine("Verification attaque simple " + this.nom + "....");
                // foreach (var Tblock in terrainBlocks)
                // {
                //     if (!this.myBlocks.Contains(Tblock))
                //     {
                //         List<PointF> mandresyTest = this.mandresy(Tblock, this.iteration - 1);
                //         if (mandresyTest != null && !adP.Intersect(mandresyTest).Any())
                //         {
                //             // Console.WriteLine("Attaque");
                //             //    throw new Exception ("attaque pour finission de " + this.nom + " at " + Tblock.Center);
                //             throw new Exception("attaque simple de " + this.nom + " at " + Tblock.Center);
                //             return Tblock.Center;
                //         }
                //         else
                //         {
                //             Console.WriteLine("Pas d' attaque maivana pour" + this.nom + " sur " + Tblock.Center);

                //         }
                //     }
                // }


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
            List<string> branche_type_toverify = new List<string> { "x+", "x-", "y+", "y-" };
            PointF P = myBlock.Center;
            foreach (var block in myBlocks)
            {
                myLp.Add(block.Center);
            }
            List<Dot> all_dots = new List<Dot>();
            foreach (var pt in myBlocks)
            {
                PointF block_point = pt.Center;
                Dot my_dot = new Dot(block_point);
                List<Branche> all_branche_in_that_dot = new List<Branche>();
                foreach (var type in branche_type_toverify)
                {
                    Branche theBranche = new Branche(type, my_dot, myLp);
                    all_branche_in_that_dot.Add(theBranche);
                }
                my_dot.his_branches.AddRange(all_branche_in_that_dot);
                all_dots.Add(my_dot);
            }


            // Console.WriteLine("nombre de dot  " + all_dots.Count);
            //verification de possibilite de tuer
            int verification = iteration;
            foreach (var dot in all_dots)
            {

                var his_branche = dot.his_branches;
                var all_x_branch = his_branche
                    .Where(branch => branch.his_type.StartsWith('x') && branch.hisPoints.Count > 0)
                    .ToList();

                var all_y_branch = his_branche
                    .Where(branch => branch.his_type.StartsWith('y') && branch.hisPoints.Count > 0)
                    .ToList();

                // Console.WriteLine("nbr branche X " + all_x_branch.Count);
                // Console.WriteLine("nbr branche Y " + all_y_branch.Count);
                foreach (var x_b in all_x_branch)
                {
                    Branche? max_branch = null;
                    Branche? min_branch = null;

                    foreach (var y_b in all_y_branch)
                    {
                        int nombre_serie_X = x_b.hisPoints.Count;
                        int nombre_serie_Y = y_b.hisPoints.Count;

                        int total = nombre_serie_X + nombre_serie_Y;
                        int diff = nombre_serie_X - nombre_serie_Y;
                        if (nombre_serie_X > nombre_serie_Y)
                        {
                            max_branch = x_b;
                            min_branch = y_b;
                        }
                        else if (nombre_serie_Y > nombre_serie_X)
                        {
                            max_branch = y_b;
                            min_branch = x_b;
                        }
                        // Console.WriteLine("Hello" + total);
                        if (total + 1 >= verification && nombre_serie_X != 0 && nombre_serie_Y != 0)
                        {
                            // Console.WriteLine("Pour " + this.nom);
                            //  /   Console.WriteLine("Le dot est sur: " + dot.his_position);
                            // Console.WriteLine("Branche X");
                            // Console.WriteLine(x_b.his_type + " " + x_b.hisPoints.Count);
                            // Console.WriteLine("Branche Y");
                            // Console.WriteLine(y_b.his_type + " " + y_b.hisPoints.Count);
                            // Console.WriteLine("\n");
                            List<PointF> resp = new List<PointF>();
                            List<PointF> max_list_points = new List<PointF>();
                            if (max_branch != null && min_branch != null)
                            {
                                int alaina = verification - 2;
                                for (int i = 0; i < alaina; i++)
                                {
                                    max_list_points.Add(max_branch.hisPoints[i]);
                                }
                                resp.Add(dot.his_position);
                                resp.AddRange(max_list_points);
                                resp.AddRange(min_branch.hisPoints);
                                Console.WriteLine("List point ");
                                foreach (var item in resp)
                                {
                                    Console.WriteLine(item);
                                }
                                // throw new Exception ("nahita max");
                                return resp;
                            }
                            else
                            {
                                resp.Clear();
                                List<PointF> list_points_x = new List<PointF>();
                                List<PointF> list_points_y = new List<PointF>();

                                int alaina = (verification - 1) / 2;
                                for (int i = 0; i < alaina; i++)
                                {
                                    list_points_x.Add(x_b.hisPoints[i]);
                                }
                                for (int i = 0; i < alaina; i++)
                                {
                                    list_points_y.Add(y_b.hisPoints[i]);
                                }
                                resp.Add(dot.his_position);
                                resp.AddRange(list_points_x);
                                resp.AddRange(list_points_y);
                                return resp;
                            }
                            // Console.WriteLine("List point max");
                            // foreach (var item in max_list_points)
                            // {
                            //     Console.WriteLine(item);
                            // }
                            // throw new Exception("Nahita isika eto " + dot.his_position + " nbr_x " + nombre_serie_X + " nbr_y " + nombre_serie_Y + " max_branch " + min_branch.his_type);
                        }
                    }
                }

            }

            //debug
            // foreach (var dot in all_dots)
            // {
            //     // List<Branche> his_branche  = dot.his_branches;
            //     // List<Branche> all_x_branch  = his_branche.Where(branch => branch.his_type[0] =='x' && branch.hisPoints.Count>0).Select(b=>b).ToList();
            //     // List<Branche> all_y_branch  = his_branche.Where(branch => branch.his_type[0] =='y'&& branch.hisPoints.Count>0).Select(b=>b).ToList();
            //     // foreach (var item in all_x_branch)
            //     // {
            //     //         Console.WriteLine("tout les x " + item.his_type);
            //     // }
            //     // //information du dot
            //     Console.WriteLine("Pour " + this.nom);
            //     Console.WriteLine("Le dot est sur: " + dot.his_position);
            //     if (dot.his_branches.Count != 0)
            //     {
            //         Console.WriteLine("Les branches de cette dot: ");
            //         foreach (var branche in dot.his_branches)
            //         {
            //             if (branche.hisPoints.Count != 0)
            //             {
            //                 Console.WriteLine("type " + branche.his_type + " pointer " + branche.hisPoints.Count);
            //             }
            //             else
            //             {
            //                 Console.WriteLine("Aucune serie Disponible sur ce type " + branche.his_type);
            //             }
            //         }
            //     }
            //     else
            //     {
            //         Console.WriteLine("Pas de branche possible");
            //     }
            // Console.WriteLine("---------------------------------");
            // }
            // Console.WriteLine("\n\n\n\n");





            return null;
        }


    }
}

