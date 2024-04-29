using Godot;
using System;

partial class Knight : Piece
{
    public override string SoundPath { get =>"res://assets/music/knight_sound.wav"; }

    public override BoardTileState[,] GetMoves(Vector2 actualPos, BoardTileState[,] mapArray)
    {
        int[] knightMoves = { -2, -1, 1, 2 };

		foreach (int dx in knightMoves){
			foreach (int dy in knightMoves){
				if (Math.Abs(dx) + Math.Abs(dy) == 3){
					int x = (int)actualPos.X + dx;
					int y = (int)actualPos.Y + dy;

					if (x >= 0 && x < Constants.mapWidth && y >= 0 && y < Constants.mapHeight){
						BoardTileState value = mapArray[x, y];

						if (value == BoardTileState.OPEN || value == BoardTileState.OPEN_TO_JUMP)
						{
							mapArray[x, y] = BoardTileState.PIECE_MOVEMENT;
						}
					}
				}
			}
		}

        return mapArray;
    }
}
