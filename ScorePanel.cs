using aff;
using terrain;


public class ScorePanel : Panel
{

    public  static Label label = new Label
    {
        Location = new Point(0, 0),
        Width = 200,
        Height = 100,
        Text = String.Empty
    };
    public ScorePanel()
    {
        configurationTerrainLabel();
        configurationScorePanel();
    }

    public void configurationScorePanel()
    {
        this.Location = new Point(65, 10);
        this.Size = new Size(200, 100);
        this.BackColor = Color.LightGray;
        this.Controls.Add(label);
    }
    public  static void configurationTerrainLabel()
    {

        label.Text = "";
        label.Text += "\n  " + Program.j1.nom + " Score: " + Program.j1.score;
        label.Text += "\n  " + Program.j2.nom + " Score: " + Program.j2.score;
        label.Refresh();

    }
}