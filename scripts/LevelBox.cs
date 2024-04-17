using Godot;
using System;

public partial class LevelBox : PanelContainer
{
	[Export]
	int LevelNumber { get; set; } = 1;
	[Export]
	bool IsLocked { get; set; } = false;

	public LevelBox(){}

	public void SetLevel(int level){
		LevelNumber = level;

		var label = (Label)GetChild(0);
		label.Text = level.ToString();
	}

	public void SetIsLocked(bool isLocked){
		IsLocked = isLocked;
	
		var locek = (MarginContainer)GetChild(2);
		locek.Visible = IsLocked;
	}


	private void StartGame(){
		GetTree().ChangeSceneToFile("res://scenes/levels/level"+LevelNumber+".tscn");
	}
}
