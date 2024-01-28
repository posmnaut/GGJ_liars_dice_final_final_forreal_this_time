extends Node

@onready var up_freq_butt_anim = $"Demo-Scene-Root/UpFreqButton/AnimationPlayer"
@onready var up_face_butt_anim = $"Demo-Scene-Root/UpFaceButton/AnimationPlayer"
@onready var dwn_freq_butt_anim = $"Demo-Scene-Root/DownFreqButton/AnimationPlayer"
@onready var dwn_face_butt_anim = $"Demo-Scene-Root/DownFaceButton/AnimationPlayer"
@onready var sub_butt_anim = $"Demo-Scene-Root/Submit-Base/AnimationPlayer"
@onready var bluff_butt_anim = $"Demo-Scene-Root/Bluff-Base/AnimationPlayer"

var butt_debug = 0


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func _animate_button(button) :
	if button == 0:
		up_freq_butt_anim.play("butt_anim_a");
	if button == 1:
		up_face_butt_anim.play("butt_anim_b");
	if button == 2:
		dwn_freq_butt_anim.play("butt_anim_c");
	if button == 3:
		dwn_face_butt_anim.play("butt_anim_d");
	if button == 4:
		sub_butt_anim.play("butt_anim_f");
	if button == 5:
		bluff_butt_anim.play("butt_anim_e");

func _on_bid_select_button_anim(button):
	_animate_button(button)
