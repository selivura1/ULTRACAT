using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : EntityBase
{
    [SerializeField] int SoulDrop = 1;
    [SerializeField] EntityStatbar _statbar = new EntityStatbar();
    [SerializeField] Secret _secret;
    public override void Initialize()
    {
        base.Initialize();
        _statbar.Initialize(this);
    }
    public override void Terminate(EntityBase killer = null)
    {
        var player = (PlayerEntity)killer;
        if (player)
        {
            float chance = player.EntityStats.BonusChance.Value;
            if (Random.Range(0, 100) <= chance)
            {
                SpawnCollectibleNearby(GameManager.Database.Collectibles[0]);
            }
        }
        DropExp();
        _statbar.RemoveDisplay();
        if(_secret)
        {
            GameManager.Database.UnlockSecret(_secret);
        }
        base.Terminate(killer);
    }

    private void DropExp()
    {
        if (SoulDrop > 0)
        {
            for (int i = 0; i < SoulDrop; i++)
            {
                SpawnCollectibleNearby(GameManager.Database.Collectibles[1]);
            }
        }
    }
    private void SpawnCollectibleNearby(Bonus collectible, float range = 1)
    {
        var spawned = GameManager.BonusPool.Get(collectible);
        spawned.transform.position = GetRandomPosNearby(range);
        spawned.Initialize();
    }
    private Vector3 GetRandomPosNearby(float range = 1f)
    {
        return transform.position + new Vector3(Random.Range(-range, range), Random.Range(-range, range));
    }
}
[System.Serializable]
public class EntityStatbar
{
    protected StatBar _display;
    [SerializeField] bool _versus = false;
    [SerializeField] bool _spawnBossBar = false;
    [SerializeField] bool _spawnBar = true;

    public void Initialize(EntityBase entity)
    {
        if (_spawnBar && !_display)
        {
            _display = GameManager.EntityDisplaySpawner.SpawnMobHPBar(entity);
        }
        if (_spawnBossBar)
            GameManager.EntityDisplaySpawner.SpawnBossbar(entity);
        if (_versus)
            GameObject.FindAnyObjectByType<BossfightUI>().ShowVersus(entity.Splash);
    }
    public void RemoveDisplay()
    {
        if (_display)
            _display.Terminate();
    }
}