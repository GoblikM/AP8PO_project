using Godot;
using System;

public partial class ChangeScene : Node
{
	private void ToLevelsMenu(){
		GetTree().ChangeSceneToFile("res://scenes/LevelsSelectScene.tscn");
	}

	private void ToMainMenu(){
		GetTree().ChangeSceneToFile("res://scenes/MenuScene.tscn");
	}


	private void ResetGameScene(){
		GetTree().ChangeSceneToFile(GetTree().CurrentScene.SceneFilePath);
	}
}
