using UnityEngine;
namespace Ultracat
{
    public class KeeperBoss : MonoBehaviour
    {
        EntityBase _entity;
        PlayerEntity _player;
        GlobalSoundSpawner _soundSpawner;
        [SerializeField] Projectile _attackProjectile;
        [SerializeField] float _rangedAttackDamageMultiplier = 2;
        [SerializeField] Projectile _meleeAttackProjectile;
        [SerializeField] AudioClip _prepareForAttackSound;
        [SerializeField] AudioClip _meleeAttackSound;
        [SerializeField] AudioClip _laserAttackSound;

        private void Start()
        {
            _soundSpawner = FindAnyObjectByType<GlobalSoundSpawner>();
            _player = GameManager.PlayerSpawner.GetPlayer();
            _entity = GetComponent<EntityBase>();
        }
        public void AttackPrepare()
        {
            _soundSpawner.PlaySound(_prepareForAttackSound, SoundType.Enemy, 1, 1);
        }
        public void Attack()
        {
            _soundSpawner.PlaySound(_meleeAttackSound, SoundType.Enemy);
            var spawned = GameManager.AttacksPool.Get(_meleeAttackProjectile);
            spawned.transform.position = transform.position;
            spawned.Initialize(_entity, _player.transform.position - transform.position, _entity.EntityStats.Attack.Value * _rangedAttackDamageMultiplier);
        }
        public void Attack2()
        {
            _soundSpawner.PlaySound(_laserAttackSound, SoundType.Enemy);
            var spawned = GameManager.AttacksPool.Get(_attackProjectile);
            spawned.transform.position = transform.position;
            spawned.Initialize(_entity, _player.transform.position - transform.position, _entity.EntityStats.Attack.Value * _rangedAttackDamageMultiplier);
        }
    }
}