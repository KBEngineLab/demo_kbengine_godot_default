using Godot;
using System;
using KBEngine;

public partial class SelectAvatarItem : Button
{
    public AVATAR_INFOS AvatarInfo;
    public int Index = 0;
    void _on_button_up()
    {
        MainUi.Instance.UI_OnSelectAvatarItemClick(this);
    }
}
