using Godot;
using System;
using KBEngine;

public partial class PlayerController : Node3D
{
    [Export]
    public float MoveSpeed = 5.0f;

    private Vector3 _targetPosition;
    
    private CharacterBody3D _characterBody;
    
    private Label3D _nameLabel;
    private Label3D _hpLabel;
    
    
    public static PlayerController Instance;

    public Avatar Avatar;
    

    public override void _Ready()
    {
        Instance = this;
        
        _targetPosition = GlobalTransform.Origin;
        _characterBody = GetNode<CharacterBody3D>("PlayerCharacterBody3D");

        _nameLabel = GetNode<Label3D>("PlayerCharacterBody3D/HeadInfo/NameLabel");
        _hpLabel = GetNode<Label3D>("PlayerCharacterBody3D/HeadInfo/HPLabel");
    }


    public int GetStatus()
    {
        if (Avatar == null) return -1;
        return Avatar.state;
    }

    public void SetHeadInfo()
    {
        _nameLabel.Text = this.Avatar.name;
        _hpLabel.Text = this.Avatar.HP.ToString() + "/" + this.Avatar.HP_Max.ToString();
    }

    public override void _Process(double delta)
    {
        
        Avatar.position = _characterBody.GlobalPosition;
        Avatar.direction = new KBVector3(_characterBody.GlobalRotationDegrees.X,_characterBody.GlobalRotationDegrees.Y+180,_characterBody.GlobalRotationDegrees.Z);

        if (!Avatar.isPlayer())
        {
            // 获取当前位置
            var currentPos = _characterBody.GlobalTransform.Origin;
            
            // 平滑移动到目标位置
            var newPos = currentPos.MoveToward(_targetPosition, (float)(MoveSpeed * delta));
            
            // 更新位置
            _characterBody.GlobalTransform = new Transform3D(_characterBody.GlobalTransform.Basis, newPos);
        }
        
    }

    // 外部可调用此方法设置目标点
    public void SetTargetPosition(Vector3 newTarget)
    {
        _targetPosition = newTarget;
    }
}
