using System;
using System.Collections.Generic;
using Godot;

public class GameUtils{

    public static Panel CreatePossibleMovePanel(int x, int y, Color color){
        Panel panel = new Panel();

        panel.SetPosition(new Vector2(x * Constants.tileSize + Constants.tileSize/4, y * Constants.tileSize + Constants.tileSize/4));
        panel.SetSize(new Vector2(Constants.tileSize/2, Constants.tileSize/2));
        StyleBoxFlat styleBox = new();
        styleBox.SetCornerRadiusAll(90);
        panel.AddThemeStyleboxOverride("panel", styleBox);

        panel.Modulate = color;

        return panel;
    }


    private float targetAlpha = 0.7f;
    private bool increasing = true;
	public CanvasGroup AnimatePossibleMoves(CanvasGroup PossibleMoves, float breathingSpeed = 0.8f, float delta = 0.01f){
		if (increasing){
            targetAlpha += breathingSpeed * delta;
            if (targetAlpha >= 1.0f)
            {
                targetAlpha = 1.0f;
                increasing = false;
            }
        } else{
            targetAlpha -= breathingSpeed * delta;
            if (targetAlpha <= 0.5f)
            {
                targetAlpha = 0.5f;
                increasing = true;
            }
        }
        PossibleMoves.Modulate = new Color(1, 1, 1, targetAlpha);
        return PossibleMoves; 
	}


    public static bool ArePiecesMoving(List<Vector2> Positions, List<Vector2> TempPositions)
	{
		if (Positions.Count != TempPositions.Count)
			return true;

		for (int i = 0; i < Positions.Count; i++)
		{
			if (Positions[i] != TempPositions[i])
				return true;
		}

		return false;
	}

    public static Vector2 GetPositionInMap(Piece wordPoint){
		int x = (int)Math.Ceiling(wordPoint.GlobalPosition.X / Constants.tileSize) - 1;
		int y = (int)Math.Ceiling(wordPoint.GlobalPosition.Y / Constants.tileSize) - 1;
		
		return new Vector2(x, y);
	}
	public static Vector2 GetPositionInWord(Vector2 mapPoint){
		int x = (int)Math.Ceiling(mapPoint.X * Constants.tileSize) + (Constants.tileSize/2);
		int y = (int)Math.Ceiling(mapPoint.Y * Constants.tileSize) + (Constants.tileSize/2);
		
		return new Vector2(x, y);
	}
}

