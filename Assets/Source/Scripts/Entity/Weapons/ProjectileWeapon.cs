using UnityEngine;
namespace Ultracat
{
    public class ProjectileWeapon : WeaponBase
    {
        public bool StunOnAttack = true;
        public Projectile Projectile;
        [SerializeField] protected float spread = 1;
        [SerializeField] protected int projectileAmount = 1;
        protected override void OnAttack(Vector2 direction)
        {
            if (StunOnAttack)
            {
                userMovement.Stun(true);
            }
            for (int i = 0; i < projectileAmount; i++)
            {
                var spawned = SpawnProjectile(transform.position, Projectile, WeaponSettings.attackSpawnRange);
                spawned.Initialize(user, CalculateDirection(direction, spread), CalculateDamage(user));
                spawned.OnHit.AddListener(OnHit);
            }
            Combat.SetScaleByDirection(direction);
        }
        protected override void FinishAttack(bool startCooldown = true)
        {
            WeaponState = WeaponState.Cooldown;
            cooldownStartTime = Time.time;
            PlayAttackFX();
            userMovement.Stun(false);
            if (animator)
                animator.Play(WeaponSettings.Animations.WeaponAnimationKeys[Random.Range(0, WeaponSettings.Animations.WeaponAnimationKeys.Length)]);
        }
        protected static Projectile SpawnProjectile(Vector3 spawnPos, Projectile projectile, Vector2 attackRange)
        {
            var spawned = GameManager.AttacksPool.Get(projectile);
            spawned.transform.position = spawnPos + new Vector3(Random.Range(-attackRange.x, attackRange.x), Random.Range(-attackRange.y, attackRange.y));
            return spawned;
        }
        protected static Vector2 CalculateDirection(Vector2 direction, float spread)
        {
            var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            angle += Random.Range(-spread, spread);
            return Quaternion.AngleAxis(angle, -Vector3.forward) * Vector2.up;
        }
        protected override void OnStopCharging(Vector2 direction)
        {
            Combat.SetScaleByDirection(direction);
            if (entityAnimator)
                entityAnimator.SetBool("IsChanneling", false);
            WeaponState = WeaponState.Cooldown;
            userMovement.Stun(false);
            onStopCharging?.Invoke();
            cooldownStartTime = Time.time;
            if (StunOnAttack)
            {
                userMovement.StunForTime(WeaponSettings.chargeAttackTime);
            }
        }
        protected void PlayAttackFX()
        {
            GameManager.SoundSpawner.PlaySound(WeaponSettings.Sounds[Random.Range(0, WeaponSettings.Sounds.Length)], WeaponSettings.SoundChannel, 0.9f, 1.1f);
            CameraTweaker.Shake(WeaponSettings.cameraShakeAmp, WeaponSettings.cameraShakeFrec, WeaponSettings.cameraShakeTime);
            if (entityAnimator)
                entityAnimator.Play(WeaponSettings.Animations.CharacterAnimationKeys[Random.Range(0, WeaponSettings.Animations.CharacterAnimationKeys.Length)]);
        }
        protected override void OnHit(Projectile proj, EntityBase hit)
        {
            base.OnHit(proj, hit);
        }

        protected override void OnStartCharging(Vector2 direction)
        {

        }
    }
}