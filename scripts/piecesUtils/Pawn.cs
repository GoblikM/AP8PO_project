using System.Runtime.Serialization.Formatters;
using Godot;

partial class Pawn : Piece
{
    public override string SoundPath { get =>"res://assets/music/pawn_sound.wav"; }

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
}