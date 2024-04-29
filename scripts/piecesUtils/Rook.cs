using Godot;

partial class Rook : Piece
{
    public override void _Ready()
	{
        GD.Print("Rook");
    }

    public override BoardTileState[,] GetMoves(Vector2 actualPos, BoardTileState[,] mapArray)
    {	
		// X axis
		mapArray = MovementAxisX(actualPos, mapArray);
		
		// Y axis
		mapArray = MovementAxisY(actualPos, mapArray);

        return mapArray;
    }

    public override void SayHello()
    {
        GD.Print("Hello from ROOK");
    }
}
