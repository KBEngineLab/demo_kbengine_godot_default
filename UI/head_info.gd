extends Node3D


@onready var _camera := $"/root/Main/World/Player/CameraPivot/SpringArm3D/Camera3D" as Camera3D
func _process(_delta: float) -> void:
	if _camera:
		var cam_pos = _camera.global_transform.origin
		var my_pos = global_transform.origin
		cam_pos.y = my_pos.y # 只在水平面旋转

		look_at(cam_pos, Vector3.UP)
		rotate_y(PI) # 修复左右翻转
