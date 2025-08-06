using System;
using Godot;
using KBEngine;

public class Account : AccountBase
{
    public static Account Instance;
    public Account()
    {
        GD.Print("Account Constructor");
        Instance = this;
        // this.baseEntityCall.reqAvatarList();
    }

    public override void __init__()
    {
        KBELog.DEBUG_MSG("Account::__init__()");
        baseEntityCall.reqAvatarList();
    }

    /// <summary>
    /// 创建角色回调
    /// </summary>
    /// <param name="arg1">retcode 0成功</param>
    /// <param name="arg2">角色信息</param>
    public override void onCreateAvatarResult(byte arg1, AVATAR_INFOS arg2)
    {
        if (arg1 == 0)
        {
            baseEntityCall.reqAvatarList();
        }
        else
        {
            KBELog.ERROR_MSG("KBEAccount::onCreateAvatarResult: error=" + KBEngineApp.app.serverErr(arg1));
        }
    }

    public override void onRemoveAvatar(ulong arg1)
    {
        if (arg1 > 0)
        {
            baseEntityCall.reqAvatarList();
        }
        else
        {
            KBELog.ERROR_MSG("KBEAccount::onRemoveAvatar(): error=角色删除失败");
        }
    }

    /// <summary>
    /// 请求角色列表回调
    /// </summary>
    /// <param name="arg1">角色列表</param>
    public override void onReqAvatarList(AVATAR_INFOS_LIST arg1)
    {
        MainUi.Instance.UI_CreateAvatarList(arg1);
    }
}
