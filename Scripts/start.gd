extends Node
@onready var kbeapp_scene: PackedScene = preload("res://App.tscn")
@onready var mainui_scene: PackedScene = preload("res://UI/MainUI.tscn")


func _ready():
	#init_kbeapp()
	init_mainui()

func init_kbeapp():
	var kbeapp_instance = kbeapp_scene.instantiate()
	add_child(kbeapp_instance)
	print("KBEApp 初始化完成")

func init_mainui():
	var mainui_instance = mainui_scene.instantiate()
	add_child(mainui_instance)
	print("MainUI 初始化完成")
