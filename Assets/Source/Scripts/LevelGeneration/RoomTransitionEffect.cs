using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ultracat
{
    [RequireComponent(typeof(DungeonGenerator))]
    public class RoomTransitionEffect : MonoBehaviour
    {
        private DungeonGenerator _dungeonGenerator;
        private FadeUI _fadeUI;
        private void Start()
        {
            _fadeUI = FindAnyObjectByType<FadeUI>();
            _dungeonGenerator = GetComponent<DungeonGenerator>();
            _dungeonGenerator.onRoomSpawned += _fadeUI.Fade;
        }
        private void OnDestroy()
        {
            _dungeonGenerator.onRoomSpawned -= _fadeUI.Fade;
        }
    }
}
