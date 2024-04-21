using Godot;
using System;

public partial class ChangeScene : Node
{

	[Export]
	public AnimationPlayer TransitionAnimation { get; set; }

	String To { get; set; }


	public override void _Ready(){
		GD.Print("LALALLA");
		TransitionAnimation.Play("fade_in");
	}


	private void ToLevelsMenu(){
		TransitionAnimation.Play("fade_out");
		To = "res://scenes/LevelsSelectScene.tscn";
	}

	private void ToMainMenu(){
		TransitionAnimation.Play("fade_out");
		To = "res://scenes/MenuScene.tscn";
	}


	private void ResetGameScene(){
		TransitionAnimation.Play("fade_out");
		To = GetTree().CurrentScene.SceneFilePath;
	}

	public void ToGameLevel(String sceneName){
		TransitionAnimation.Play("fade_out");
		To = sceneName;
	}


	public void _on_transition_animation_animation_finished(String anim_name){
		if (To != null){
			GetTree().ChangeSceneToFile(To);
		}
	}
}
