using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ultracat
{
    [RequireComponent(typeof(DungeonGenerator))]
    public class RoomCleaner : MonoBehaviour
    {
        DungeonGenerator _dungeonGenerator;
        private void Awake()
        {
            _dungeonGenerator = GetComponent<DungeonGenerator>();
            _dungeonGenerator.onRoomPreDestroy += CleanUp;
        }
        private void OnDestroy()
        {
            _dungeonGenerator.onRoomPreDestroy -= CleanUp;
        }
        private void CleanUp()
        {
            GameManager.WeaponBoxPool.DeactivateAll();
            GameManager.BonusPool.DeactivateAll();
        }
    }
}
