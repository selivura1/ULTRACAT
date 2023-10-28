using System.Collections;
using UnityEngine;
namespace Ultracat
{
    public class PistolWeapon : ProjectileWeapon
    {
        [SerializeField] private int _chargedShotsAmount = 5;
        [SerializeField] private int _chargedProjectileAmount = 3;
        [SerializeField] private float _chargedAttackTimeBetweenShots = 0.05f;
        [SerializeField] private float _chargedSpread = 45;
        protected override void OnStopCharging(Vector2 direction)
        {
            base.OnStopCharging(direction);
            StartCoroutine(AttackSequence(direction));
        }
        IEnumerator AttackSequence(Vector2 direction)
        {
            var amount = _chargedShotsAmount * GetChargeFactor(ChargeTime);
            for (int i = 0; i < Mathf.FloorToInt(amount); i++)
            {
                for (int x = 0; x < _chargedProjectileAmount; x++)
                {
                    var spawned = SpawnProjectile(transform.position, Projectile, WeaponSettings.attackSpawnRange);
                    spawned.Initialize(user, CalculateDirection(direction, _chargedSpread), CalculateChargedDamage(ChargeTime));
                    spawned.OnHit.AddListener(OnHit);
                }
                PlayAttackFX();
                yield return new WaitForSeconds(_chargedAttackTimeBetweenShots);
            }
        }
    }
}