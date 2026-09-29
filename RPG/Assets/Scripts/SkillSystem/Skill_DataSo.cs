using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Skill Data", fileName = "Skill data - ")]
public class Skill_DataSo : ScriptableObject
{
    public int cost;
    public SkillType skillType;
    public bool unlockByDefault;
    public UpgradeData upgradeData;


    [Header("Skill descripion")]
    public string displayName;
    [TextArea]
    public string description;
    public Sprite icon;
}
[System.Serializable]
public class UpgradeData
{
    public SkillUpgradeType upgradeType;
    public float coolDown;
}
