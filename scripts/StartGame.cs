using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public enum BoardTileState{
	BLOCKED,
	OPEN,
	OPEN_TO_JUMP,
	PIECE_MOVEMENT
}


public partial class StartGame : Node2D
{
	[Export]
	PackedScene[] Pieces {get; set; }
	[Export]
	Vector2[] StartPositions {get; set; }

	[Export]
	TileMap TileMap { get; set; }


	private List<Piece> PiecesInstances { get; set; } = new List<Piece>();
	private int SelectedPieceId { get; set; } = -1;
	private Vector2 SelectedPiecePosition { get; set; }
	private Vector2 TempSelectedPiecePosition { get; set; }

	private List<Vector2> Positions { get; set; } = new List<Vector2>();
	private List<Vector2> TempPositions { get; set; } = new List<Vector2>();


	private BoardTileState[,] MapArray = new BoardTileState[Constants.mapWidth, Constants.mapHeight];
	private List<Vector2> BlockTiles = new List<Vector2>();
	private List<Vector2> OpenTiles = new List<Vector2>();
	Vector2 FinishPointTile {get; set; }

	private CanvasGroup PossibleMoves = new CanvasGroup();
	private List<(int, int)> ActualMoves = new List<(int, int)>();

	public override void _Ready()
	{
		SetMap();

		foreach (var (item, index) in Pieces.Select((item, index) => (item, index))){
			Piece pi = (Piece)item.Instantiate();

			pi.SayHello();
			PiecesInstances.Add(pi);	
			PiecesInstances[index].Set("position", new Vector2(StartPositions[index].X * Constants.tileSize + (Constants.tileSize/2), StartPositions[index].Y * Constants.tileSize + (Constants.tileSize/2)));
			AddChild(pi);		
		}

		AddChild(PossibleMoves);


		foreach(var tile in TileMap.GetUsedCells(0)){
			BlockTiles.Add(new Vector2(tile.X, tile.Y));
		}
		// foreach(var tile in TileMap.GetUsedCells(1)){
		// 	OpenTiles.Add(new Vector2(tile.X, tile.Y));
		// }
		FinishPointTile = TileMap.GetUsedCells(2)[0];
	}

	private bool ArePiecesMoving()
    {
        if (Positions.Count != TempPositions.Count)
            return true;

        for (int i = 0; i < Positions.Count; i++)
        {
            if (Positions[i] != TempPositions[i])
                return true;
        }

        return false;
    }

	private bool UpdateMap { get; set; } = true;
	private int SelectedPieceIdTemp { get; set; } = -1;
	public override void _Process(double delta)
	{
		if (SelectedPieceId < 0){
			ChoosePieceWithClick();
			return;
		}
		SelectedPieceIdTemp = SelectedPieceId;

		Positions.Clear();
		foreach(var piece in PiecesInstances){
			Positions.Add(piece.GlobalPosition);
		}
		
		if (ArePiecesMoving())
        {
			UpdateMap = true;
        }
        else
        {
			ChoosePieceWithClick();
			if (UpdateMap == true || SelectedPieceId != SelectedPieceIdTemp){
				CleanVariables();
				SetMap();
				MapArray = PiecesInstances[SelectedPieceId].GetMoves(GetSelectedPiecePosition(), MapArray);

				UpdateMap = false;
				ShowPossibleMoves();
			}
			
			MoveWithCorrectClick();
			
			if (GetSelectedPiecePosition().IsEqualApprox(FinishPointTile)){
				GD.Print("KONEC");
			}
        }
		
		TempPositions.Clear();
		foreach(var piece in PiecesInstances){
			TempPositions.Add(piece.GlobalPosition);
		}
	}


	private void ShowPossibleMoves(){	
		for (int x = 0; x < MapArray.GetLength(0); x++){
			for (int y = 0; y < MapArray.GetLength(1); y++){
				var value = MapArray[x, y];
				if (value == BoardTileState.PIECE_MOVEMENT){
					if (!ActualMoves.Contains((x, y))) {
						ColorRect panel = new ColorRect();
						panel.SetPosition(new Vector2(x * Constants.tileSize + Constants.tileSize/4, y * Constants.tileSize + Constants.tileSize/4));
						panel.SetSize(new Vector2(Constants.tileSize/2, Constants.tileSize/2));
						
						Color color = new Color("#000000");
						panel.Modulate = color;

						PossibleMoves.AddChild(panel);
						ActualMoves.Add((x, y));
					}
				}				
			}
		}
	}


	private Vector2 GetSelectedPiecePosition(){
		int x = (int)Math.Ceiling(PiecesInstances[SelectedPieceId].GlobalPosition.X / Constants.tileSize) - 1;
		int y = (int)Math.Ceiling(PiecesInstances[SelectedPieceId].GlobalPosition.Y / Constants.tileSize) - 1;
		
		return new Vector2(x, y);
	}

	private void ChoosePieceWithClick(){
		Vector2 mousePos = GetViewport().GetMousePosition();

		if (Input.IsActionJustPressed("click")){
			int x = (int)Math.Ceiling(mousePos.X / Constants.tileSize) - 1;
			int y = (int)Math.Ceiling(mousePos.Y / Constants.tileSize) - 1;

			var index = PiecesInstances
				.Select((item, index) => new { Item = item, Index = index })
				.FirstOrDefault(it => {
					int it_x = (int)Math.Ceiling(it.Item.Position.X / Constants.tileSize) - 1;
					int it_y = (int)Math.Ceiling(it.Item.Position.Y / Constants.tileSize) - 1;
					return new Vector2(it_x, it_y) == new Vector2(x, y);
				});

			if (index != null)
			{
				SelectedPieceId = index.Index;
			}
			
		}
	}

	private void MoveWithCorrectClick(){
		Vector2 mousePos = GetViewport().GetMousePosition();

		if (Input.IsActionJustPressed("click")){
			int x = (int)Math.Ceiling(mousePos.X / Constants.tileSize) - 1;
			int y = (int)Math.Ceiling(mousePos.Y / Constants.tileSize) - 1;

			if (ActualMoves.Any(t => t == (x, y))){
				Move(x, y);
			}
		}
	}

	private void Move(int x, int y){
		CleanVariables();
		SetMap();

		SelectedPiecePosition = new Vector2(x, y);
		PiecesInstances[SelectedPieceId].Set("position", new Vector2(SelectedPiecePosition.X * Constants.tileSize + (Constants.tileSize/2), SelectedPiecePosition.Y * Constants.tileSize + (Constants.tileSize/2)));
		MapArray = PiecesInstances[SelectedPieceId].GetMoves(SelectedPiecePosition, MapArray);

		foreach(var piece in PiecesInstances){
			piece.MoveAndCollide(new Vector2(0, 1));
			piece.Rotate(0);
		}
	}

	private void CleanVariables(){
		foreach (Node child in PossibleMoves.GetChildren())
		{
			PossibleMoves.RemoveChild(child);
		}
		ActualMoves.Clear();
	}

	private void SetMap(){
		for (int x = 0; x < MapArray.GetLength(0); x++){
			for (int y = 0; y < MapArray.GetLength(1); y++){
				if (BlockTiles.Any(it => it.IsEqualApprox(new Vector2(x, y)))){
					MapArray[x, y] = BoardTileState.BLOCKED;
				} else if (OpenTiles.Any(it => it.IsEqualApprox(new Vector2(x, y)))){
					MapArray[x, y] = BoardTileState.OPEN_TO_JUMP;
				}
				else{
					MapArray[x, y] = BoardTileState.OPEN;
				}
			}
		}

		foreach(var piece in PiecesInstances){
			int x = (int)Math.Ceiling(piece.GlobalPosition.X / Constants.tileSize) - 1;
			int y = (int)Math.Ceiling(piece.GlobalPosition.Y / Constants.tileSize) - 1;

			MapArray[x, y] = BoardTileState.BLOCKED;
		}
	}
}
