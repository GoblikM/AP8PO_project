using Godot;
using System;
using System.IO;

public partial class ChangeScene : Node
{

	[Export]
	public AnimationPlayer TransitionAnimation { get; set; }

	String To { get; set; }


	public override void _Ready(){
		TransitionAnimation.Play("fade_in");
	}


	public void ToLevelsMenu(){
		TransitionAnimation.Play("fade_out");
		To = "res://scenes/LevelsSelectScene.tscn";
	}

	private void ToMainMenu(){
		TransitionAnimation.Play("fade_out");
		To = "res://scenes/MenuScene.tscn";
	}


	public void ResetGameScene(){
		TransitionAnimation.Play("fade_out");
		To = GetTree().CurrentScene.SceneFilePath;
	}

	public void ToGameLevel(String sceneName, bool fromLevel=false){
		if (ResourceLoader.Exists(sceneName)){
			if (!fromLevel){
				var LevelSelectSound = GetNode("/root/LevelSelectSound").GetChild<AudioStreamPlayer>(0);
				LevelSelectSound.Play();
			}

			TransitionAnimation.Play("fade_out");
			To = sceneName;
		}
		else if (fromLevel){
			ToLevelsMenu();
		}
	}


	public void _on_transition_animation_animation_finished(String anim_name){
		if (To != null){
			GetTree().ChangeSceneToFile(To);
		}
	}


	public void Quit(){
		GetTree().Quit();
	}
}
