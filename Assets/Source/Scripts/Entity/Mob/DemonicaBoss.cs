using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicaBoss : MonoBehaviour
{
    private EntityBase _entity;
    private PlayerEntity _player;
    private Movement _movement;
    [SerializeField] private float _zoneAttackRange = 3;
    [SerializeField] private float _zoneAttackDamageMultiplier = 2;
    [SerializeField] private int _zoneAttackAmount = 3;
    [SerializeField] private Projectile _zoneAttack;
    [SerializeField] private Projectile _meleeAttackProjectile;
    [SerializeField] private Projectile _rangeAttackProjectile;
    [SerializeField] private AudioClip _attackPrepareSound;
    [SerializeField] private AudioClip _zoneSpawnSound;
    [SerializeField] private AudioClip _rangedAttackSound;
    [SerializeField] private AudioClip _meleeAttackSound;
    private GlobalSoundSpawner _soundSpawner;
    private bool _moveToPlayer;
    private void Start()
    {
        _soundSpawner = FindAnyObjectByType<GlobalSoundSpawner>();
        _movement = GetComponent<Movement>();
        _player = GameManager.PlayerSpawner.GetPlayer();
        _entity = GetComponent<EntityBase>();
    }
    private void FixedUpdate()
    {
        if(_moveToPlayer)
        {
            _movement.Move((_player.transform.position - transform.position).normalized);
        }
        else
        {
            _movement.Move(Vector2.zero);
        }
    }
    public void AttackMelee()
    {
        _soundSpawner.PlaySound(_meleeAttackSound, SoundType.Enemy);
       _moveToPlayer = false;
        var spawned = GameManager.AttacksPool.Get(_meleeAttackProjectile);
        spawned.transform.position = transform.position;
        spawned.Initialize(_entity, _player.transform.position - transform.position, _entity.EntityStats.Attack.Value * _zoneAttackDamageMultiplier);
    }
    public void AttackRange()
    {
        _soundSpawner.PlaySound(_rangedAttackSound, SoundType.Enemy);
        _moveToPlayer = false;
        var spawned = GameManager.AttacksPool.Get(_rangeAttackProjectile);
        spawned.transform.position = transform.position;
        spawned.Initialize(_entity, _player.transform.position - transform.position, _entity.EntityStats.Attack.Value * _zoneAttackDamageMultiplier);
    }
    public void AttackZoneSnipe()
    {
        _soundSpawner.PlaySound(_zoneSpawnSound, SoundType.Enemy);
        _moveToPlayer = false;
        var amount = _zoneAttackAmount;
        SpawnZone(_player.transform.position);
        for (int i = 0; i < amount; i++)
        {
            SpawnZone(_player.transform.position + new Vector3(Random.Range(-_zoneAttackRange, _zoneAttackRange), Random.Range(-_zoneAttackRange, _zoneAttackRange)));
        }
    }
    public void SpawnZoneOnPlayer()
    {
        _soundSpawner.PlaySound(_zoneSpawnSound, SoundType.Enemy);
        SpawnZone(_player.transform.position);
    }
    private void SpawnZone(Vector3 pos)
    {
        var spawned = GameManager.AttacksPool.Get(_zoneAttack);
        spawned.transform.position = pos;
        spawned.Initialize(_entity, Vector2.zero, _entity.EntityStats.Attack.Value);
    }
    public void DashToPlayer()
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
