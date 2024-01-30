using Godot;
using System;
using System.Linq.Expressions;
using System.Xml.Serialization;

public partial class StartGame : Node3D
{
	[Signal]
	public delegate void NameEnteredEventHandler();

	string playerName = "";
	private StyleBoxFlat customStyleBox = new StyleBoxFlat();
	bool firstPlayTheme = true;
	int dbDropper = 0;
	Label3D nameLabel;
	LineEdit nameEdit;
	AnimationPlayer animPlayer;
	Timer qfTimer;
	AudioStreamPlayer3D mainThemeAudio;
    AudioStreamPlayer3D backgroundThemeAudio;
	Node3D background;
	PlayerCamera playerCam;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerCam = GetTree().Root.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild<PlayerCamera>(0);

		nameLabel = GetTree().Root.GetChild(0).GetChild(5).GetChild(0).GetChild(0).GetChild<Label3D>(0);
		animPlayer = GetTree().Root.GetChild(0).GetChild(5).GetChild<AnimationPlayer>(1);
		qfTimer = GetTree().Root.GetChild(0).GetChild(5).GetChild<Timer>(3);
		mainThemeAudio = GetTree().Root.GetChild(0).GetChild(5).GetChild<AudioStreamPlayer3D>(4);
		backgroundThemeAudio = GetTree().Root.GetChild(0).GetChild(5).GetChild<AudioStreamPlayer3D>(5);
        background = GetTree().Root.GetChild(0).GetChild(5).GetChild<Node3D>(0);
		nameEdit = GetTree().Root.GetChild(0).GetChild(5).GetChild<LineEdit>(2);

		//NOTE: Text box now wont take up space.
		nameEdit.CustomMinimumSize = new Vector2(0,0);

		customStyleBox.BgColor = new Color(0, 0, 0, 0);
		nameEdit.AddThemeStyleboxOverride("normal", customStyleBox);
		nameEdit.Modulate = new Color(1, 1, 1, 0);
		
		// nameEdit.Visible = false;

		animPlayer.Play("PaperFall");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if(firstPlayTheme == false && mainThemeAudio.GetPlaybackPosition() >= 14.40f){
		// 	mainThemeAudio.Play(4.85f);
		// }
		// else if(firstPlayTheme == true && mainThemeAudio.GetPlaybackPosition() >= 29.00f){
		// 	firstPlayTheme = false;
		// 	mainThemeAudio.Play(4.6f);
		// }

		if(IsInstanceValid(qfTimer)){
			if(qfTimer.TimeLeft > 0 && dbDropper%25 == 0){
				mainThemeAudio.VolumeDb -= 5;
			}
			dbDropper -= 1;
		}
	}

	public void _on_LineEdit_text_changed(string newText){
		nameLabel.Text = newText;
	}

	public void _on_LineEdit_text_entered(string newText){
		//Play animation here.
		animPlayer.Play("NameEntered");
		EmitSignal("NameEntered");

		SetEditableInstance(nameLabel, false);
		//Queue everything for deletion, with the `QueueFree()` function, after a short duration.
		qfTimer.Start();
	}

	public void _on_QueueFreeTimer_timeout(){
		background.QueueFree();
		animPlayer.QueueFree();
		qfTimer.QueueFree();
		nameEdit.QueueFree();
		mainThemeAudio.QueueFree();

        backgroundThemeAudio.Play();
		// mainThemeAudio.VolumeDb = -40.0f;
	}
}
