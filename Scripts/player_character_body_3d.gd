extends CharacterBody3D

const SPEED = 5.0
const JUMP_VELOCITY = 4.5

@onready var _camera_pivot := $"../CameraPivot"
@onready var _playerModel := $PlayerModel



func _physics_process(delta: float) -> void:
	var avatarStatus = $"..".GetStatus()
	if avatarStatus == -1 or avatarStatus == 1:
		return
		
	
	var input_dir = Vector2.ZERO

	if Input.is_action_pressed("move_forward"):
		input_dir.y -= 1
	if Input.is_action_pressed("move_backward"):
		input_dir.y += 1
	if Input.is_action_pressed("move_left"):
		input_dir.x -= 1
	if Input.is_action_pressed("move_right"):
		input_dir.x += 1

	input_dir = input_dir.normalized()

	if input_dir != Vector2.ZERO:
		# 使用相机方向（从 pivot 取 basis，忽略 pitch，仅取 yaw 水平朝向）
		var camera_yaw_basis = Basis(Vector3.UP, _camera_pivot.rotation.y)
		
		var move_dir = (camera_yaw_basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()

		# 设置速度
		velocity.x = move_dir.x * SPEED
		velocity.z = move_dir.z * SPEED

		# 可选：角色朝向移动方向
		look_at(global_position + Vector3(move_dir.x, 0, move_dir.z), Vector3.UP)
		#look_at(global_position + Vector3(move_dir.x, 0, move_dir.z))
		#_playerModel.look_at(global_position + Vector3(move_dir.x, 0, move_dir.z))
	else:
		# 停止水平移动
		velocity.x = 0
		velocity.z = 0

	# 重力与跳跃
	if not is_on_floor():
		velocity.y -= 9.8 * delta
	else:
		if Input.is_action_just_pressed("jump"):
			velocity.y = JUMP_VELOCITY
		else:
			velocity.y = 0

	move_and_slide()
