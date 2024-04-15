using Godot;

partial class Queen : Piece
{
    public override void _Ready()
	{
        GD.Print("Queen");
    }

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

    public override void SayHello()
    {
        GD.Print("Hello from Queen");
    }
}