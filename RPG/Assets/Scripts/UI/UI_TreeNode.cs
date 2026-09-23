using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    private UI ui;
    private RectTransform rect;
    private UI_SkillTree skillTree;
    private UI_TreeConnectHandler treeConnectHandler;

    [Header("Unlock details")]
    public UI_TreeNode[] neededNodes;
    public UI_TreeNode[] conflictNodes;
    public bool isLocked;
    public bool isUnlocked;


    public Skill_DataSo skillData;
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    private string lockedColorHex = "#808080";
    private UnityEngine.Color lastColor;

    

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
        skillTree = GetComponentInParent<UI_SkillTree>();
        treeConnectHandler = GetComponent<UI_TreeConnectHandler>();


        updateIconColor(GetColorByHex(lockedColorHex));
    }

    private void Unlock()
    {
        isUnlocked = true;
        updateIconColor(UnityEngine.Color.white);
        LockConflictNodes();

        skillTree.RemoveSkillPoint(skillData.cost);
        treeConnectHandler.connectionImageUnlocked(isUnlocked);
    }

    private bool CanBeUnlocked()
    {
        if (isLocked || isUnlocked)
            return false;

        if (skillTree.EnoughSkillPoint(skillData.cost) == false)
            return false;


        foreach (var node in neededNodes)
        {
            if(node.isUnlocked == false)
                return false;
        }

        foreach (var node in conflictNodes)
        {
            if (node.isUnlocked)
                return false; 
        }

        return true;
    }

    private void LockConflictNodes()
    {
        foreach (var node in conflictNodes)
            node.isLocked = true;
    }

    private void updateIconColor(UnityEngine.Color color)
    {
        if (skillIcon == null)
            return;
        lastColor = skillIcon.color;
        skillIcon.color = color;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if(CanBeUnlocked())
            Unlock();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowtoolTip(true, rect,this);
        if(isUnlocked || isLocked)
           return;

        UnityEngine.Color color = UnityEngine.Color.white * .9f; color.a = 1;
        updateIconColor(color);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.ShowtoolTip(false, rect);
        if (isUnlocked || isLocked)
            return;

        updateIconColor(lastColor);

    }

    private UnityEngine.Color GetColorByHex(string Hex)
    {
        ColorUtility.TryParseHtmlString(Hex, out UnityEngine.Color color);

        return color;
    }

    private void OnDisable()
    {
        if (isLocked)
            updateIconColor(GetColorByHex(lockedColorHex));
        if (isUnlocked)
            updateIconColor(UnityEngine.Color.white);
    }

    private void OnValidate()
    {
        if (skillData == null)
            return;

        skillName = skillData.displayName;
        skillIcon.sprite = skillData.icon;
        gameObject.name = "UI TreeNode - " + skillData.displayName;
    }
}
