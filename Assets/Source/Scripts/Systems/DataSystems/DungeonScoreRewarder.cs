using UnityEngine;

namespace Ultracat
{
    [RequireComponent(typeof(DungeonGenerator))]
    public class DungeonScoreRewarder : MonoBehaviour
    {
        DungeonGenerator _dungeonGenerator;
        [SerializeField] int _roomClearScore = 50;
        [SerializeField] int _stageCompleteScore = 1000;
        void Start()
        {
            _dungeonGenerator = GetComponent<DungeonGenerator>();
            _dungeonGenerator.onRoomPreDestroy += RemoveRoomListener;
            _dungeonGenerator.onRoomSpawned += AddRoomListener;
            _dungeonGenerator.onStageCompleted += OnStageComplete;
        }
        void RemoveRoomListener()
        {
            _dungeonGenerator.CurrentRoom.onRoomClear -= OnRoomClear;
        }
        void AddRoomListener()
        {
            _dungeonGenerator.CurrentRoom.onRoomClear += OnRoomClear;
        }
        public void OnStageComplete()
        {
            Score.AddScore(_stageCompleteScore);
        }
        public void OnRoomClear()
        {
            Score.AddScore(_roomClearScore);
        }
        private void OnDestroy()
        {
            _dungeonGenerator.onRoomPreDestroy -= RemoveRoomListener;
            _dungeonGenerator.onRoomSpawned -= AddRoomListener;
            _dungeonGenerator.onStageCompleted -= OnStageComplete;
        }
    }
}
