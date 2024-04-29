using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class GameLevel : Node2D
{
	[Export]
	PackedScene[] Pieces {get; set; }
	[Export]
	Vector2[] StartPositions {get; set; }

	[Export]
	TileMap TileMap { get; set; }
	[Export]
	PackedScene FinishPoint { get; set; }

	[Export]
	ChangeScene GameUiNode { get; set; }
	AudioStreamPlayer MusicEffectsPlayer { get; set; }

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
	Node FinishPointInstance { get; set; }
	bool FinishReached = false;

	private CanvasGroup PossibleMoves = new CanvasGroup();
	private List<(int, int)> ActualMoves = new List<(int, int)>();
	private GameUtils GameUtils = new GameUtils();
	public override void _Ready()
	{
		MusicEffectsPlayer = GameUiNode.GetNode<AudioStreamPlayer>("MusicEffectsPlayer");

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

		FinishPointTile = TileMap.GetUsedCells(2)[0];
		FinishPointInstance = FinishPoint.Instantiate();
		TileMap.GetCellTileData(2, (Vector2I)FinishPointTile).Modulate = new Color(1,1,1,1);
	}


	private bool UpdateMap { get; set; } = true;
	private int SelectedPieceIdTemp { get; set; } = -1;
	public override void _Process(double delta)
	{
		PossibleMoves = GameUtils.AnimatePossibleMoves(PossibleMoves);

		if (!FinishReached){
			if (SelectedPieceId < 0){
				ChoosePieceWithClick();
				return;
			}
			SelectedPieceIdTemp = SelectedPieceId;

			Positions.Clear();
			foreach(var piece in PiecesInstances){
				Positions.Add(piece.GlobalPosition);
			}
			
			if (GameUtils.ArePiecesMoving(Positions, TempPositions)){
				UpdateMap = true;
			} else{
				ChoosePieceWithClick();
				if (UpdateMap == true || SelectedPieceId != SelectedPieceIdTemp){
					CleanVariables();
					SetMap();
					MapArray = PiecesInstances[SelectedPieceId].GetMoves(GameUtils.GetPositionInMap(PiecesInstances[SelectedPieceId]), MapArray);

					UpdateMap = false;
					ShowPossibleMoves();
				}
				MoveWithCorrectClick();
				
				if (GameUtils.GetPositionInMap(PiecesInstances[SelectedPieceId]).IsEqualApprox(FinishPointTile)){
					FinishReached = true;

					Vector2 pos = GameUtils.GetPositionInWord(FinishPointTile);
					FinishPointInstance.Set("position", pos);
					AddChild(FinishPointInstance);
					TileMap.GetCellTileData(2, (Vector2I)FinishPointTile).Modulate = new Color(1,1,1,0);
					FinishPointInstance.GetNode<AnimationPlayer>("AnimationPlayer").Play("finish_reached");
				}
			}
			
			TempPositions.Clear();
			foreach(var piece in PiecesInstances){
				TempPositions.Add(piece.GlobalPosition);
			}
		} else{
			Vector2 p = (Vector2)FinishPointInstance.Get("position");
			if (p.Y > (Constants.mapHeight * Constants.tileSize)){
                string levelNumber = GetTree().CurrentScene.SceneFilePath.GetFile().GetBaseName().Replace("level", "");
				GameUiNode.ToGameLevel("res://scenes/levels/level"+(levelNumber.ToInt()+1)+".tscn", true);
			}
		}
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

			if (index != null && index.Index != SelectedPieceId)
			{
				SelectedPieceId = index.Index;
				PiecesInstances[SelectedPieceId].Animate();
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
		MusicEffectsPlayer.Stream = GD.Load<AudioStream>("res://assets/music/move_sound.mp3");
		MusicEffectsPlayer.Play();

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
	private void ShowPossibleMoves(){	
		for (int x = 0; x < MapArray.GetLength(0); x++){
			for (int y = 0; y < MapArray.GetLength(1); y++){
				var value = MapArray[x, y];
				if (value == BoardTileState.PIECE_MOVEMENT){
					if (!ActualMoves.Contains((x, y))) {
                        Panel panel = GameUtils.CreatePossibleMovePanel(x, y, new Color("#000000"));
						PossibleMoves.AddChild(panel);

						ActualMoves.Add((x, y));
					}
				}				
			}
		}
	}
}
