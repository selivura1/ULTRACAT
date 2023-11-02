using UnityEngine;
namespace Ultracat
{
    public enum BarType
    {
        Default,
        Detailed
    }

    public class EntityStatbarSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool<StatBar> _barPool;
        [SerializeField] StatBar _hpBar;
        [SerializeField] Transform HUDTransform;
        [SerializeField] BossBarManager BossBar;
        private void Awake()
        {
            _barPool = new ObjectPool<StatBar>(_hpBar, 5, HUDTransform);
        }
        public StatBar SpawnMobHPBar(EntityBase target)
        {
            var spawned = _barPool.GetFreeElement();
            spawned.Initialize(target);
            return spawned;
        }
        public void SpawnBossbar(EntityBase target)
        {
            BossBar.CreateBar(target);
        }
    }
}