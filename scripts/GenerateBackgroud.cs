using Godot;
using System;

public partial class GenerateBackgroud : CanvasGroup
{
	public override void _Ready()
	{
		int horizonalCount = Constants.mapWidth;
		int verticalCount = Constants.mapHeight;

		for(int h=0; h < horizonalCount; h++){
			for(int v=0; v < verticalCount; v++){
				ColorRect panel = new ColorRect();

				panel.SetPosition(new Vector2(h * Constants.tileSize, v * Constants.tileSize));
				panel.SetSize(new Vector2(Constants.tileSize, Constants.tileSize));
				
				Color color = (v+h)%2==0 ? new Color("#964d22") : new Color("#f8e7bb");
				panel.Modulate = color;

				AddChild(panel);
			}
		}
	}
}
