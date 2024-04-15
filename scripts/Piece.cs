using Godot;


public abstract partial class Piece : RigidBody2D
{
    public abstract BoardTileState[,] GetMoves(Vector2 actualPos, BoardTileState[,] mapArray);

    public abstract void SayHello();


    protected static BoardTileState[,] MovementAxisX(Vector2 actualPos, BoardTileState[,] mapArray){
        int[] directionsX = { -1, 1 };
		foreach (int direction in directionsX){
			int x = (int)actualPos.X + direction;

			while (x >= 0 && x < Constants.mapWidth){
				BoardTileState value = mapArray[x, (int)actualPos.Y];

				if (value == BoardTileState.OPEN){
					mapArray[x, (int)actualPos.Y] = BoardTileState.PIECE_MOVEMENT;
				}
				else if (value != BoardTileState.OPEN_TO_JUMP){
					break;
				}
				x += direction;
			}
		}
        return mapArray;
    }

    protected static BoardTileState[,] MovementAxisY(Vector2 actualPos, BoardTileState[,] mapArray){
        int[] directionsY = { -1, 1 };
		foreach (int direction in directionsY){
			int y = (int)actualPos.Y + direction;

			while (y >= 0 && y < Constants.mapHeight){
				BoardTileState value = mapArray[(int)actualPos.X, y];

				if (value == BoardTileState.OPEN){
					mapArray[(int)actualPos.X, y] = BoardTileState.PIECE_MOVEMENT;
				}
				else if (value != BoardTileState.OPEN_TO_JUMP){
					break;
				}
				y += direction;
			}
		}
        return mapArray;
    }

    protected static BoardTileState[,] MovementDiagonal(Vector2 actualPos, BoardTileState[,] mapArray){
        for (int dx = -1; dx <= 1; dx += 2){
            for (int dy = -1; dy <= 1; dy += 2){
                int x = (int)actualPos.X + dx;
                int y = (int)actualPos.Y + dy;

                while (x >= 0 && x < Constants.mapWidth && y >= 0 && y < Constants.mapHeight){
                    BoardTileState value = mapArray[x, y];
                    
					if (value == BoardTileState.OPEN){
						mapArray[x, y] = BoardTileState.PIECE_MOVEMENT;
					} 
                    else if (value != BoardTileState.OPEN_TO_JUMP){
					    break;
				    }
                    
                    x += dx;
                    y += dy;
                }
            }
        }
        return mapArray;
    }
}
