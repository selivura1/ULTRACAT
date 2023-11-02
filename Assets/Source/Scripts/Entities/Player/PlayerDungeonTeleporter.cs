using UnityEngine;

namespace Ultracat
{
    [RequireComponent (typeof(DungeonGenerator))]
    public class PlayerDungeonTeleporter : MonoBehaviour
    {
        Movement _playerMovement;
        private DungeonGenerator _dungeonGenerator;
        void Awake()
        {
            _playerMovement = FindAnyObjectByType<PlayerEntity>().GetComponent<Movement>();
            _dungeonGenerator = GetComponent<DungeonGenerator>();
            _dungeonGenerator.onRoomSpawned += TeleportToTheRoomStart;
        }
        private void OnDestroy()
        {
            _dungeonGenerator.onRoomSpawned -= TeleportToTheRoomStart;
        }
        private void TeleportToTheRoomStart()
        {
            _playerMovement.Teleport(_dungeonGenerator.CurrentRoom.PlayerRespawn.position);
        }

    }
}
