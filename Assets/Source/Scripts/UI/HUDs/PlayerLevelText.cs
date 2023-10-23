using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLevelText : MonoBehaviour
{
    private PlayerSpawner playerSpawner;
    private PlayerLevels playerLevels;
    TMP_Text levelText;
    [SerializeField] string _preValueText = "LV:";

    private void OnEnable()
    {
        levelText = GetComponent<TMP_Text>();
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
        levelText.text = _preValueText + playerLevels.Level.ToString();
    }
}
