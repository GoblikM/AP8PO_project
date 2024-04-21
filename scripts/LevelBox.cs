using Godot;
using System;

public partial class LevelBox : Button
{
	[Export]
	int LevelNumber { get; set; } = 1;
	[Export]
	bool IsLocked { get; set; } = false;


	public void SetLevel(int level){
		LevelNumber = level;

		var label = (Label)GetChild(0);
		label.Text = level.ToString();
	}

	public void SetIsLocked(bool isLocked){
		IsLocked = isLocked;
	
		var locek = (MarginContainer)GetChild(1);
		locek.Visible = IsLocked;
	}


	private void StartGame(){
		Node node = GetNode("/root/LevelsSelectScene/");
		if (node is ChangeScene script){
			script.ToGameLevel("res://scenes/levels/level"+LevelNumber+".tscn");
		}
	}
}
