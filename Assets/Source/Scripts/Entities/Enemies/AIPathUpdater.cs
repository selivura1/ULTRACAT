using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ultracat
{
    [RequireComponent(typeof(AstarPath))]
    public class AIPathUpdater : MonoBehaviour
    {
        private AstarPath _astarPath;
        private DungeonGenerator _dungeonGenerator;
        void Awake()
        {
            _astarPath = GetComponent<AstarPath>();
            _dungeonGenerator = GameManager.DungeonGenerator;
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
