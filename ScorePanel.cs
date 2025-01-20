using aff;
using terrain;


public class ScorePanel : Panel
{

    public Label label   = new Label
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
    public void configurationTerrainLabel()
    {
        Score score = new Score();
        
            label.Text ="";
            label.Text += "\n  Socre"  ;
            label.Text += "\n  Socre"  ;
            label.Refresh();

        }
}