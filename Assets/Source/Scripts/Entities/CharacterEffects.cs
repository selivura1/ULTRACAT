using UnityEngine;

namespace Ultracat
{
    public class CharacterEffects : MonoBehaviour
    {
        protected EntityBase entity;
        [SerializeField] EffectHandler[] onDeathEffects;
        [SerializeField] EffectHandler[] onDeathRandomEffects;
        [SerializeField] EffectHandler[] onDanageRandomEffects;
        [SerializeField] EffectHandler[] onHealEffects;
        protected virtual void Start()
        {
            entity = GetComponent<EntityBase>();
            if (entity)
            {
                entity.onDeath += SpawnDeathEffects;
                entity.onDamage += SpawnDamageEffects;
                // entity.onHeal += SpawnHealthEffect;
            }
        }
        protected virtual void OnDestroy()
        {
            if (entity)
            {
                entity.onDeath -= SpawnDeathEffects;
                //entity.onHeal -= SpawnHealthEffect;
            }
        }
        public void SpawnDamageEffects(DamageBreakDown da)
        {

            if (onDanageRandomEffects.Length > 0)
                GameManager.EffectsPool.Get(onDanageRandomEffects[Random.Range(0, onDanageRandomEffects.Length)]).transform.position = transform.position;
        }
        public void SpawnDeathEffects(EntityBase fix)
        {
            foreach (var item in onDeathEffects)
            {
                GameManager.EffectsPool.Get(item).transform.position = transform.position;
            }
            if (onDeathRandomEffects.Length > 0)
                GameManager.EffectsPool.Get(onDeathRandomEffects[Random.Range(0, onDeathRandomEffects.Length)]).transform.position = transform.position;
        }
        public void SpawnHealthEffect(float amount)
        {
            foreach (var item in onHealEffects)
            {
                var spawned = GameManager.EffectsPool.Get(item);
                spawned.transform.SetParent(transform);
                spawned.transform.localPosition = Vector3.zero;
            }
        }
    }
}