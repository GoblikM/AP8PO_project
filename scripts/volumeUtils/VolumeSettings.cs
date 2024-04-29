using Godot;
using System;


public abstract partial class VolumeSetings : HBoxContainer{
    [Export]
	Button ToggleButton { get; set; }
	[Export]
	Label Label { get; set; }
	[Export]
	Slider Slider { get; set; }

	float Value { get; set; } = 0f;
	float DbValue { get; set; } = 0;
	protected int BusIndex { get; set; }


    protected void ValueChanged(float value){
		Value = value;
		DbValue = Mathf.LinearToDb(Value);
		AudioServer.SetBusVolumeDb(BusIndex, DbValue);

		Slider.SetValueNoSignal(Value);
		Label.Text = Math.Round(Value*100) + " %";

		ToggleButton.Icon = Value <=0 ? (CompressedTexture2D)GD.Load("res://assets/buttons/btnMusicOff.png") : (CompressedTexture2D)GD.Load("res://assets/buttons/btnMusicOn.png");
	}


    protected void _on_slider_value_changed(float value){
		ValueChanged(value);
	}


	protected void _on_toggle_button_pressed()
	{	
		if (Value <= 0){
			ValueChanged(0.2f);
			ToggleButton.Icon = (CompressedTexture2D)GD.Load( "res://assets/buttons/btnMusicOn.png");
		} else if (Value > 0){
			ValueChanged(0);
			ToggleButton.Icon = (CompressedTexture2D)GD.Load( "res://assets/buttons/btnMusicOff.png");
		}
	}
}