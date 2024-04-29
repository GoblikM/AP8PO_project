using Godot;
using System;

partial class Bishop : Piece
{
    public override string SoundPath { get =>"res://assets/music/bishop_sound.wav"; }

    public override BoardTileState[,] GetMoves(Vector2 actualPos, BoardTileState[,] mapArray)
    {
        // Diagonal
        return MovementDiagonal(actualPos, mapArray);
    }
}