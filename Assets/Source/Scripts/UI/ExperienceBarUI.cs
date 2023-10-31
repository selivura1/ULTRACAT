using UnityEngine;
namespace Ultracat
{
    public class ExperienceBarUI : MonoBehaviour
    {
        [SerializeField] ProgressBar experienceBar;
        private PlayerEntity _player;
        private PlayerLevels playerLevels;

        private void Start()
        {
            experienceBar.Min = 0;
        }
        private void OnEnable()
        {
            _player = FindAnyObjectByType<PlayerEntity>();
            playerLevels = _player.GetComponent<PlayerLevels>();
            playerLevels.onExperienceChange += UpdateBar;
            UpdateBar();
        }
        private void OnDisable()
        {
            playerLevels.onExperienceChange -= UpdateBar;
        }
        private void UpdateBar()
        {
            experienceBar.CurrentValue = playerLevels.Experience;
            experienceBar.Max = playerLevels.ExperienceToNextLevel;
        }
    }
}