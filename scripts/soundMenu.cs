using Godot;
using System;

public partial class soundMenu : HBoxContainer
{
	[Export]
	Button ToggleButton { get; set; }
	[Export]
	Label SoundLabel { get; set; }
	[Export]
	Slider SoundSlider { get; set; }

	int Value { get; set; } = 20;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SoundSlider.SetValueNoSignal(Value);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void ValueChanged(int value){
		Value = value;
		SoundSlider.SetValueNoSignal(Value);
		SoundLabel.Text = Value + " %";

		ToggleButton.Icon = Value <=0 ? (CompressedTexture2D)GD.Load( "res://assets/buttons/btnMusicOff.png") : (CompressedTexture2D)GD.Load( "res://assets/buttons/btnMusicOn.png");
	}

	private void _on_sound_slider_value_changed(float value){
		ValueChanged((int)value);
	}

	private void _on_sound_toggle_button_pressed()
	{	
		if (Value <= 0){
			ValueChanged(20);
			ToggleButton.Icon = (CompressedTexture2D)GD.Load( "res://assets/buttons/btnMusicOn.png");
		} else if (Value > 0){
			ValueChanged(0);
			ToggleButton.Icon = (CompressedTexture2D)GD.Load( "res://assets/buttons/btnMusicOff.png");
		}
	}
}
