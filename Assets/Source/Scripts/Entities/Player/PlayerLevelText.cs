using TMPro;
using UnityEngine;
namespace Ultracat
{
    public class PlayerLevelText : MonoBehaviour
    {
        private PlayerEntity _player;
        private PlayerLevels playerLevels;
        TMP_Text levelText;
        [SerializeField] string _preValueText = "LV:";

        private void OnEnable()
        {
            levelText = GetComponent<TMP_Text>();
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
            levelText.text = _preValueText + playerLevels.Level.ToString();
        }
    }
}