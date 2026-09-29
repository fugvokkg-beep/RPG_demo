using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{
    public int skillPoint;
    public Player_SkillManager skillManager { get;private set;  }

    private void Awake()
    {
        skillManager = FindAnyObjectByType<Player_SkillManager>();
    }

    private void Start()
    {
        
    }


    public bool EnoughSkillPoint(int cost) => skillPoint >= cost;
    public void RemoveSkillPoint(int cost) => skillPoint -= cost;
}
