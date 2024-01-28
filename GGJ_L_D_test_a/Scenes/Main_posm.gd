extends Node

@onready var face_increase = $BidSelect/FaceIncrease/AnimationPlayer
@onready var face_decrease = $BidSelect/FaceDecrease/AnimationPlayer
@onready var feq_increase = $BidSelect/FeqIncrease/AnimationPlayer
@onready var feq_decrease = $BidSelect/FreqDecrease/AnimationPlayer
@onready var submit_butt = $"BidSelect/Submit-button/AnimationPlayer"
@onready var bluff_butt = $"BidSelect/Bluff-button/AnimationPlayer"

var butt_debug = 0


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func _animate_button(button) :
	if button == 0:
		face_increase.play("butt_anim");
	if button == 1:
		face_decrease.play("butt_anim");
	if button == 2:
		feq_increase.play("butt_anim");
	if button == 3:
		feq_decrease.play("butt_anim");
	if button == 4:
		submit_butt.play("butt_anim");
	if button == 5:
		bluff_butt.play("butt_anim");

func _on_bid_select_button_anim(button):
	_animate_button(button)
