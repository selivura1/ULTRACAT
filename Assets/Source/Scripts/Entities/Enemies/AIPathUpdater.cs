using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ultracat
{
    public class AIPathUpdater : MonoBehaviour
    {
        private AstarPath _astarPath;
        private DungeonGenerator _dungeonGenerator;
        void Awake()
        {
            _astarPath = FindAnyObjectByType<AstarPath>();
            _dungeonGenerator = FindAnyObjectByType<DungeonGenerator>();
            _dungeonGenerator.onRoomSpawned += UpdateTheGrid;
        }
        private void OnDestroy()
        {
            _dungeonGenerator.onRoomSpawned -= UpdateTheGrid;
        }
        private void UpdateTheGrid()
        {
            _astarPath.Scan();
        }
    }
}
