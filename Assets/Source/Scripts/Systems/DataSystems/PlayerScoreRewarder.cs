using UnityEngine;

namespace Ultracat
{
    [RequireComponent(typeof(PlayerEntity))]
    [RequireComponent(typeof(PlayerLevels))]
    public class PlayerScoreRewarder : MonoBehaviour
    {
        PlayerEntity _player;
        PlayerLevels _playerLevels;
        [SerializeField] int _killScore = 10;
        [SerializeField] int _levelUpScore = 200;
        private void Start()
        {
            _player = GetComponent<PlayerEntity>();
            _playerLevels = GetComponent<PlayerLevels>();
            _playerLevels.onLevelUp += OnlevelUp;
        }
        private void OnlevelUp()
        {
            Score.AddScore(_levelUpScore);
        }
        private void OnKill()
        {
            Score.AddScore(_killScore);
        }
        private void OnDestroy()
        {
            _player.onKill -= OnKill;
            _playerLevels.onLevelUp -= OnlevelUp;
        }
    }
}
