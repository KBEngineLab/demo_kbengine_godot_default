extends Node3D

@onready var _player := $"../PlayerCharacterBody3D"


func _process(delta: float) -> void:
	position = _player.position




@export_range(0.0, 1.0) var mouse_sensitivity = 0.01
@export var tilt_limit = deg_to_rad(75)
var is_rotating = false

func _input(event):
	if event is InputEventMouseButton and event.button_index == MOUSE_BUTTON_RIGHT:
		if event.pressed:
			is_rotating = true
		else:
			is_rotating = false
			
func _unhandled_input(event: InputEvent) -> void:
	if event is InputEventMouseMotion and is_rotating:
		rotation.x -= event.relative.y * mouse_sensitivity
		# Prevent the camera from rotating too far up or down.
		rotation.x = clampf(rotation.x, -tilt_limit, tilt_limit)
		rotation.y += -event.relative.x * mouse_sensitivity
