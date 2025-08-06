using System;
using Godot;
using KBEngine;

public class Avatar : AvatarBase
{
    public static Avatar Instance;
    public Avatar()
    {
        Instance = this;
    }
    public override void dialog_addOption(byte arg1, uint arg2, string arg3, int arg4)
    {
    }

    public override void dialog_close()
    {
    }

    public override void dialog_setText(string arg1, byte arg2, uint arg3, string arg4)
    {
    }

    public override void onAddSkill(int arg1)
    {
    }

    public override void onJump()
    {
    }

    public override void onRemoveSkill(int arg1)
    {
    }

    public override void recvDamage(int arg1, int arg2, int arg3, int arg4)
    {
    }

    public override void __init__()
    {
        base.__init__();
    }

    public override void onEnterWorld()
    {
        base.onEnterWorld();
        
        if (this.isPlayer())
        {
            var monster = GD.Load<PackedScene>("res://Prefab/Player.tscn");
            PlayerController monsterNode = monster.Instantiate<PlayerController>();
            // monsterNode.trs
            this.renderObj = monsterNode;
            World.Instance.GetTree().CurrentScene.GetNode("World").AddChild(monsterNode);
            monsterNode.Avatar = this;
            monsterNode.GlobalPosition = new Vector3(position.x, 0, position.z);
            
            monsterNode.SetHeadInfo();
           
        }
        else
        {
            var monster = GD.Load<PackedScene>("res://Prefab/Avatar.tscn");
            AvatarController monsterNode = monster.Instantiate<AvatarController>();
            this.renderObj = monsterNode;
            monsterNode.Avatar = this;
            World.Instance.GetTree().CurrentScene.GetNode("World").AddChild(monsterNode);
            monsterNode.GlobalPosition = new Vector3(position.x, 0, position.z);
            monsterNode.SetHeadInfo();
        }
    }

    public override void onHPChanged(int oldValue)
    {
        base.onHPChanged(oldValue);
        if (this.renderObj == null) return;

        if (this.isPlayer())
        {
            ((PlayerController)this.renderObj).SetHeadInfo();
        }
        else
        {
            
            ((AvatarController)this.renderObj).SetHeadInfo();
        }
    }

    public override void onHP_MaxChanged(int oldValue)
    {
        base.onHP_MaxChanged(oldValue);
        if (this.renderObj == null) return;
        GD.Print(this.HP_Max);
        GD.Print(this.HP);
        if (this.isPlayer())
        {
            ((PlayerController)this.renderObj).SetHeadInfo();
        }
        else
        {
            ((AvatarController)this.renderObj).SetHeadInfo();
        }
    }

    public override void onLeaveWorld()
    {
        base.onLeaveWorld();
        
        if (this.renderObj != null)
        {
            ((Node3D)this.renderObj).QueueFree();
        }
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
        ((Node3D)this.renderObj).GlobalPosition = new Vector3(position.x, 0, position.z);
    }

    public override void onSmoothPositionChanged(KBVector3 oldValue)
    {
        base.onSmoothPositionChanged(oldValue);
        if (this.renderObj == null) return;

        if (this.isPlayer())
        {
            
        }
        else
        {
            ((AvatarController)this.renderObj).SetTargetPosition(new Vector3(position.x, 1, position.z));
        }
    }

    public override void onDirectionChanged(KBVector3 oldValue)
    {
        base.onDirectionChanged(oldValue);
        
        if (this.renderObj == null) return;
        
        if (this.isPlayer())
        {
            
        }
        else
        {
            ((AvatarController)this.renderObj).GlobalRotationDegrees = new Vector3(position.x, position.y - 180, position.z);
        }
    }
}
