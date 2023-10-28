using UnityEngine;
namespace Ultracat
{
    public class ExperienceBarUI : MonoBehaviour
    {
        [SerializeField] ProgressBar experienceBar;
        private PlayerSpawner playerSpawner;
        private PlayerLevels playerLevels;

        private void Start()
        {
            experienceBar.Min = 0;
        }
        private void OnEnable()
        {
            playerSpawner = FindAnyObjectByType<PlayerSpawner>();
            playerLevels = playerSpawner.GetPlayer().GetComponent<PlayerLevels>();
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