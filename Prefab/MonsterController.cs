using Godot;
using System;

public partial class MonsterController : MeshInstance3D
{
    [Export]
    public float MoveSpeed = 5.0f;

    private Vector3 _targetPosition;

    public Monster Monster;
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
        _nameLabel.Text = this.Monster.name;
        _hpLabel.Text = this.Monster.HP.ToString() + "/" + this.Monster.HP_Max.ToString();
    }
    
    public override void _Process(double delta)
    {
        // 获取当前位置
        var currentPos = GlobalTransform.Origin;

        // 平滑移动到目标位置
        var newPos = currentPos.MoveToward(_targetPosition, (float)(MoveSpeed * delta));

        // 更新位置
        GlobalTransform = new Transform3D(GlobalTransform.Basis, newPos);
    }

    // 外部可调用此方法设置目标点
    public void SetTargetPosition(Vector3 newTarget)
    {
        _targetPosition = newTarget;
    }
}
