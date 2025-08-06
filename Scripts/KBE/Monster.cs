using System;
using Godot;
using KBEngine;

public class Monster : MonsterBase
{
    public Monster()
    {
        
    }

    public override void __init__()
    {
        base.__init__();
        
    }

    public override void recvDamage(int arg1, int arg2, int arg3, int arg4)
    {
    }

    public override void onEnterWorld()
    {
        base.onEnterWorld();
        var monster = GD.Load<PackedScene>("res://Prefab/Monster.tscn");
        MonsterController monsterNode = monster.Instantiate<MonsterController>();
        // monsterNode.trs
        this.renderObj = monsterNode;
        World.Instance.GetTree().CurrentScene.GetNode("World").AddChild(monsterNode);
        monsterNode.Monster = this;
        

        // monsterNode.Position = position;
        ((Node3D)this.renderObj).GlobalPosition = new Vector3(position.x, 0, position.z);
        
        monsterNode.SetHeadInfo();
    }

    public override void onLeaveWorld()
    {
        base.onLeaveWorld();
        if (this.renderObj != null)
        {
            ((Node3D)this.renderObj).QueueFree();
        }
    }
    
    public override void onHPChanged(int oldValue)
    {
        base.onHPChanged(oldValue);
        if (this.renderObj == null) return;
        ((MonsterController)this.renderObj).SetHeadInfo();
    }

    public override void onEnterSpace()
    {
        base.onEnterSpace();
    }

    public override void onLeaveSpace()
    {
        base.onLeaveSpace();
    }

    public override void onPositionChanged(KBVector3 oldValue)
    {
        base.onPositionChanged(oldValue);

        if (this.renderObj == null) return;

        ((Node3D)this.renderObj).GlobalPosition = new Vector3(position.x, 1, position.z);
    }

    public override void onSmoothPositionChanged(KBVector3 oldValue)
    {
        base.onSmoothPositionChanged(oldValue);
        if (this.renderObj == null) return;
        ((MonsterController)this.renderObj).SetTargetPosition(new Vector3(position.x, 1, position.z));
    }

    public override void onDirectionChanged(KBVector3 oldValue)
    {
        base.onDirectionChanged(oldValue);
    }
}
