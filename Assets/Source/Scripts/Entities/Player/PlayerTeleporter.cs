using UnityEngine;

namespace Ultracat
{
    public class PlayerTeleporter : MonoBehaviour
    {
        Movement _playerMovement;
        private DungeonGenerator _dungeonGenerator;
        void Awake()
        {
            _playerMovement = FindAnyObjectByType<PlayerEntity>().GetComponent<Movement>();
            _dungeonGenerator = GameManager.DungeonGenerator;
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
