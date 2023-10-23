using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinedCatBoss : MonoBehaviour
{
    [SerializeField] AttackTrack _attackTrace;
    [SerializeField] Projectile _attackProjectileTrack;
    [SerializeField] Projectile _attackProjectileSimple;
    [SerializeField] TargetableProjectile _attackProjectileHoming;
    [SerializeField] string[] _attackList;
    [SerializeField] AudioClip _shootSound;
    [SerializeField] AudioClip _ghostSound;
    GlobalSoundSpawner _soundSpawner;
    Animator _anim;
    PlayerEntity _player;
    EntityBase _user;

    private void Start()
    {
        _soundSpawner = FindAnyObjectByType<GlobalSoundSpawner>();
        _anim = GetComponent<Animator>();
        _player = GameManager.PlayerSpawner.GetPlayer();
        _user = GetComponent<EntityBase>();
    }
    public void GoToRandomAttack()
    {
        string attack = _attackList[Random.Range(0, _attackList.Length)];
        _anim.Play(attack);
    }
    public void SpawnHomingGhost()
    {
        _soundSpawner.PlaySound(_ghostSound, SoundType.Enemy,1,1);
        TargetableProjectile projectileSpawned = (TargetableProjectile)GameManager.AttacksPool.Get(_attackProjectileHoming);
        projectileSpawned.transform.position = transform.position;
        projectileSpawned.Initialize(_user, Vector2.zero, _user.EntityStats.Attack.Value * 1f);
        projectileSpawned.Target = _player;
    }

    public void SpawnSimpleBullet()
    {
        _soundSpawner.PlaySound(_shootSound, SoundType.Enemy);
        Projectile projectileSpawned = GameManager.AttacksPool.Get(_attackProjectileSimple);
        projectileSpawned.transform.position = transform.position;
        projectileSpawned.Initialize(_user, _player.transform.position - transform.position, _user.EntityStats.Attack.Value * .75f);
    }
    public void SpawnAttackTrack()
    {
        var spawned = Instantiate(_attackTrace, _player.transform.position, Quaternion.identity);
        spawned.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        var projectileSpawned = GameManager.AttacksPool.Get(_attackProjectileTrack);
        projectileSpawned.transform.position = spawned.StartingPoints[Random.Range(0,spawned.StartingPoints.Length)].position;
        projectileSpawned.Initialize(_user, spawned.Center.position - projectileSpawned.transform.position, _user.EntityStats.Attack.Value * 0.5f);
        Destroy(spawned.gameObject, 1);
    }
}
