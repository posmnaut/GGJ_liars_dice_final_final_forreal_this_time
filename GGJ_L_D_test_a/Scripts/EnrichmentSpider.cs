using Godot;
using System;

public partial class EnrichmentSpider : Node3D
{
	AnimationPlayer animationPlayer;
	
	Warden warden;
	PlayerCamera playerCam;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		warden = GetTree().Root.GetChild(0).GetChild<Warden>(2);
		playerCam = GetTree().Root.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild<PlayerCamera>(0);


		animationPlayer = GetTree().Root.GetChild(0).GetChild(10).GetChild<AnimationPlayer>(1);
		animationPlayer.Play("happy");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void _on_animation_player_animation_finished(StringName anim_name){
		if(playerCam.gameStart == true){
			animationPlayer.Play("happy");
		}
		else{
			float chanceInt = GD.Randf();
			if(chanceInt >= 0.30f){
				animationPlayer.Play("idle");
			}
			else{
				animationPlayer.Play("lean in");
			}
		}
	}

	public void _on_warden_no_eye_contact(){
		animationPlayer.Play();
	}

	public void _on_warden_player_won_bluff(){
		animationPlayer.Play("sad_001");
	}

	public void _on_warden_warden_won_bluff(){
		animationPlayer.Play("happy");
	}
}
