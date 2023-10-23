using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDoubleProjectile: ProjectileWeapon
{
    [SerializeField] Projectile _secondaryAttackProjectile;

    public WeaponAnimations ChargedAttackAnimations = new WeaponAnimations();
    [SerializeField] protected AudioClip[] ChargedSounds;
    protected override void OnStopCharging(Vector2 direction)
    {
        if (StunOnAttack)
        {
            userMovement.Stun(true);
        }
        for (int i = 0; i < projectileAmount; i++)
        {
            var spawned = SpawnProjectile(transform.position, _secondaryAttackProjectile, WeaponSettings.attackSpawnRange);
            spawned.Initialize(user, CalculateDirection(direction, spread), CalculateDamage(user));
            spawned.OnHit.AddListener(OnHit);
        }
        Combat.SetScaleByDirection(direction);

        WeaponState = WeaponState.Cooldown;
        cooldownStartTime = Time.time;

        GameManager.SoundSpawner.PlaySound(WeaponSettings.Sounds[Random.Range(0, WeaponSettings.Sounds.Length)], WeaponSettings.SoundChannel, 0.9f, 1.1f);
        CameraTweaker.Shake(WeaponSettings.cameraShakeAmp, WeaponSettings.cameraShakeFrec, WeaponSettings.cameraShakeTime);
        if (entityAnimator)
            entityAnimator.Play(WeaponSettings.Animations.CharacterAnimationKeys[Random.Range(0, WeaponSettings.Animations.CharacterAnimationKeys.Length)]);

        userMovement.Stun(false);
        if (animator)
            animator.Play(WeaponSettings.Animations.WeaponAnimationKeys[Random.Range(0, WeaponSettings.Animations.WeaponAnimationKeys.Length)]);
    }
}
