using UnityEngine;
namespace Ultracat
{
    public enum WeaponState
    {
        Ready,
        Cooldown,
        Charging,
        Attacking,
    }
    //Base class for weapons
    public abstract class WeaponBase : MonoBehaviour
    {
        public WeaponSettings WeaponSettings;
        protected float attackStartTime = 0;
        protected float cooldownStartTime = 0;
        protected float reloadSpeed;
        public float ChargeStartTime { get; protected set; } = 0;
        public float ChargeTime { get; protected set; } = 0;
        public WeaponState WeaponState { get; protected set; }

        [Header("Presentation")]
        protected Animator entityAnimator;

        public System.Action onAmmoChanged;
        public System.Action onStartCharging;
        public System.Action onStopCharging;

        protected EntityBase user;
        protected Movement userMovement;

        public Transform graphics;
        [SerializeField] protected Animator animator;

        private void Awake()
        {
            user = GetComponentInParent<EntityBase>();
            userMovement = user.GetComponent<Movement>();
            entityAnimator = user.GetComponent<Animator>(); //Reload();
            reloadSpeed = WeaponSettings.BaseReloadSpeed;
        }

        private void FixedUpdate()
        {
            AttackTimer();
            CooldownTimer();
            ChargeTimer();
        }
        private void AttackTimer()
        {
            if (WeaponState == WeaponState.Attacking)
                if (Time.fixedTime >= attackStartTime + WeaponSettings.attackTime)
                {
                    WeaponState = WeaponState.Ready;
                    FinishAttack();
                }
        }
        private void CooldownTimer()
        {
            if (WeaponState == WeaponState.Cooldown)
                if (Time.fixedTime >= cooldownStartTime + GetReloadTime())
                {
                    WeaponState = WeaponState.Ready;
                }
        }
        private void ChargeTimer()
        {
            if (WeaponState == WeaponState.Charging)
            {
                ChargeTime += Time.fixedDeltaTime;
                if (ChargeTime > WeaponSettings.MaxChargeTime)
                    ChargeTime = WeaponSettings.MaxChargeTime;
            }
        }
        public void StartAttack(Vector2 direction)
        {
            if (!CheckIfCanAttack()) return;
            attackStartTime = Time.fixedTime;
            WeaponState = WeaponState.Attacking;
            OnAttack(direction);
        }
        protected abstract void OnAttack(Vector2 direction);
        protected abstract void FinishAttack(bool startCooldown = true);
        public void StartCharging(Vector2 direction)
        {
            if (!CheckIfCanAttack() || !WeaponSettings.ChargeAttackEnabled) return;
            OnStartCharging(direction);
            Combat.SetScaleByDirection(direction);
            entityAnimator.SetBool("IsChanneling", true);
            WeaponState = WeaponState.Charging;
            ChargeTime = 0;
            userMovement.Stun(true);
            onStartCharging?.Invoke();
        }

        protected abstract void OnStartCharging(Vector2 direction);
        public void StopCharging(Vector2 direction)
        {
            if (WeaponState != WeaponState.Charging) return;
            OnStopCharging(direction);

        }
        protected abstract void OnStopCharging(Vector2 direction);
        public virtual bool CheckIfCanAttack()
        {
            if (WeaponState != WeaponState.Ready) return false;
            return true;
        }
        public virtual float GetReloadTime()
        {
            var time = 1 / ((reloadSpeed + user.EntityStats.AttackSpeed.Value) / 100);
            return time;
        }
        public virtual float CalculateDamage(EntityBase entity)
        {
            return WeaponSettings.baseDamage + entity.EntityStats.Attack.Value * WeaponSettings.percentFromATK / 100;
        }
        protected virtual float GetChargeFactor(float channelTime)
        {
            return channelTime / WeaponSettings.MaxChargeTime;
        }
        protected virtual float CalculateChargedDamage(float channelTime)
        {
            return WeaponSettings.baseDamage + user.EntityStats.Attack.Value * WeaponSettings.percentFromATK / 100 * GetChargeFactor(channelTime) * WeaponSettings.chargeMultiplier;
        }
        protected virtual void OnHit(Projectile proj, EntityBase hit)
        {
            if (WeaponSettings.hitSounds.Length > 0)
                GameManager.SoundSpawner.PlaySound(WeaponSettings.hitSounds[Random.Range(0, WeaponSettings.hitSounds.Length)], WeaponSettings.SoundChannel);
            proj.OnHit.RemoveListener(OnHit);
        }
    }
    [System.Serializable]
    public class WeaponAnimations
    {
        public string[] CharacterAnimationKeys = { "Attack1", "Attack2", "Attack3" };
        public string[] WeaponAnimationKeys = { "Attack1", "Attack2", "Attack3" };
    }

}