using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeOrb : ActiveItem
{
    [SerializeField] protected float healFromMaxHealth = 0.1f;
    [SerializeField] protected string characterHealAnimationKey = "heal";
    public override void OnActivate()
    {
        Debug.Log(entity);
        base.OnActivate();
        var spawned = GameManager.EffectsPool.Get(GameManager.Database.Effects[11]);
        spawned.transform.SetParent(transform);
        spawned.transform.localPosition = Vector3.zero;
        entity.Heal(entity.EntityStats.Health.Value * healFromMaxHealth);
        entity.GetComponent<Animator>().Play(characterHealAnimationKey);
    }
}
