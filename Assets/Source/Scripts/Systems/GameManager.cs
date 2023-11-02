using System.Collections.Generic;
using UnityEngine;
namespace Ultracat
{
    //Содержит в себе все референсы на утилки для спавна/пулинга/сейва объектов
    public class GameManager : MonoBehaviour
    {
        public static GlobalSoundSpawner SoundSpawner { get; private set; }
        public static PoolingSystem<EntityBase> EntitySpawner { get; private set; }
        public static PoolingSystem<Bonus> BonusPool { get; private set; }
        public static PoolingSystem<WeaponBox> WeaponBoxPool { get; private set; }
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
            WeaponBoxPool = new PoolingSystem<WeaponBox>(transform);

            SoundSpawner = FindAnyObjectByType<GlobalSoundSpawner>();
            EntityDisplaySpawner = FindAnyObjectByType<EntityStatbarSpawner>();
            PopUpSpawner = FindAnyObjectByType<PopUpSpawner>();
            Database = FindAnyObjectByType<Database>();
            Options = FindAnyObjectByType<Options>();
        }
#if UNITY_EDITOR
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                AttacksPool.ClearPools();
                BonusPool.ClearPools();
                EntitySpawner.ClearPools();
                EffectsPool.ClearPools();
            }
        }
#endif
        public static List<T> GetRandomObjectsFromList<T>(List<T> list, int amount, bool repeat = false)
        {
            List<T> used = new List<T>();
            List<T> output = new List<T>();
            for (int i = 0; i < amount; i++)
            {
                var generated = list[Random.Range(0, list.Count)];
                while (used.Contains(generated))
                {
                    generated = list[Random.Range(0, list.Count)];
                }
                output.Add(generated);
                used.Add(generated);
            }
            return output;
        }
    }
}