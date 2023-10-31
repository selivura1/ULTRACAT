using System;
using UnityEngine;
namespace Ultracat
{
    public class DungeonGenerator : MonoBehaviour
    {
        Dungeon _dungeon { get { return _levelPack[Stage]; } }
        [SerializeField] Dungeon[] _levelPack;
        [SerializeField] Room _lobby;
        public Room CurrentRoom { get; private set; }
        int _roomNumber = 0;
        public int Stage { get; private set; } = 0;
        public Action onRoomSpawned;
        public Action onRoomPreDestroy;
        public Action onStageCompleted;
        public Action onBossFightStart;

        public void CreateDungeon()
        {
            Destroy(_lobby.gameObject);
            ProceedToNextRoom();
        }
        private void CompleteStage()
        {
            onStageCompleted?.Invoke();
            Stage++;
            if (_levelPack.Length <= Stage)
            {
                FindAnyObjectByType<MenuControllerUI>().GameWin();
                return;
            }
            _roomNumber = 0;
            ProceedToNextRoom();
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
            CurrentRoom.Initialize();
            _roomNumber++;
            onRoomSpawned?.Invoke();
            CurrentRoom.Exit.onTriggered += ProceedToNextRoom;
        }
        private void ProceedToNextRoom()
        {
            if (CurrentRoom)
                CurrentRoom.Exit.onTriggered -= ProceedToNextRoom;
            if (_roomNumber == _dungeon.RoomsToBoss)
            {
                SpawnRandomRoom(_dungeon.BossRooms);
                onBossFightStart?.Invoke();
                return;
            }
            else if (_roomNumber > _dungeon.RoomsToBoss)
            {
                CompleteStage();
                return;
            }
            SpawnRandomRoom(_dungeon.Rooms);
        }
    }
}