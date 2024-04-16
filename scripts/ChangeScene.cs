using Godot;
using System;

public partial class ChangeScene : Node
{

    private void _on_button_pressed(){
        GetTree().ChangeSceneToFile("res://scenes/LevelsSelectScene.tscn");
    }

    private void start1(){
        GetTree().ChangeSceneToFile("res://scenes/levels/level1.tscn");
    }

    private void goToMenu(){
        GetTree().ChangeSceneToFile("res://scenes/LevelsSelectScene.tscn");
    }

    private void ResetGameScene(){
        GetTree().ChangeSceneToFile(GetTree().CurrentScene.SceneFilePath);
    }
}
