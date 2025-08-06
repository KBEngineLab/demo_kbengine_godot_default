using Godot;
using System;

public partial class AvatarController : CharacterBody3D
{
    [Export]
    public float MoveSpeed = 5.0f;

    [Export]
    public float SmoothFactor = 5.0f;

    private Vector3 _targetPosition;
    
    public Avatar Avatar;
    
    private Label3D _nameLabel;
    private Label3D _hpLabel;

    public override void _Ready()
    {
        _targetPosition = GlobalTransform.Origin;
        
        
        _nameLabel = GetNode<Label3D>("HeadInfo/NameLabel");
        _hpLabel = GetNode<Label3D>("HeadInfo/HPLabel");
    }
    
    
    public void SetHeadInfo()
    {
        _nameLabel.Text = this.Avatar.name;
        _hpLabel.Text = this.Avatar.HP.ToString() + "/" + this.Avatar.HP_Max.ToString();
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 currentPosition = GlobalTransform.Origin;
        // 判断是否接近目标
        if (currentPosition.DistanceTo(_targetPosition) > 0.05f)
        {
            // 插值平滑移动（位置）
            Vector3 newPosition = currentPosition.Lerp(_targetPosition, (float)(delta * SmoothFactor));

            // 计算速度向量
            Vector3 velocity = (newPosition - currentPosition) / (float)delta;

            // 使用 move_and_slide 平滑移动
            Velocity = velocity;
            MoveAndSlide();
        }
        else
        {
            Velocity = Vector3.Zero;
        }
    }

    // 外部可调用此方法设置目标点
    public void SetTargetPosition(Vector3 newTarget)
    {
        _targetPosition = newTarget;
    }
}
