using Godot;
using System;

public partial class LevelGrid : GridContainer
{
	public override void _Ready()
	{
		int i = 1;
		foreach(Node n in GetChildren()){
            if (n is LevelBox script)
            {
                script.SetLevel(i);
                if (ResourceLoader.Exists("res://scenes/levels/level"+i+".tscn")){
                    script.SetIsLocked(false);
                } else{
                    script.SetIsLocked(true);
                }
                i++;
            }

        }
	}
}
