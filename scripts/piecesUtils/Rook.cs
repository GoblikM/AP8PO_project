using Godot;

partial class Rook : Piece
{
    public override string SoundPath { get =>"res://assets/music/rook_sound.wav"; }

    public override BoardTileState[,] GetMoves(Vector2 actualPos, BoardTileState[,] mapArray)
    {	
		// X axis
		mapArray = MovementAxisX(actualPos, mapArray);
		
		// Y axis
		mapArray = MovementAxisY(actualPos, mapArray);

        return mapArray;
    }
}
