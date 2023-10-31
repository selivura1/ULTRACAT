using UnityEngine;
namespace Ultracat
{
    //Player's HP bar HUD script
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] ProgressBar _healthBar;
        EntityBase _player;
        private void Start()
        {
            _player = FindAnyObjectByType<PlayerEntity>();
            if (_player)
            {
                _player.onHealthChanged += UpdateBars;
                UpdateBars();
            }
        }
        private void OnDestroy()
        {
            if (_player)
            {
                _player.onHealthChanged -= UpdateBars;
                UpdateBars();
            }
        }
        private void UpdateBars(float hp = 0)
        {
            _healthBar.Max = _player.EntityStats.Health.Value;
            _healthBar.CurrentValue = _player.GetHealth();
            _healthBar.Min = 0;
        }
    }
}