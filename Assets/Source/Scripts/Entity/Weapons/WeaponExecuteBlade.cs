using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponExecuteBlade : ProjectileWeapon
{
    [SerializeField] private float _executeGap  = 5;
    [SerializeField] private Projectile _chargeAttackProjectile;
    protected override void OnStopCharging(Vector2 direction)
    {
        base.OnStopCharging(direction);
        PlayAttackFX();
        ThrowKnife(direction);
    }

    private void ThrowKnife(Vector2 direction)
    {
        var spawned = SpawnProjectile(transform.position, _chargeAttackProjectile, WeaponSettings.attackSpawnRange);
        spawned.Initialize(user, CalculateDirection(direction, 0), CalculateChargedDamage(ChargeTime));
        spawned.OnHit.AddListener(OnHitCharged);
        spawned.OnHit.AddListener(OnHit);
        spawned.CurrentSpeed *= GetChargeFactor(ChargeTime);
    }

    protected void OnHitCharged(Projectile proj, EntityBase hit)
    {
        userMovement.Teleport(hit.transform.position);
        proj.OnHit.RemoveListener(OnHitCharged);
    }
    protected override void OnHit(Projectile proj, EntityBase hit)
    {
        base.OnHit(proj, hit);
        if(hit.GetHealth() > 0 && hit.GetHealth() <= hit.EntityStats.Health.Value * (_executeGap / 100))
        {
            hit.TakeDamage(new DamageBreakDown(9999, user, DamageType.Execute));
        }
    }
}
