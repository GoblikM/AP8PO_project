using Godot;
using System;

public partial class King : Piece
{
    public override string SoundPath { get =>"res://assets/music/king_sound.wav"; }

    public override BoardTileState[,] GetMoves(Vector2 actualPos, BoardTileState[,] mapArray)
    {
		for (int x = (int)actualPos.X - 1; x <= actualPos.X + 1; x++){
            for (int y = (int)actualPos.Y - 1; y <= actualPos.Y + 1; y++){
                if (x >= 0 && x < Constants.mapWidth && y >= 0 && y < Constants.mapHeight){
					BoardTileState value = mapArray[x, y];
					if (value == BoardTileState.OPEN){
						mapArray[x, y] = BoardTileState.PIECE_MOVEMENT;
					}
                }
            }
        }

        return mapArray;
    }
}
