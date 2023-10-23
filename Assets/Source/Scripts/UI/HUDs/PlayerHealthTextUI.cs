using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTextUI : MonoBehaviour
{
    TMPro.TMP_Text _text;
    PlayerEntity _player;
    [SerializeField] int _numbersAfterPoint = 1;
    private void Start()
    {
        _player = GameManager.PlayerSpawner.GetPlayer();
        _text = GetComponent<TMPro.TMP_Text>();
        _player.onHealthChanged += UpdateText;
        UpdateText(0);
    }
    private void OnDestroy()
    {
        _player.onHealthChanged -= UpdateText;
    }
    private void UpdateText(float amount)
    {
        string finalText = _player.GetHealth().ToString("F" + _numbersAfterPoint.ToString()) + "/" + _player.EntityStats.Health.Value.ToString("F" + _numbersAfterPoint.ToString());
        if (_player.CurrentAbsorbtions > 0)
            finalText += " ["+_player.CurrentAbsorbtions+"]";
        if (_player.CurrentResurections > 0)
            finalText += " x" + _player.CurrentResurections;
        _text.text = finalText;
    }
}
