using System.Runtime.Serialization.Formatters;
using Godot;

partial class Pawn : Piece
{
    public override void _Ready()
	{
        GD.Print("PAWN");
    }

    public override BoardTileState[,] GetMoves(Vector2 actualPos, BoardTileState[,] mapArray)
    {
        if ((int)actualPos.Y - 1 >= 0 && mapArray[(int)actualPos.X, (int)actualPos.Y - 1] == BoardTileState.OPEN){
            mapArray[(int)actualPos.X, (int)actualPos.Y - 1] = BoardTileState.PIECE_MOVEMENT;

            if ((int)actualPos.Y - 2 >= 0 && mapArray[(int)actualPos.X, (int)actualPos.Y - 2] == BoardTileState.OPEN){
                mapArray[(int)actualPos.X, (int)actualPos.Y - 2] = BoardTileState.PIECE_MOVEMENT;
            }
        }
        
        return mapArray;
    }

    public override void SayHello()
    {
        GD.Print("Hello from PAWN");
    }
}