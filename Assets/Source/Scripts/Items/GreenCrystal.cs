using UnityEngine;
namespace Ultracat
{
    public class GreenCrystal : Item
    {
        [SerializeField] private float _healthRequired = 0.7f;
        [SerializeField] private StatModifier _damamgeBuff;
        bool _hasBuff = false;
        protected override void OnStart()
        {
            CheckIfEnoughHealth();
            entity.onHealthChanged += CheckIfEnoughHealth;
        }

        private void CheckIfEnoughHealth(float change = 0)
        {
            if (entity.GetHealth() > entity.EntityStats.Health.Value * _healthRequired)
            {
                if (!_hasBuff)
                {
                    entity.EntityStats.Attack.AddModifier(_damamgeBuff);
                    _hasBuff = true;
                }
            }
            else if (_hasBuff)
            {
                entity.EntityStats.Attack.RemoveModifier(_damamgeBuff);
                _hasBuff = false;
            }
        }
        protected override void OnDespawn()
        {
            entity.onHealthChanged -= CheckIfEnoughHealth;
        }
    }
}