using UnityEngine;
namespace Ultracat
{
    [CreateAssetMenu]
    public class WeaponSettings : ScriptableObject
    {
        public string Name;
        [TextArea]
        public string Desc;
        public Sprite Icon;
        public SoundType SoundChannel = SoundType.Effect;
        [Header("Attack")]
        public float baseDamage = 0;
        public float percentFromATK = 100f;
        public float attackTime = 0.1f;
        public Vector2 attackSpawnRange;
        public float BaseReloadSpeed = 100;
        [Header("Charge attack")]
        public float MaxChargeTime = 1;
        public float ChargeStartTime { get; protected set; } = 0;
        public float ChargeTime { get; protected set; } = 0;
        public float chargeMultiplier = 2.0f;
        public float chargeAttackTime = 0.4f;
        [Header("Effects")]
        public AudioClip[] Sounds;
        public AudioClip[] hitSounds;
        public float cameraShakeFrec = 0.5f;
        public float cameraShakeAmp = 0.5f;
        public float cameraShakeTime = 0.5f;
        public WeaponAnimations Animations = new WeaponAnimations();
    }
}