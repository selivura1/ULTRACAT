using System.Collections;
using UnityEngine;
namespace Ultracat
{
    public class ClawsWeapon : ProjectileWeapon
    {
        [SerializeField] int _chargedStrikesAmount = 3;
        [SerializeField] int _chargedHealAmount = 2;
        private const float ChargeTimeReloadReduction = 2;
        protected override void OnStopCharging(Vector2 direction)
        {
            base.OnStopCharging(direction);
            StartCoroutine(AttackSequence(direction));
        }
        private void HealOnHit(Projectile proj, EntityBase hit)
        {
            user.Heal(_chargedHealAmount * GetChargeFactor(ChargeTime));
            proj.OnHit.RemoveListener(HealOnHit);
        }
        private IEnumerator AttackSequence(Vector2 direction)
        {
            for (int i = 0; i < _chargedStrikesAmount; i++)
            {
                PlayAttackFX();
                var spawned = SpawnProjectile(transform.position, Projectile, WeaponSettings.attackSpawnRange);
                spawned.Initialize(user, CalculateDirection(direction, spread), CalculateChargedDamage(ChargeTime));
                spawned.OnHit.AddListener(HealOnHit);
                spawned.OnHit.AddListener(OnHit);
                yield return new WaitForSeconds(GetReloadTime() / ChargeTimeReloadReduction);
            }
        }
    }
}