using Godot;
using System;

public partial class MusicMenu : VolumeSetings
{
	public override void _Ready()
	{
		BusIndex = AudioServer.GetBusIndex("Music");

		float dbValue = AudioServer.GetBusVolumeDb(BusIndex);
		float value = Mathf.DbToLinear(dbValue);
		ValueChanged(value);
	}
}
