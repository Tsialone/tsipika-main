using System;
using System.Windows.Forms;
using System.Drawing;

public class MyConsole : Panel
{
    public static Label label;
    private int totalLines = 0;

    public MyConsole()
    {
        configurationLabel();
        configurationMyConsole();
    }

    public static void addLine(string line)
    {
        label.Text += "\n-" + line;
        if (label.Parent is Panel parentPanel)
        {
            parentPanel.AutoScrollPosition = new Point(0, parentPanel.VerticalScroll.Maximum); 
        }
    }

    public void configurationLabel()
    {
        label = new Label
        {
            AutoSize = true,
            ForeColor = Color.White,
            Location = new Point(10, totalLines * 20) 
        };
    }
    public void configurationMyConsole()
    {
        Location = new Point(0, 530);
        Size = new Size(1180, 100);
        BackColor = Color.Black;
        AutoScroll = true;
        this.Controls.Add(label);
    }
    public void repaint (){
        this.Invalidate();
    }
}
