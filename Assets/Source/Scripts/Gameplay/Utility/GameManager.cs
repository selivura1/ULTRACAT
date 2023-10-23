using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Содержит в себе все референсы на утилки для спавна/пулинга/сейва объектов
public class GameManager : MonoBehaviour
{
    public static GlobalSoundSpawner SoundSpawner { get; private set; }
    public static PoolingSystem<EntityBase> EntitySpawner { get; private set; }
    public static DungeonGenerator DungeonGenerator { get; private set; }
    public static PoolingSystem<Bonus> BonusPool { get; private set; }
    public static EntityStatbarSpawner EntityDisplaySpawner { get; private set; }
    public static PopUpSpawner PopUpSpawner { get; private set; }
    public static PlayerSpawner PlayerSpawner { get; private set; }
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
        PlayerSpawner = FindAnyObjectByType<PlayerSpawner>();
        Database = FindAnyObjectByType<Database>();
        Options = FindAnyObjectByType<Options>();
    }
}
[System.Serializable]
public class PoolingSystem<T> where T : MonoBehaviour
{
    private List<ObjectPool<T>> _pools = new List<ObjectPool<T>>();
    private Transform _container;
    public PoolingSystem(Transform container)
    {
        _container = container;
    }
    public T Get(T prefab)
    {
        T output;
        foreach (var item in _pools)
        {
            if (item.Prefab == prefab)
            {
                output = item.GetFreeElement();
                return output;
            }
        }
        var newPool = new ObjectPool<T>(prefab, 0, _container);
        _pools.Add(newPool);
        output = newPool.GetFreeElement();
        return output;
    }
}

[System.Serializable]
public class ObjectPool<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool AutoExpand { get; set; } = true;
    public Transform Container { get; }

    private List<T> _pool;

    public ObjectPool(T prefab, int count)
    {
        Prefab = prefab;
        Container = null;
        CreatePool(count);
    }

    public ObjectPool(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDef = false)
    {
        var spawned = Object.Instantiate(Prefab, Container);
        spawned.gameObject.SetActive(isActiveByDef);
        _pool.Add(spawned);
        return spawned;
    }

    public bool HasFreeElement(out T elem)
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                elem = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }
        elem = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var elem))
        {
            return elem;
        }
        if (AutoExpand)
            return CreateObject(true);

        throw new System.Exception("BRUH NO ELEM&&&!&!&!& (((");
    }
}
