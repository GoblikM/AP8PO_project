using Godot;
using System;

public partial class GameUI : ChangeScene
{
    [Export]
	Label LabelTime { get; set; }
    [Export]
	Label LabelLevel { get; set; }

    Timer Timer { get; set; }
    int Time { get; set;}

    public override void _Ready()
	{
        GD.Print("GAMEUI");
        base.TransitionAnimation.Play("fade_in");

        Timer = GetNode<Timer>("Timer");
		Timer.Start();

        String levelNumber = GetTree().CurrentScene.SceneFilePath.GetFile().GetBaseName().Replace("level", "");
        LabelLevel.Text = "Aktuální level: " + levelNumber;
    }

    public void Timeout(){
        // Time++;
        LabelTime.Text = "Čas hry: " + TimeSpan.FromSeconds(Time++).ToString(@"hh\:mm\:ss");
    }
}
