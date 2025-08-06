using Godot;
using System;
using KBEngine;

public partial class App : GodotKBEMain
{
    public override void _Ready()
    {
        KBELog.Init(new GodotLogProvider());
        base._Ready();
    }
}
