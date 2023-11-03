using UnityEngine;
namespace Ultracat
{
    public class DemonicaBossAI : MonoBehaviour
    {
        private EntityBase _entity;
        private PlayerEntity _player;
        private Movement _movement;
        private Animator _animator;

        [Header("Explosion")]
        [SerializeField] private Projectile _explosionAttackProjectile;
        [SerializeField] private AudioClip _explosionSpawnSound;
        

        [Header("Explosion attack")]
        [SerializeField] private float _explosionAttackRange = 3;
        [SerializeField] private float _explosionAttackDamageMultiplier = 2;

        [Header("Big explosion attack")]
        [SerializeField] string _bigExplosionAttack = "BigExplosionAttack";
        [SerializeField] private int _bigExplosionAttackAmount = 6;

        [Header("Melee attack")]
        [SerializeField] string _roundMeleeAttack = "RoundMeleeAttack";
        [SerializeField] private Projectile _meleeAttackProjectile;
        [SerializeField] private AudioClip _meleeAttackSound;
        [SerializeField] private AudioClip _attackPrepareSound;

        [Header("Leg Melee attack")]
        [SerializeField] string _legMeleeAttack = "LegMeleeAttack";

        [Header("Punch melee attack")]
        [SerializeField] string _punchMeleeAttack = "PunchMeleeAttack";

        [Header("Barrage attack")]
        [SerializeField] string _barrageAttack = "Barrage";

        [Header("Fireball attack")]
        [SerializeField] string _fireballAttack = "FireballAttack";
        [SerializeField] float _fireballDamageMultiplier = 1;
        [SerializeField] private Projectile _rangeAttackProjectile;
        [SerializeField] private AudioClip _rangedAttackSound;

        int _combo = 0;
        private GlobalSoundSpawner _soundSpawner;
        private bool _moveToPlayer;
        private void Start()
        {
            _soundSpawner = FindAnyObjectByType<GlobalSoundSpawner>();
            _movement = GetComponent<Movement>();
            _player = FindAnyObjectByType<PlayerEntity>();
            _entity = GetComponent<EntityBase>();
            _animator = GetComponent<Animator>();
        }
        private void FixedUpdate()
        {
            if (_moveToPlayer)
            {
                _movement.Move((_player.transform.position - transform.position).normalized, _entity.EntityStats.Speed.Value);
            }
            else
            {
                _movement.Move(Vector2.zero, 0);
            }
        }
        public void StartAttackByCombo()
        {
            switch (_combo)
            {
                case 0:
                    PerformAttack(_barrageAttack);
                    break;
                case 1:
                    PerformAttack(_bigExplosionAttack);
                    break;
                case 2:
                    PerformAttack(_barrageAttack);
                    break;
                case 3:
                    PerformAttack(_fireballAttack);
                    break;
                case 4:
                    PerformAttack(_punchMeleeAttack);
                    break;
                case 5:
                    PerformAttack(_bigExplosionAttack);
                    break;
                case 6:
                    PerformAttack(_roundMeleeAttack);
                    _combo = 0;
                    break;
                default:
                    PerformAttack(_fireballAttack);
                    break;
            }
        }
        public void SpawnCrossExplosionPrison()
        {
            //X0X
            //0X0
            //X0X
            int size = 2;
            Vector3 offset = new Vector3(-1, 1);
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if(!((y == 0 && x == 1) || (y == 1 && x == 2) || (y == 1 && x == 0) || (y == 2 && x == 1)))
                    SpawnExplosion(_player.transform.position + new Vector3(x * size, -y * size) + offset * size);
                }
            }
        }
        public void SpawnPlusExplosionPrison()
        {
            //0X0
            //XXX
            //0X0
            int size = 2;
            Vector3 offset = new Vector3(-1, 1);
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (!((y == 0 && x == 0) || (y == 0 && x == 2) || (y == 2 && x == 0) || (y == 2 && x == 2)))
                        SpawnExplosion(_player.transform.position + new Vector3(x * size, -y * size) + offset * size);
                }
            }
        }
        private void PerformAttack(string attackKey)
        {
            _animator.Play(attackKey, 0);
            _combo++;
        }

        public void AttackRoundMelee()
        {
            _soundSpawner.PlaySound(_meleeAttackSound, SoundType.Enemy);
            _moveToPlayer = false;
            var spawned = GameManager.AttacksPool.Get(_meleeAttackProjectile);
            spawned.transform.position = transform.position;
            spawned.Initialize(_entity, _player.transform.position - transform.position, _entity.EntityStats.Attack.Value);
        }
        public void ShootFireballAtTarget()
        {
            _soundSpawner.PlaySound(_rangedAttackSound, SoundType.Enemy);
            _moveToPlayer = false;
            var spawned = GameManager.AttacksPool.Get(_rangeAttackProjectile);
            spawned.transform.position = transform.position;
            spawned.Initialize(_entity, _player.transform.position - transform.position, _entity.EntityStats.Attack.Value* _fireballDamageMultiplier);
        }
        public void SpawnBigExplosionOnTarget()
        {
            _soundSpawner.PlaySound(_explosionSpawnSound, SoundType.Enemy);
            _moveToPlayer = false;
            var amount = _bigExplosionAttackAmount;
            SpawnExplosion(_player.transform.position);
            for (int i = 0; i < amount; i++)
            {
                SpawnExplosion(_player.transform.position + new Vector3(Random.Range(-_explosionAttackRange, _explosionAttackRange), Random.Range(-_explosionAttackRange, _explosionAttackRange)));
            }
        }
        public void SpawnExplosionOnTarget()
        {
            _soundSpawner.PlaySound(_explosionSpawnSound, SoundType.Enemy);
            SpawnExplosion(_player.transform.position);
        }
        private void SpawnExplosion(Vector3 pos)
        {
            var spawned = GameManager.AttacksPool.Get(_explosionAttackProjectile);
            spawned.transform.position = pos;
            spawned.Initialize(_entity, Vector2.zero, _entity.EntityStats.Attack.Value * _explosionAttackDamageMultiplier);
        }
        public void DashToTarget()
        {
            _soundSpawner.PlaySound(_attackPrepareSound, SoundType.Enemy, 1, 1);
            _moveToPlayer = false;
            transform.position = _player.transform.position;
        }
        public void MoveTowardsPlayer()
        {
            _moveToPlayer = true;
        }
        public void StopMoving()
        {
            _moveToPlayer = false;
        }
    }
}