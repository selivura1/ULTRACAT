using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//Script for room logic such as mob spawn
public class Room : MonoBehaviour
{
    public Transform PlayerRespawn;
    List<EntityBase> spawnedMobs = new List<EntityBase>();
    [SerializeField] RoomExit _exit;
    public bool IsClear { get; private set; }
    public Action onRoomClear;
    public void Initialize()
    {
        SpawnRoomMobs();
        CheckIfRoomClear();
    }
    public void SpawnRoomMobs()
    {
        MobSpawnPoint[] mobSpawnPoints = FindObjectsByType<MobSpawnPoint>(FindObjectsSortMode.InstanceID);
        for (int i = 0; i < mobSpawnPoints.Length; i++)
        {
            var spawned = GameManager.EntitySpawner.Get(mobSpawnPoints[i].Mob);
            spawned.transform.position = mobSpawnPoints[i].transform.position;
            spawned.Initialize();
            spawnedMobs.Add(spawned);
        }
        foreach (var mob in spawnedMobs)
        {
            mob.onDeath += RemoveDeadMob;
        }
        foreach (var spawnPoint in mobSpawnPoints)
        {
            spawnPoint.gameObject.SetActive(false);
        }
    }
    private void RemoveDeadMob(EntityBase deadMob)
    {
        spawnedMobs.Remove(deadMob);
        deadMob.onDeath -= RemoveDeadMob;
        CheckIfRoomClear();
    }
    bool CheckIfRoomClear()
    {
        if (spawnedMobs.Count <= 0)
        {
            ClearRoom();
            return true;
        }
        else if(IsClear != false)
        {
            LockRoom();
        }
        return false;
    }

    public void ClearRoom()
    {
        IsClear = true;
        _exit.Activate();
        onRoomClear?.Invoke();
    }
    public void LockRoom()
    {
        IsClear = false;
        _exit.Deactivate();
    }
}
