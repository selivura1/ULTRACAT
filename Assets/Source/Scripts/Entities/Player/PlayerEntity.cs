using UnityEngine;
namespace Ultracat
{
    public class PlayerEntity : EntityBase
    {
        [SerializeField] private float immortalTimer = 1f;
        private float _immortalStart = 0;
        private void Start()
        {
            Initialize();
            onDeath += (EntityBase a) => _immortalStart = Time.fixedTime;
        }
        private void OnDestroy()
        {
            onDeath -= (EntityBase a) => _immortalStart = Time.fixedTime;
        }
        private void FixedUpdate()
        {
            if (Time.fixedTime >= _immortalStart + immortalTimer)
            {
                invincible = false;
            }
        }
        private void Update()
        {
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