using Godot;
using System;
using System.Text;
using KBEngine;

public partial class MainUi : Control
{
    private Control Login;
    private Control CreateAvatar;
    private Control SelectAvatar;
    
    private LineEdit NameEdit;
    private LineEdit PasswordEdit;

    private LineEdit CreateAvatarNameEdit;
    private BoxContainer AvatarListContainer;
    private PackedScene _selectAvatarItemTscn = GD.Load<PackedScene>("res://UI/SelectAvatar/select_avatar_item.tscn");
    
    
    public static MainUi Instance;
    
    public SelectAvatarItem SelectAvatarItem;



    
    
    
    
    public override void _Ready()
    {
        Instance = this;
        Login = GetNode<Control>("Login");
        CreateAvatar = GetNode<Control>("CreateAvatar");
        SelectAvatar = GetNode<Control>("SelectAvatar");


        NameEdit = GetNode<LineEdit>("Login/UserNameEdit");
        PasswordEdit = GetNode<LineEdit>("Login/PasswordEdit");
        CreateAvatarNameEdit = GetNode<LineEdit>("CreateAvatar/CreateAvatarNameEdit");

        AvatarListContainer = GetNode<BoxContainer>("SelectAvatar/CenterContainer/AvatarList");
        
        
        Login.Visible = true;
        CreateAvatar.Visible = false;
        SelectAvatar.Visible = false;
    }

    public void OpenLoginPlane()
    {
        Login.Visible = true;
        CreateAvatar.Visible = false;
        SelectAvatar.Visible = false;
    }

    public void OpenCreateAvatarPlane()
    {
        Login.Visible = false;
        SelectAvatar.Visible = false;
        CreateAvatar.Visible = true;
    }

    public void OpenSelectAvatarPlane()
    {
        Login.Visible = false;
        SelectAvatar.Visible = true;
        CreateAvatar.Visible = false;
    }


    /// <summary>
    /// 创建角色列表
    /// </summary>
    /// <param name="avatarInfoList"></param>
    public void UI_CreateAvatarList(AVATAR_INFOS_LIST avatarInfoList)
    {
        this.OpenSelectAvatarPlane();

        foreach (var child in AvatarListContainer.GetChildren())
        {
            child.QueueFree();
        }
        
        for (var i = 0; i < avatarInfoList.values.Count; i++)
        {
            var avatarInfo = avatarInfoList.values[i];
            
            SelectAvatarItem instanceNode = _selectAvatarItemTscn.Instantiate() as SelectAvatarItem;
            if (instanceNode == null) continue;
            instanceNode.AvatarInfo = avatarInfo;

            instanceNode.Text = avatarInfo.name;
            instanceNode.Index = i;
            
            AvatarListContainer.AddChild(instanceNode);
        }
    }

    /// <summary>
    /// 选择角色
    /// </summary>
    /// <param name="selectAvatarItem"></param>
    public void UI_OnSelectAvatarItemClick(SelectAvatarItem selectAvatarItem)
    {
        this.SelectAvatarItem = selectAvatarItem;
    }
    
    
    
    void _on_login_btn_button_up()
    {
        GD.Print("连接到服务端....");

        KBEngineApp.app.login(NameEdit.Text, PasswordEdit.Text,"kbengine_unity3d_demo"u8.ToArray());
    }

    /// <summary>
    /// 创建角色按钮点击
    /// </summary>
    void _on_create_avatar_btn_button_up()
    {
        Account.Instance.baseEntityCall.reqCreateAvatar(1,this.CreateAvatarNameEdit.Text);
    }

    
    /// <summary>
    /// 创建角色返回按钮点击
    /// </summary>
    void _on_return_to_select_avatar_btn_button_up()
    {
        Login.Visible = false;
        SelectAvatar.Visible = true;
        CreateAvatar.Visible = false;
    }

    /// <summary>
    /// 进入游戏按钮点击
    /// </summary>
    void _on_enter_game_btn_button_up()
    {
        if (SelectAvatarItem == null) return;
        // GD.Load<PackedScene>("res://World.tscn");
        GetTree().ChangeSceneToFile("res://World.tscn");
        Account.Instance.baseEntityCall.selectAvatarGame(SelectAvatarItem.AvatarInfo.dbid);
    }

    /// <summary>
    /// 选择角色页面创建角色按钮点击
    /// </summary>
    void _on_goto_create_avatar_btn_button_up()
    {
        Login.Visible = false;
        SelectAvatar.Visible = false;
        CreateAvatar.Visible = true;
    }

    
    /// <summary>
    /// 删除角色按钮点击
    /// </summary>
    void _on_del_avatar_btn_button_up()
    {
        Account.Instance.baseEntityCall.reqRemoveAvatarDBID(SelectAvatarItem.AvatarInfo.dbid);
    }
    
    
    
}
