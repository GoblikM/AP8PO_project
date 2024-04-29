using Godot;

partial class Queen : Piece
{
    public override string SoundPath { get =>"res://assets/music/queen_sound.wav"; }

    public override BoardTileState[,] GetMoves(Vector2 actualPos, BoardTileState[,] mapArray)
    {
        // X axis
		mapArray = MovementAxisX(actualPos, mapArray);
		
		// Y axis
		mapArray = MovementAxisY(actualPos, mapArray);

        // Diagonal
        mapArray = MovementDiagonal(actualPos, mapArray);

        return mapArray;
    }
}