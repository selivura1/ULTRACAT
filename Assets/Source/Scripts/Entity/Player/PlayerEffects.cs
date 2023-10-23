using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : CharacterEffects
{
    [SerializeField] private EffectHandler[] levelUpEffects;

    protected override void Start()
    {
        base.Start();
        if (entity)
            entity.GetComponent<PlayerLevels>().onLevelUp += SpawnLevelUpEffect;
    }
    public void SpawnLevelUpEffect()
    {
        foreach (var item in levelUpEffects)
        {
            var spawned = GameManager.EffectsPool.Get(item);
            spawned.transform.SetParent(transform);
            spawned.transform.localPosition = Vector3.zero;
        }
    }
}
