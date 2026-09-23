using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Skill Data", fileName = "Skill data - ")]
public class Skill_DataSo : ScriptableObject
{
    public int cost;


    [Header("Skill descripion")]
    public string displayName;
    [TextArea]
    public string description;
    public Sprite icon;
}
