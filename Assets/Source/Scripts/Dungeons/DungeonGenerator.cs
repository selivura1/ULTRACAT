using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class DungeonGenerator : MonoBehaviour
{
    Dungeon _dungeon { get { return _levelPack[stage]; } }
    [SerializeField] Dungeon[] _levelPack;
    [SerializeField] AstarPath _pathfinder;
    public Room CurrentRoom { get; private set; }
    Movement playerMovement;
    PlayerSpawner playerSpawner;
    int roomNumber = -1;
    int stage = 0;
    public Action onRoomSpawned;
    public Action onRoomPreDestroy;
    public Action onStageCompleted;
    public Action onBossFightStart;

    public void CreateDungeon()
    {
        roomNumber = 0; 
        ProceedToNextRoom();
        //Log.WriteInLog("Welcome to the PELMENI dungeon!");
    }
    public void DungeonComplete()
    {
        if(_dungeon._secretForComplete)
        {
            GameManager.Database.UnlockSecret(_dungeon._secretForComplete);
        }
        onStageCompleted?.Invoke();
        stage++;
        if (_levelPack.Length <= stage)
        {
            FindAnyObjectByType<MenuControllerUI>().GameWin();
            return;
        }
        roomNumber = 0;
        ProceedToNextRoom();
    }
    private void Start()
    {
        playerSpawner = FindAnyObjectByType<PlayerSpawner>();
        playerMovement = playerSpawner.GetPlayer().GetComponent<Movement>();
    }
    private void SpawnRandomRoom(Room[] pool)
    {
        var room = pool[UnityEngine.Random.Range(0, pool.Length)];
        while (room == CurrentRoom)
        {
            room = pool[UnityEngine.Random.Range(0, pool.Length)];
        }
        SpawnRoom(room);
    }
    private void SpawnRoom(Room room)
    {
        if (CurrentRoom)
        {
            onRoomPreDestroy?.Invoke();
            Destroy(CurrentRoom.gameObject);
        }
        CurrentRoom = Instantiate(room, transform);
        playerMovement.Teleport(CurrentRoom.PlayerRespawn.position);
        _pathfinder.Scan();
        
        CurrentRoom.Initialize();
        roomNumber++;
        onRoomSpawned?.Invoke();
    }
    public void ProceedToNextRoom()
    {
        if (roomNumber == _dungeon.RoomsToBoss)
        {
            SpawnRandomRoom(_dungeon.BossRooms);
            onBossFightStart?.Invoke();
            return;
        }
        //if(roomNumber == _dungeon.RoomsToTreasure)
        //{
        //    SpawnRandomRoom(_dungeon.TreasureRooms);
        //    return;
        //}
        SpawnRandomRoom(_dungeon.Rooms);
    }
}
