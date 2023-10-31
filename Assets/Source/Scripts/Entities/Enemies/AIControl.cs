using Pathfinding;
using UnityEngine;
namespace Ultracat
{
    public enum Turn
    {
        Patrol,
        Idle,
        Chase,
        Attack,
    }

    public class AIControl : MonoBehaviour
    {
        protected Movement _movement;
        protected EntityBase _target;
        protected EntityBase _entity;
        protected Seeker _seeker;
        protected Combat _combat;
        protected Path _path;
        protected Inventory _inventory;
        [SerializeField] protected float moveRange = 5;
        [SerializeField] protected float _idleTime = 2;
        [SerializeField] protected float _minIdleTime = 0.5f;
        [SerializeField] protected float _maxWalkTime = 3;
        protected float _maxWalkTimer = 0;
        protected float _idleTimer;
        [SerializeField] protected float _nextWaypointDistance = 3;
        [SerializeField] protected float _visionRange = 10;
        [SerializeField] protected bool _reachedEndOfPath;
        [Header("Combat")]
        [SerializeField] protected float _prepareTime;
        [SerializeField] protected float _attackRange = 2f;
        [SerializeField] protected bool _preparing = false;
        protected bool _canAttack { get { return _weapon.CheckIfCanAttack() && !_preparing && _target; } }
        protected WeaponBase _weapon;
        protected float _prepareTimer = 0;
        protected int _currentWaypoint = 0;
        protected Vector3 dir;
        protected Vector3 atkDir;
        [SerializeField] protected Turn currentTurn = Turn.Idle;
        public System.Action<float> onAttackPrepare;
        [SerializeField] private LayerMask _wallMask;
        float _lastPathUpdateTime = 0;
        [SerializeField] float _pathUpdateInterval = 1;

        void Start()
        {
            _entity = GetComponent<EntityBase>();
            _seeker = GetComponent<Seeker>();
            _movement = GetComponent<Movement>();
            _combat = GetComponent<Combat>();
            _target = FindAnyObjectByType<PlayerEntity>();
            _inventory = GetComponent<Inventory>();
            _weapon = _inventory.Weapon;
            _entity.onDamage += (DamageBreakDown da) => StartChasingTarget();

            currentTurn = Turn.Idle;
        }
        public void OnPathComplete(Path path)
        {
            if (!path.error)
            {
                _path = path;
                _currentWaypoint = 0;
            }
        }
        private Vector3 GetDirToTarget()
        {
            return (_target.transform.position - transform.position).normalized;
        }
        Vector3 GetRandomPath()
        {
            return transform.position + new Vector3(Random.Range(-moveRange, moveRange), Random.Range(-moveRange, moveRange));
        }
        private void FixedUpdate()
        {
            switch (currentTurn)
            {
                case Turn.Patrol:
                    Patrol();
                    break;
                case Turn.Idle:
                    Idle();
                    break;
                case Turn.Chase:
                    Chase();
                    break;
                case Turn.Attack:
                    Attack();
                    break;
                default:
                    break;
            }
            UpdatePath();
            Move();
        }

        public void UpdatePath()
        {
            if (_path == null)
            {
                return;
            }
            _reachedEndOfPath = false;
            float distanceToWaypoint;
            while (true)
            {
                distanceToWaypoint = Vector3.Distance(transform.position, _path.vectorPath[_currentWaypoint]);
                if (distanceToWaypoint < _nextWaypointDistance)
                {
                    if (_currentWaypoint + 1 < _path.vectorPath.Count)
                    {
                        _currentWaypoint++;
                    }
                    else
                    {
                        _reachedEndOfPath = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (_maxWalkTimer < _maxWalkTime)
            {
                _maxWalkTimer += Time.fixedDeltaTime;
            }
            else
            {
                _maxWalkTimer = 0;
                _reachedEndOfPath = true;
            }
            if (!_reachedEndOfPath)
                dir = GetWaypointDirection();
            else
                dir = Vector3.zero;
        }
        private Vector3 GetWaypointDirection()
        {
            if (_path != null)
                return (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
            else
                return Vector3.zero;
        }

        public void Move()
        {
            _movement.Move(dir);
        }
        protected void StartAttack()
        {
            currentTurn = Turn.Attack;
        }
        public void Attack()
        {
            dir = Vector3.zero;
            if (_canAttack)
            {
                PrepareForAttack();
            }
            else if (_preparing)
            {
                _prepareTimer += Time.fixedDeltaTime;
                if (_prepareTimer >= _prepareTime)
                {
                    _prepareTimer = 0;
                    _preparing = false;
                    ExecuteAttack();
                }
            }
            if (!CheckForTargetinAttackRange())
                StartPatrol();
        }
        public void PrepareForAttack()
        {
            if (_preparing) return;
            onAttackPrepare?.Invoke(_prepareTime);
            _preparing = true;
            atkDir = GetDirToTarget();
        }
        public virtual void ExecuteAttack()
        {
            _combat.StartAttack(atkDir, _weapon);
        }
        public void Idle()
        {
            if (_idleTimer < Random.Range(_minIdleTime, _idleTime))
            {
                _idleTimer += Time.fixedDeltaTime;
            }
            else
            {
                _idleTimer = 0;
                StartPatrol();
            }
        }
        public void StartPatrol()
        {
            currentTurn = Turn.Patrol;
            _seeker.StartPath(transform.position, GetRandomPath(), OnPathComplete);
        }
        protected void Patrol()
        {
            if (CheckForTargetInVision())
                StartChasingTarget();
            if (_reachedEndOfPath)
                _seeker.StartPath(transform.position, GetRandomPath(), OnPathComplete);
        }
        public bool CheckForTargetInVision()
        {
            if (_target && GetDistanceToTarget(_target.transform) <= _visionRange)
            {
                if (CheckForWall())
                    return false;
                return true;
            }
            return false;
        }
        public bool CheckForTargetinAttackRange()
        {
            if (GetDistanceToTarget(_target.transform) <= _attackRange)
            {
                currentTurn = Turn.Attack;
                return true;
            }
            return false;
        }
        private bool CheckForWall()
        {
            bool wall = Physics2D.Linecast(transform.position, _target.transform.position, _wallMask);
            if (wall)
                return true;
            return false;
        }
        private float GetDistanceToTarget(Transform target)
        {
            return Vector2.Distance(transform.position, target.position);
        }
        private void StartChasingTarget()
        {
            _seeker.StartPath(transform.position, _target.transform.position, OnPathComplete);
            currentTurn = Turn.Chase;
        }
        protected void Chase()
        {
            if (CheckForTargetinAttackRange())
            {
                StartAttack();
                return;
            }
            if (Time.fixedTime - _lastPathUpdateTime >= _pathUpdateInterval)
            {
                _seeker.StartPath(transform.position, _target.transform.position, OnPathComplete);
                _lastPathUpdateTime = Time.fixedTime;
            }

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _visionRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }

}