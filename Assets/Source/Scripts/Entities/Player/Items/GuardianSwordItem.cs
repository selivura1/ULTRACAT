using UnityEngine;
namespace Ultracat
{
    public class GuardianSwordItem : Item
    {
        int _charges = 0;
        [SerializeField] int _chargesNeeded = 5;
        [SerializeField] EffectHandler _rechargeEffect;
        [SerializeField] EffectHandler _onUseEffect;
        [SerializeField] AudioClip _onRechargeSound;
        [SerializeField] AudioClip _onUseSound;
        Animator _animator;
        [SerializeField] string _chargedAnimKey = "charged";
        protected override void OnStart()
        {
            _animator = GetComponentInChildren<Animator>();
            entity.onDamageAbsorbed += OnDamage;
            entity.onKill += AddCharge;
            for (int i = 0; i < _chargesNeeded; i++)
            {
                AddCharge();
            }
        }
        protected void AddCharge()
        {
            _charges++;
            if (_charges == _chargesNeeded)
            {
                _animator.SetBool(_chargedAnimKey, true);
                GameManager.SoundSpawner.PlaySound(_onRechargeSound, SoundType.Effect);
                entity.AddAbsorbtion(1);
            }
        }

        protected void OnDamage(DamageBreakDown damage)
        {
            if (_charges < _chargesNeeded) return;
            _charges = 0;
            _animator.SetBool(_chargedAnimKey, false);
            GameManager.SoundSpawner.PlaySound(_onUseSound, SoundType.Effect);
        }
    }
}