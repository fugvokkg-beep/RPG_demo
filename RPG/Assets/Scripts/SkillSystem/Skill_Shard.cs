using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shard : Skill_Base
{
    private SkillObject_Shard currentShard;

    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonateTime = 2;

    [Header("Moving Shard Upgrade")]
    [SerializeField] private float shardSpeed = 1;

    [Header("Multicast Shard Upgrade")]
    [SerializeField] private int maxCharges = 3;
    [SerializeField] private int currentCharges;
    [SerializeField] private bool isReCharging;

    protected override void Awake()
    {
        base.Awake();

        currentCharges = maxCharges;
    }

    public override void TryUseSkill()
    {
        if(CanUseSkill() == false)
        {
            return;
        }

        if (Unlocked(SkillUpgradeType.Shard))
            HandleShardRegular();

        if(Unlocked(SkillUpgradeType.Shard_MoveToEntity))
            HandleShardMoving(); 

        if (Unlocked(SkillUpgradeType.Shard_Multicast))
            HandleShardMulticast();
    }

    private void HandleShardMulticast()
    {
        if (currentCharges <= 0)
            return;

        CreateShard();
        currentShard.MoveTowardsClosesrTarget(shardSpeed);

        currentCharges--;

        if (isReCharging == false)
            StartCoroutine(ShardReChargeCo());
    }

    private IEnumerator ShardReChargeCo()
    {
        isReCharging = true;

        while(currentCharges < maxCharges)
        {
            yield return new WaitForSeconds(cooldown);
            currentCharges++;
        }

        isReCharging = false;
    }


    private void HandleShardMoving()
    {
        CreateShard();
        currentShard.MoveTowardsClosesrTarget(shardSpeed);

        SetSkillOnCooldown();
    }


    private void HandleShardRegular()
    {
        CreateShard();
        SetSkillOnCooldown();
    }

    public void CreateShard()
    {
        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        currentShard = shard.GetComponent<SkillObject_Shard>();
        currentShard.SetupShard(detonateTime);
    }
}
