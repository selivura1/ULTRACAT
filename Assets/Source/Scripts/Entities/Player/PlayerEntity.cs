using UnityEngine;
namespace Ultracat
{
    public class PlayerEntity : EntityBase
    {
        [SerializeField] private float immortalTimer = 1f;
        private float immortalStart = 0;
        private void Start()
        {
            Initialize();
            onDeath += (EntityBase a) => immortalStart = Time.time;
        }
        private void OnDestroy()
        {
            onDeath -= (EntityBase a) => immortalStart = Time.time;
        }
        private void Update()
        {
            if (Time.time >= immortalStart + immortalTimer)
            {
                invincible = false;
            }
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.G))
            {
                EntityStats.Health.BaseValue = 999999;
                EntityStats.Attack.BaseValue = 999999;
                CurrentAbsorbtions = 9999;
                Heal(999999);
            }
#endif
        }
    }
}