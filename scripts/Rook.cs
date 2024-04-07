using Godot;

partial class Rook : Piece
{
    public override void _Ready()
	{
        GD.Print("Rook");
    }

    public override BoardTileState[,] GetMoves(Vector2 actualPos, BoardTileState[,] mapArray)
    {	
		int x = (int)actualPos.X + 1;
		while(x < Constants.mapWidth){
			BoardTileState value = mapArray[x, (int)actualPos.Y];
			
			if (value == BoardTileState.OPEN){
				mapArray[x, (int)actualPos.Y] = BoardTileState.PIECE_MOVEMENT;
			} else{
				break;
			}
			x++;
		}

		x = (int)actualPos.X - 1;
		while(x >= 0){
			BoardTileState value = mapArray[x, (int)actualPos.Y];
			
			if (value == BoardTileState.OPEN){
				mapArray[x, (int)actualPos.Y] = BoardTileState.PIECE_MOVEMENT;
			} else{
				break;
			}

			x--;
		}


		int y = (int)actualPos.Y +1;
		while(y < Constants.mapHeight){
			BoardTileState value = mapArray[(int)actualPos.X, y];
			
			if (value == BoardTileState.OPEN){
				mapArray[(int)actualPos.X, y] = BoardTileState.PIECE_MOVEMENT;
			} else{
				break;
			}
			y++;
		}
		y = (int)actualPos.Y - 1;
		while(y >= 0){
			BoardTileState value = mapArray[(int)actualPos.X, y];
			
			if (value == BoardTileState.OPEN){
				mapArray[(int)actualPos.X, y] = BoardTileState.PIECE_MOVEMENT;
			} else{
				break;
			}

			y--;
		}

        return mapArray;
    }

    public override void SayHello()
    {
        GD.Print("Hello from ROOK");
    }
}
