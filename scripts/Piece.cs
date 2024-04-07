using Godot;


public abstract partial class Piece : RigidBody2D
{
    public abstract BoardTileState[,] GetMoves(Vector2 actualPos, BoardTileState[,] mapArray);

    public abstract void SayHello();
}
