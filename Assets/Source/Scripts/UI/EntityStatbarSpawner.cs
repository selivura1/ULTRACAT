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
        [SerializeField] StatBar _hpBar;
        [SerializeField] Transform HUDTransform;
        [SerializeField] BossBarManager BossBar;
        public StatBar SpawnMobHPBar(EntityBase target)
        {
            var spawned = Instantiate(_hpBar, HUDTransform);
            spawned.SetOwner(target);
            return spawned;
        }
        public void SpawnBossbar(EntityBase target)
        {
            BossBar.CreateBar(target);
        }
    }
}