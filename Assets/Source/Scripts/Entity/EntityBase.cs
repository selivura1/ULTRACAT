using System;
using UnityEngine;
namespace Ultracat
{
    public enum LifeState
    {
        Alive,
        Dead,
    }
    public class EntityBase : MonoBehaviour, IDamageable
    {
        public Sprite Splash;
        [SerializeField] public new string name;
        [Header("Stats")]
        [SerializeField] float currentHealth;
        public int CurrentAbsorbtions { get; private set; }
        public int CurrentResurections { get; private set; }
        public EntityStats EntityStats = new EntityStats();
        public PotionHandler PotionHandler { get; private set; }
        protected bool invincible = false;
        private LifeState _lifeState;
        public LifeState LifeState { get => _lifeState; set => _lifeState = value; }

        public delegate void EntityAction(EntityBase entity);
        public event EntityAction onDeath;
        public Action<DamageBreakDown> onDamage;
        public Action<float> onHealthChanged;
        public Action<EntityBase> onLethalDamage;
        public Action<float> onHeal;


        public Action onInitialized;
        public Action<DamageBreakDown> onPreDamageApplied;
        public Action<DamageBreakDown> onDamageAbsorbed;

        public bool Enemy = true;

        public Action onKill;

        [SerializeField] bool _playOnDamage = true;
        [SerializeField] private string _damagedAnimation = "oof";
        private Animator _anim;

        public float GetHealth()
        {
            return currentHealth;
        }
        private void Awake()
        {
            PotionHandler = GetComponent<PotionHandler>();
            _anim = GetComponent<Animator>();
        }
        public void TakeDamage(DamageBreakDown damage)
        {
            var attacker = damage.Source;
            if (invincible)
            {
                return;
            }
            if (CurrentAbsorbtions > 0)
            {
                CurrentAbsorbtions--;
                onDamageAbsorbed?.Invoke(damage);
                return;
            }
            onPreDamageApplied?.Invoke(damage);
            if (damage.Damage <= 0)
            {
                return;
            }
            float hp = currentHealth;
            hp -= damage.Damage;
            currentHealth = hp;

            if (currentHealth <= 0)
            {
                onLethalDamage?.Invoke(this);
                if (currentHealth <= 0)
                    Terminate(damage.Source);
            }

            if (_playOnDamage)
                if (gameObject.activeSelf)
                    _anim.Play(_damagedAnimation, 1);

            onDamage?.Invoke(damage);
            onHealthChanged?.Invoke(damage.Damage);

            if (!attacker) return;
            if (damage.Type != DamageType.Execute)
                attacker.Heal(damage.Damage * attacker.EntityStats.Omnivamp.Value);
        }
        public void Heal(float amount)
        {
            float hp = currentHealth;
            float maxHp = EntityStats.Health.Value;
            hp += amount;
            if (hp >= maxHp)
            {
                hp = maxHp;
            }
            currentHealth = hp;
            onHeal?.Invoke(amount);
            onHealthChanged?.Invoke(amount);
        }
        public void AddAbsorbtion(int amount)
        {
            CurrentAbsorbtions += amount;
            onHealthChanged?.Invoke(0);
        }
        public virtual void Initialize()
        {
            _lifeState = LifeState.Alive;
            currentHealth = EntityStats.Health.Value;
            onInitialized?.Invoke();
        }
        public virtual void Terminate(EntityBase killer = null)
        {
            _lifeState = LifeState.Dead;
            if (killer != null)
            {
                var entity = killer;
                if (entity)
                    entity.onKill?.Invoke();
            }
            gameObject.SetActive(false);
            onDeath?.Invoke(this);
        }
        public bool CheckIfOverHealed()
        {
            if (currentHealth > EntityStats.Health.Value)
            {
                currentHealth = EntityStats.Health.Value;
                return true;
            }
            return false;
        }
    }
    public class DamageBreakDown
    {
        public float Damage { get; private set; }
        public EntityBase Source { get; private set; }
        public DamageType Type { get; private set; }
        public DamageBreakDown(float damage, EntityBase source = null, DamageType type = DamageType.Attack)
        {
            Type = type;
            Damage = damage;
            Source = source;
        }
    }
    public enum DamageType
    {
        Attack,
        Execute
    }
}