extends Node

@onready var up_freq_butt_anim = $BidSelect/FeqIncrease/AnimationPlayer
@onready var up_face_butt_anim = $BidSelect/FaceIncrease/AnimationPlayer
@onready var dwn_freq_butt_anim = $BidSelect/FreqDecrease/AnimationPlayer
@onready var dwn_face_butt_anim = $BidSelect/FaceDecrease/AnimationPlayer
@onready var sub_butt_anim = $"BidSelect/Submit-button/AnimationPlayer"
@onready var bluff_butt_anim = $"BidSelect/Bluff-button/AnimationPlayer"

var butt_debug = 0


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func _animate_button(button) :
	if button == 0:
		up_freq_butt_anim.play("butt_anim");
	if button == 1:
		up_face_butt_anim.play("butt_anim");
	if button == 2:
		dwn_freq_butt_anim.play("butt_anim");
	if button == 3:
		dwn_face_butt_anim.play("butt_anim");
	if button == 4:
		sub_butt_anim.play("butt_anim");
	if button == 5:
		bluff_butt_anim.play("butt_anim");

func _on_bid_select_button_anim(button):
	_animate_button(button)
