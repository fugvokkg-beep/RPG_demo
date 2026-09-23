using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GMcmd
{
    [MenuItem("Cmcmd/读取表格")]
    public static void ReadTable()
    {
        
    }

    [MenuItem("Cmcmd/打开界面")]
    public static void OpenBackpackPanel()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }

    [MenuItem("Cmcmd/关闭")]
    public static void CloseBackpackPanel()
    {
        UIManager.Instance.ClosePanel(UIConst.PackagePanel);
    }
}
