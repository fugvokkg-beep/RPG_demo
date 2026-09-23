using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillToolTip : UI_ToolTip
{
    private UI_SkillTree skillTree;

    [SerializeField] private Text skillName;
    [SerializeField] private Text skillDescription;
    [SerializeField] private Text skillRequirements;

    [Space]
    [SerializeField] private string metConditionHex;
    [SerializeField] private string notMetConditionHex;
    [SerializeField] private string importantInfoHex;
    [SerializeField] private Color exampleColor;

    protected override void Awake()
    {
        base.Awake();

        skillTree = GetComponentInParent<UI_SkillTree>(true);
    }

    public override void ShowtoolTip(bool show, RectTransform targetRect)
    {
        base.ShowtoolTip(show, targetRect);
    }

    public void ShowtoolTip(bool show, RectTransform targetRect, UI_TreeNode node)
    {
        base.ShowtoolTip(show, targetRect);

        skillName.text = node.skillData.displayName;
        skillDescription.text = node.skillData.description;
        skillRequirements.text = GetRequirements(node.skillData.cost, node.neededNodes,node.conflictNodes);
    }

    private string GetRequirements(int skillCost,UI_TreeNode[] neededNodes, UI_TreeNode[] conflictNodes)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("需要：");

        string costColor = skillTree.EnoughSkillPoint(skillCost) ? metConditionHex : notMetConditionHex;

        sb.AppendLine($"<color = {costColor}> {skillCost} 点技力 </color>");

        foreach(var node in neededNodes)
        {
            string nodeColor = node.isLocked? metConditionHex : notMetConditionHex;
            sb.AppendLine($"<color = {nodeColor}> {node.skillData.displayName} </color>");
        }

        if(conflictNodes.Length < 0)
            return sb.ToString();

        sb.Append("");
        sb.AppendLine($"<color = {importantInfoHex}> 锁定 </color>");

        foreach (var node in conflictNodes)
        {
            sb.AppendLine($"<color = {importantInfoHex}> {node.skillData.displayName} </color>");
        }

        return sb.ToString();
    }
}
