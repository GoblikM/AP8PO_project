using Godot;
using System;

public partial class SoundMenu : VolumeSetings
{
	public override void _Ready()
	{
		BusIndex = AudioServer.GetBusIndex("SoundEFX");

		float dbValue = AudioServer.GetBusVolumeDb(BusIndex);
		float value = Mathf.DbToLinear(dbValue);
		ValueChanged(value);
	}
}
