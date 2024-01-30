using Godot;
using System;

//IDEA AREA:
//1.) What if it is liars dice, but you want to lose!
//2.) TOMORROW: Set up bluffing system for the "Warden".
//3.) Turn swap for winner going next first.
//4.) What happens when you run out of dice.
//FUTURE: Make a return statement that is a boolean and will stop this signal ->
//-> from firing if the "Warden" calls a bluff.

//5.) It's when he calls bluff on me after I already won a bluff and earned `playerFirst`.
//FUTURE: 6.) If `Warden` just bid the same way the player did but without many matching ->
//-> dice, then if he would be set to bid the same as the player again, instead he bids ->
//-> his own way.
//FUTURE: 7.) Much better way of showing that a new round has started. 


public partial class Main : Node
{
	Transform3D oldPlayerTrans;

	Node3D environment;
	PlayerCamera playerCam;
	Node3D gameStartAngle;
	Warden warden;
	Dice diceSpatial;
	Node3D bidSelect;
	Node3D startGame;
	Timer gameStartTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		environment = GetTree().Root.GetChild(0).GetChild<Node3D>(0);
		warden = GetTree().Root.GetChild(0).GetChild<Warden>(2);
		diceSpatial = GetTree().Root.GetChild(0).GetChild<Dice>(3);

		bidSelect = GetTree().Root.GetChild(0).GetChild<Node3D>(4);
		startGame = GetTree().Root.GetChild(0).GetChild<Node3D>(5);
		gameStartTimer = GetTree().Root.GetChild(0).GetChild<Timer>(6);

		// environment.Visible = false;
		// warden.Visible = false;
		// diceSpatial.Visible = false;
		// bidSelect.Visible = false;

		playerCam = GetTree().Root.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild<PlayerCamera>(0);
		oldPlayerTrans = playerCam.Transform;
		gameStartAngle = GetTree().Root.GetChild(0).GetChild<Node3D>(7);
		playerCam.Transform = gameStartAngle.Transform;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(playerCam.gameStart != true){
			Transform3D startLocation = playerCam.Transform;
			Transform3D endLocation = oldPlayerTrans;
			playerCam.Transform = startLocation.InterpolateWith(endLocation, 0.001f + (float) delta);
		}
	}

	public void _on_start_game_name_entered(){
		gameStartTimer.Start();
	}

	public void _on_game_start_timer_timeout(){
		playerCam.gameStart = false;
		environment.Visible = true;
		warden.Visible = true;
		diceSpatial.Visible = true;
		bidSelect.Visible = true;
	}

	public void _on_dice_game_end_player_win(){

	}

	public void _on_dice_game_end_player_loss(){

	}
}
