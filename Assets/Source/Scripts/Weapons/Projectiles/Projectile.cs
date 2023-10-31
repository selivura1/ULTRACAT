using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ultracat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected float lifetime = 1;
        [HideInInspector] public float CurrentSpeed = 500;
        [SerializeField] protected float speed = 500;
        [SerializeField] private bool _destroyOnFirstTarget = true;
        [SerializeField] private bool _melee = false;
        [SerializeField] private bool _enemy = false;
        [SerializeField] private float delay = 0;
        [SerializeField] protected LayerMask wallMask;
        protected Vector2 initPos;
        [Tooltip("Percentage from start damage for each next target")] public float areaDamageForNextTarget = 50;
        [SerializeField] EffectHandler _effect;
        public bool RandomFlipX = false;
        public bool RandomFlipY = false;
        public UnityEvent<Projectile> OnTerminated;
        public UnityEvent<Projectile, EntityBase> OnHit;
        List<EntityBase> _staying = new List<EntityBase>();
        List<EntityBase> _damagedEntities = new List<EntityBase>();
        protected new Rigidbody2D rigidbody;
        protected EntityBase caster;
        private SpriteRenderer _spriteRenderer;
        protected float damage;
        protected bool initialized;
        protected bool delayEnd;
        //private bool _terminated = false;
        protected float timer = 0;
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EntityBase entity))
            {
                _staying.Add(entity);
                if (CheckIfCanOperate())
                {
                    DamageAllStaying();
                }
            }
            if (_destroyOnFirstTarget)
                Terminate();
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out EntityBase entity))
                return;
            if (_damagedEntities.Contains(entity))
            {
                _damagedEntities.Remove(entity);
            }
            if (_staying.Contains(entity))
            {
                _staying.Remove(entity);
            }
        }
        protected void DamageAllStaying()
        {
            for (int i = 0; i < _staying.Count; i++)
            {
                var entity = _staying[i];
                if (entity == caster || _damagedEntities.Contains(entity)) continue;
                if (_enemy && entity.tag != "Player") continue;
                if (_melee)
                {
                    bool wall = Physics2D.Linecast(initPos, entity.transform.position, wallMask);
                    if (!wall)
                    {
                        ApplyDamage(entity);
                    }
                }
                else
                {
                    ApplyDamage(entity);
                }
                if (_destroyOnFirstTarget)
                {
                    Terminate();
                    break;
                }
            }
        }

        private void ApplyDamage(EntityBase entity)
        {
            entity.TakeDamage(new DamageBreakDown(damage, caster));
            _damagedEntities.Add(entity);
            damage *= areaDamageForNextTarget / 100;
            OnHit?.Invoke(this, entity);
        }

        protected bool CheckIfCanOperate()
        {
            if (!initialized || !delayEnd) return false;
            return true;
        }
        protected void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public virtual void Initialize(EntityBase caster, Vector2 direction, float damage)
        {
            OnHit.RemoveAllListeners();
            OnTerminated.RemoveAllListeners();
            initPos = transform.position;
            if (RandomFlipX)
            {
                if (Random.Range(0f, 1f) >= 0.5f)
                {
                    _spriteRenderer.flipX = true;
                }
                else _spriteRenderer.flipX = false;
            }
            if (RandomFlipY)
            {
                if (Random.Range(0f, 1f) >= 0.5f)
                {
                    _spriteRenderer.flipY = true;
                }
                else _spriteRenderer.flipY = false;
            }
            CancelInvoke();
            _damagedEntities = new List<EntityBase>();
            _staying = new List<EntityBase>();
            CurrentSpeed = speed;
            initialized = true;
            this.damage = damage;
            this.caster = caster;
            transform.right = direction;
            timer = lifetime;
            delayEnd = false;
            Invoke(nameof(EndDelay), delay);
        }
        private void FixedUpdate()
        {
            MovementPerFixedUpdate();
            CountdownTimer();
        }
        public virtual void MovementPerFixedUpdate()
        {
            rigidbody.velocity = transform.right * CurrentSpeed * Time.fixedDeltaTime;
        }
        public virtual void CountdownTimer()
        {
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
                Terminate();
        }
        public virtual void Terminate()
        {
            //_terminated = true;
            if (_effect)
            {
                GameManager.EffectsPool.Get(_effect).transform.position = transform.position;
            }
            OnTerminated?.Invoke(this);
            gameObject.SetActive(false);
        }
        protected void EndDelay()
        {
            delayEnd = true;
            DamageAllStaying();
        }
    }
}