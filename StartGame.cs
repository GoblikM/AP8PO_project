using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public enum BoardTileState{
	BLOCKED,
	OPEN,
	PIECE_MOVEMENT
}


public partial class StartGame : Node2D
{
	[Export]
	PackedScene[] Pieces {get; set;}
	[Export]
	Vector2[] Positions {get; set;}


	private List<Piece> PiecesInstances { get; set; } = new List<Piece>();
	private int SelectedPieceId { get; set; } = -1;
	private Vector2 SelectedPiecePosition { get; set; }
	private Vector2 TempSelectedPiecePosition { get; set; }

	private BoardTileState[,] MapArray = new BoardTileState[Constants.mapWidth, Constants.mapHeight];

	private CanvasGroup PossibleMoves = new CanvasGroup();
	private List<(int, int)> ActualMoves = new List<(int, int)>();

	public override void _Ready()
	{
		SetMap();

		foreach (var (item, index) in Pieces.Select((item, index) => (item, index))){
			Piece pi = (Piece)item.Instantiate();

			pi.SayHello();
			PiecesInstances.Add(pi);	
			PiecesInstances[index].Set("position", new Vector2(Positions[index].X * Constants.tileSize + (Constants.tileSize/2), Positions[index].Y * Constants.tileSize + (Constants.tileSize/2)));
			AddChild(pi);		
		}

		AddChild(PossibleMoves);
	}


	private bool UpdateMap { get; set; } = true;
	public override void _Process(double delta)
	{
		if (SelectedPieceId < 0){
			ChoosePieceWithClick();
			return;
		}

		SelectedPiecePosition = PiecesInstances[SelectedPieceId].Position;

		if (!TempSelectedPiecePosition.IsEqualApprox(SelectedPiecePosition)){
			UpdateMap = true;
		} else{
			if (UpdateMap == true){
				CleanVariables();
				SetMap();
				MapArray = PiecesInstances[SelectedPieceId].GetMoves(GlobalPositionToMapPos(), MapArray);

				UpdateMap = false;
			}
			ChoosePieceWithClick();


			ShowPossibleMoves();
			MoveWithCorrectClick();
		}

		TempSelectedPiecePosition = SelectedPiecePosition;
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


	private Vector2 GlobalPositionToMapPos(){
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
		MapArray = PiecesInstances[SelectedPieceId].GetMoves(SelectedPiecePosition, MapArray);

		PiecesInstances[SelectedPieceId].Set("position", new Vector2(SelectedPiecePosition.X * Constants.tileSize + (Constants.tileSize/2), SelectedPiecePosition.Y * Constants.tileSize + (Constants.tileSize/2)));
		PiecesInstances[SelectedPieceId].MoveAndCollide(new Vector2(0, 1));
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
				MapArray[x, y] = BoardTileState.OPEN;
			}
		}

		foreach(var piece in PiecesInstances){
			int x = (int)Math.Ceiling(piece.Position.X / Constants.tileSize) - 1;
			int y = (int)Math.Ceiling(piece.Position.Y / Constants.tileSize) - 1;
			MapArray[x, y] = BoardTileState.BLOCKED;
		}
	}
}
