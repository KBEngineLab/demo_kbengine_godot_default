using System;
using Godot;
using KBEngine;

public class NPC : NPCBase
{
    public override void onEnterWorld()
    {
        base.onEnterWorld();
        var monster = GD.Load<PackedScene>("res://Prefab/NPC.tscn");
        ObjectController monsterNode = monster.Instantiate<ObjectController>();
        // monsterNode.trs
        this.renderObj = monsterNode;
        World.Instance.GetTree().CurrentScene.GetNode("World").AddChild(monsterNode);
        
        monsterNode.SetHeadInfo(this.name);

        // monsterNode.Position = position;
        ((Node3D)this.renderObj).GlobalPosition = new Vector3(position.x, 1, position.z);
    }

    public override void onLeaveWorld()
    {
        base.onLeaveWorld();
        if (this.renderObj != null)
        {
            ((Node3D)this.renderObj).QueueFree();
        }
    }
}
