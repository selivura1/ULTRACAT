using UnityEngine;
namespace Ultracat
{
    //Содержит в себе все референсы на утилки для спавна/пулинга/сейва объектов
    public class GameManager : MonoBehaviour
    {
        public static GlobalSoundSpawner SoundSpawner { get; private set; }
        public static PoolingSystem<EntityBase> EntitySpawner { get; private set; }
        public static DungeonGenerator DungeonGenerator { get; private set; }
        public static PoolingSystem<Bonus> BonusPool { get; private set; }
        public static EntityStatbarSpawner EntityDisplaySpawner { get; private set; }
        public static PopUpSpawner PopUpSpawner { get; private set; }
        public static Database Database { get; private set; }
        public static Options Options { get; private set; }
        public static PoolingSystem<EffectHandler> EffectsPool { get; private set; }
        public static PoolingSystem<Projectile> AttacksPool { get; private set; }

        private void Awake()
        {
            AttacksPool = new PoolingSystem<Projectile>(transform);
            EntitySpawner = new PoolingSystem<EntityBase>(transform);
            EffectsPool = new PoolingSystem<EffectHandler>(transform);
            BonusPool = new PoolingSystem<Bonus>(transform);

            SoundSpawner = FindAnyObjectByType<GlobalSoundSpawner>();
            DungeonGenerator = FindAnyObjectByType<DungeonGenerator>();
            EntityDisplaySpawner = FindAnyObjectByType<EntityStatbarSpawner>();
            PopUpSpawner = FindAnyObjectByType<PopUpSpawner>();
            Database = FindAnyObjectByType<Database>();
            Options = FindAnyObjectByType<Options>();
        }
    }
}