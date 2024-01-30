using Godot;
using System;

public partial class Dice : Node3D
{
    [Signal]
    public delegate void PlayerWonBluffEventHandler();
    [Signal]
    public delegate void WardenWonBluffEventHandler();
    [Signal]
    public delegate void RoundTwoStartEventHandler(bool playerWonRound);
    [Signal]
    public delegate void RoundThreeStartEventHandler(bool playerWonRound);
    [Signal]
    public delegate void GameEndPlayerLossEventHandler();
    [Signal]
    public delegate void GameEndPlayerWinEventHandler();

    private Random ranNumGen = new Random();
    public bool shakenOnce = false;
    int[] playerDiceArray = new int[5];
    public int roundNum = 1;
    bool playerWonRound;
    public int playerLifeCount = 3;
    bool diceFell = false;
    bool dicepreviouslyFell = false;

    Label3D dieLab1;
    Label3D dieLab2;
    Label3D dieLab3;
    Label3D dieLab4;
    Label3D dieLab5;
    AudioStreamPlayer3D cupShakeAudio;
    Label3D freqLabel;
    Label3D faceLabel;
    AnimationPlayer animation1;
    AnimationPlayer animation2;
    AnimationPlayer animation3;
    AnimationPlayer animation4;
    AnimationPlayer animation5;
    AnimationPlayer animationf1;
    AnimationPlayer animationf2;

    Warden warden;
    PlayerCamera playerCam;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        dieLab1 = GetTree().Root.GetChild(0).GetChild(3).GetChild<Label3D>(0);
        dieLab2 = GetTree().Root.GetChild(0).GetChild(3).GetChild<Label3D>(1);
        dieLab3 = GetTree().Root.GetChild(0).GetChild(3).GetChild<Label3D>(2);
        dieLab4 = GetTree().Root.GetChild(0).GetChild(3).GetChild<Label3D>(3);
        dieLab5 = GetTree().Root.GetChild(0).GetChild(3).GetChild<Label3D>(4);

        animation1 = GetTree().Root.GetChild(0).GetChild(3).GetChild(0).GetChild<AnimationPlayer>(1);
        animation2 = GetTree().Root.GetChild(0).GetChild(3).GetChild(1).GetChild<AnimationPlayer>(1);
        animation3 = GetTree().Root.GetChild(0).GetChild(3).GetChild(2).GetChild<AnimationPlayer>(1);
        animation4 = GetTree().Root.GetChild(0).GetChild(3).GetChild(3).GetChild<AnimationPlayer>(1);
        animation5 = GetTree().Root.GetChild(0).GetChild(3).GetChild(4).GetChild<AnimationPlayer>(1);
        animationf1 = GetTree().Root.GetChild(0).GetChild(3).GetChild(14).GetChild(0).GetChild<AnimationPlayer>(0);
        animationf2 = GetTree().Root.GetChild(0).GetChild(3).GetChild(14).GetChild(1).GetChild<AnimationPlayer>(0);

        dieLab1.Visible = false;
        dieLab2.Visible = false;
        dieLab3.Visible = false;
        dieLab4.Visible = false;
        dieLab5.Visible = false;

        cupShakeAudio = GetTree().Root.GetChild(0).GetChild(3).GetChild<AudioStreamPlayer3D>(5);
        freqLabel = GetTree().Root.GetChild(0).GetChild(3).GetChild<Label3D>(6);
        faceLabel = GetTree().Root.GetChild(0).GetChild(3).GetChild<Label3D>(7);
        
        warden = GetTree().Root.GetChild(0).GetChild<Warden>(2);
        playerCam = GetTree().Root.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild<PlayerCamera>(0);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        freqLabel.Text = playerCam.playfreqBid.ToString();
        faceLabel.Text = playerCam.playfaceBid.ToString();
    }

    public void _on_player_camera_cup_shaken(){
        shakenOnce = true;

        cupShakeAudio.Play();

        if(diceFell == false && roundNum == 2){
            diceFell = true;
            dicepreviouslyFell = true;
            animationf1.Play("Falling");
            animationf2.Play("Falling");
            playerDiceArray = new int[7];
        }
        else if(dicepreviouslyFell == true && roundNum == 2){
            playerDiceArray[5] = 5;
            playerDiceArray[6] = 4;
            for(int i = 0; i < playerDiceArray.Length-2; i++){
            playerDiceArray[i] = ranNumGen.Next(1, 7);
            }
        }
        else{
        for(int i = 0; i < playerDiceArray.Length; i++){
            playerDiceArray[i] = ranNumGen.Next(1, 7);

            if(i == 0){
                // dieLab1.Text = playerDiceArray[i].ToString();
            }
            else if(i == 1){
                // dieLab2.Text = playerDiceArray[i].ToString();
            }
            else if(i == 2){
                // dieLab3.Text = playerDiceArray[i].ToString();
            }
            else if(i == 3){
                // dieLab4.Text = playerDiceArray[i].ToString();
            }
            else if(i == 4){
                // dieLab5.Text = playerDiceArray[i].ToString();
            }
        }
        }

        // dieLab1.Text = ranNumGen.Next(1, 7).ToString();
        // dieLab2.Text = ranNumGen.Next(1, 7).ToString();
        // dieLab3.Text = ranNumGen.Next(1, 7).ToString();
        // dieLab4.Text = ranNumGen.Next(1, 7).ToString();
        // dieLab5.Text = ranNumGen.Next(1, 7).ToString();
    }

    public void _on_player_camera_finger_cup_shaken(){
        cupShakeAudio.Play();

        for(int i = 0; i < playerDiceArray.Length; i++){
            playerDiceArray[i] = ranNumGen.Next(1, 7);
        }
    }

    public void _on_player_camera_cup_lift(){
        for(int i = 0; i < playerDiceArray.Length; i++){
            GD.Print(i + " " + playerDiceArray[i]);
            if(i == 0){
                dieLab1.Visible = true;
                string a1 = playerDiceArray[0].ToString();
                animation1.Play("face" + a1);
                GD.Print("face" + a1);
            }
            else if(i == 1){
                dieLab2.Visible = true;
                string a2 = playerDiceArray[1].ToString();
                animation2.Play("face" + a2);
            }
            else if(i == 2){
                dieLab3.Visible = true;
                string a3 = playerDiceArray[2].ToString();
                animation3.Play("face" + a3);
            }
            else if(i == 3){
                dieLab4.Visible = true;
                string a4 = playerDiceArray[3].ToString();
                animation4.Play("face" + a4);
            }
            else if(i == 4){
                dieLab5.Visible = true;
                string a5 = playerDiceArray[4].ToString();
                animation5.Play("face" + a5);
            }
        }
    }

    public void _on_player_camera_cup_down(){
        dieLab1.Visible = false;
        dieLab2.Visible = false;
        dieLab3.Visible = false;
        dieLab4.Visible = false;
        dieLab5.Visible = false;
    }

    //IMPORTANT NOTE: I am using the `Dice` Class as the mediator between bluffs because ->
    //-> it can easily acquire and change both the players and "Warden"s dice.
    //Player calls bluff:
    public void _on_PlayerCamera_BluffInputHandler(int wardenFreq, int wardenFace){
        int[] totalFreqArray = new int[6];
        if(roundNum == 2){
            totalFreqArray = new int[8];
        }

        for(int i = 0; i < playerDiceArray.Length; i++){
            int dieFaceP = playerDiceArray[i] - 1;//Subtract `1` to fit indicies better.
            GD.Print("Bluff Dice face:" + dieFaceP);
            GD.Print(totalFreqArray.Length);
            GD.Print(playerDiceArray.Length);
            totalFreqArray[dieFaceP] += 1;
        }

        for(int i = 0; i < warden.dieArray.Length; i++){
            int dieFaceW = warden.dieArray[i] - 1;//Subtract `1` to fit indicies better.
            totalFreqArray[dieFaceW] += 1;
        }

        if(totalFreqArray[wardenFace-1] < wardenFreq){
            GD.Print("PLAYER WON THE BLUFF");
            warden.playerFirst = true;

            if(warden.dieArray.Length > 1){
                EmitSignal("PlayerWonBluff");
            }
            else if(warden.dieArray.Length <= 1 && roundNum == 1){
                roundNum = 2;
                playerWonRound = true;
                playerDiceArray = new int[5];

                EmitSignal("RoundTwoStart", playerWonRound);
            }
            else if(warden.dieArray.Length <= 1 && roundNum == 2){
                roundNum = 3;
                playerWonRound = true;
                playerDiceArray = new int[5];
                
                EmitSignal("RoundThreeStart", playerWonRound);
            }
            else if(warden.dieArray.Length <= 1 && roundNum == 3){
                //Emit "GameEnd" Signal.
                EmitSignal("GameEndPlayerWin");
            }
        }
        else{
            GD.Print("WARDEN WON THE BLUFF");
            playerDiceArray = new int[playerDiceArray.Length-1];//Remove one die from the ->
            //-> players cup by making a new `playerDiceArray` that has `Length` one less ->
            //-> than previous `Length`.
            GD.Print("PLAYER DICE AMOUT: " + playerDiceArray.Length);
            warden.playerFirst = false;

            if(playerDiceArray.Length > 1){
                EmitSignal("WardenWonBluff");
            }
            else if(playerDiceArray.Length <= 1 && roundNum == 1){
                roundNum = 2;
                playerWonRound = false;
                playerDiceArray = new int[5];

                EmitSignal("RoundTwoStart", playerWonRound);
            }
            else if(playerDiceArray.Length <= 1 && roundNum == 2){
                roundNum = 3;
                playerWonRound = false;
                playerDiceArray = new int[5];

                EmitSignal("RoundThreeStart", playerWonRound);
            }
            else if(roundNum == 3 && playerLifeCount == 1){
                //ADD: Player trips balls to death.

                //Emit "GameEnd" Signal.
                EmitSignal("GameEndPlayerLoss");
            }
            else if(roundNum == 3 && playerLifeCount > 1){
                //ADD: Player trips balls Signal.

                EmitSignal("WardenWonBluff");
            }
        }
    }
    
    //"Warden" calls bluff:
    public void _on_warden_w_bluff(){
        // warden.wardenLabel.Text = "CALLING [REDACTED]'s BLUFF!";
        // warden.wardenLabel.Visible = true;
        int[] totalFreqArray = new int[6];
        if(roundNum == 2){
            totalFreqArray = new int[8];
        }

        for(int i = 0; i < playerDiceArray.Length; i++){
            int dieFaceP = playerDiceArray[i] - 1;//Subtract `1` to fit indicies better.
            totalFreqArray[dieFaceP] += 1;
        }

        for(int i = 0; i < warden.dieArray.Length; i++){
            int dieFaceW = warden.dieArray[i] - 1;//Subtract `1` to fit indicies better.
            totalFreqArray[dieFaceW] += 1;
        }

        if(totalFreqArray[playerCam.playfaceBid-1] < playerCam.playfreqBid){
            GD.Print("WARDEN WON THE BLUFF");
            playerDiceArray = new int[playerDiceArray.Length-1];//Remove one die from the ->
            //-> players cup by making a new `playerDiceArray` that has `Length` one less ->
            //-> than previous `Length`.
            GD.Print("PLAYER DICE AMOUT: " + playerDiceArray.Length);
            warden.playerFirst = false;
            
            if(playerDiceArray.Length > 1){
                EmitSignal("WardenWonBluff");
            }
            else if(playerDiceArray.Length <= 1 && roundNum == 1){
                roundNum = 2;
                playerWonRound = false;
                playerDiceArray = new int[5];

                EmitSignal("RoundTwoStart", playerWonRound);
            }
            else if(playerDiceArray.Length <= 1 && roundNum == 2){
                roundNum = 3;
                playerWonRound = false;
                playerDiceArray = new int[5];

                EmitSignal("RoundThreeStart", playerWonRound);
            }
            else if(roundNum == 3 && playerLifeCount == 1){
                //ADD: Player trips balls to death.

                //Emit "GameEnd" Signal.
                EmitSignal("GameEndPlayerLoss");
            }
            else if(roundNum == 3 && playerLifeCount > 1){
                //ADD: Player trips balls Signal.

                EmitSignal("WardenWonBluff");
            }
        }
        else{
            GD.Print("PLAYER WON THE BLUFF");
            warden.playerFirst = true;
            
            if(warden.dieArray.Length > 1){
                EmitSignal("PlayerWonBluff");
            }
            else if(warden.dieArray.Length <= 1 && roundNum == 1){
                roundNum = 2;
                playerWonRound = true;
                playerDiceArray = new int[5];

                EmitSignal("RoundTwoStart", playerWonRound);
            }
            else if(warden.dieArray.Length <= 1 && roundNum == 2){
                roundNum = 3;
                playerWonRound = true;
                playerDiceArray = new int[5];

                EmitSignal("RoundThreeStart", playerWonRound);
            }
            else if(warden.dieArray.Length <= 1 && roundNum == 3){
                //Emit "GameEnd" Signal.
                EmitSignal("GameEndPlayerWin");
            }
        }
    }
}
