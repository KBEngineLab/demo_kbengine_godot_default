using Godot;
using System;

public partial class ObjectController : MeshInstance3D
{
    private Label3D _nameLabel;

    public override void _Ready()
    {
        _nameLabel = GetNode<Label3D>("HeadInfo/NameLabel");
    }
    
    
    public void SetHeadInfo(String name)
    {
        _nameLabel.Text = name;
    }

}
