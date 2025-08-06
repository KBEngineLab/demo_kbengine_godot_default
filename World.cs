using Godot;
using System;

public partial class World : Node3D
{
    public static World Instance;

    public World()
    {
        World.Instance = this;
    }


    public override void _Ready()
    {
        base._Ready();
        var worldUITscn = GD.Load<PackedScene>("res://UI/WorldUI.tscn");
        Control worldUI = worldUITscn.Instantiate<Control>();
        worldUI.MouseFilter = Control.MouseFilterEnum.Ignore;
        // GetParent().
        GetParent().CallDeferred("add_child", worldUI);
    }

}
