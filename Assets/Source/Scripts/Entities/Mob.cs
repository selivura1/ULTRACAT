using UnityEngine;
namespace Ultracat
{
    public class Mob : EntityBase
    {
        [SerializeField] int SoulDrop = 1;
        [SerializeField] EntityStatbar _statbar = new EntityStatbar();
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
        [SerializeField] bool _versus = false;
        [SerializeField] bool _spawnBossBar = false;
        [SerializeField] bool _spawnBar = true;

        public void Initialize(EntityBase entity)
        {
            if (_spawnBar)
            {
                GameManager.EntityDisplaySpawner.SpawnMobHPBar(entity);
            }
            if (_spawnBossBar)
                GameManager.EntityDisplaySpawner.SpawnBossbar(entity);
            if (_versus)
                GameObject.FindAnyObjectByType<BossfightUI>().ShowVersus(entity.Splash);
        }
    }
}