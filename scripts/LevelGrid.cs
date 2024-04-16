using Godot;
using System;

public partial class LevelGrid : GridContainer
{
	public override void _Ready()
	{
		int i = 1;
		foreach(Node n in GetChildren()){
			LevelBox script = n as LevelBox;
			script.SetLevel(i);
			script.SetIsLocked(true);

			i++;
		}
	}
}
