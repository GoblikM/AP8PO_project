using Godot;
using System;

partial class Bishop : Piece
{
    public override void _Ready()
	{
        GD.Print("Bishop");
    }

    public override BoardTileState[,] GetMoves(Vector2 actualPos, BoardTileState[,] mapArray)
    {
        // Diagonal
        return MovementDiagonal(actualPos, mapArray);
    }

    public override void SayHello()
    {
        GD.Print("Hello from Bishop");
    }
}