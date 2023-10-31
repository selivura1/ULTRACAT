using TMPro;
using UnityEngine;
namespace Ultracat
{
    public class PlayerStatsUI : MonoBehaviour
    {
        TMP_Text _text;

        private void OnEnable()
        {
            _text = GetComponent<TMP_Text>();
            Refresh();
        }
        private void Refresh()
        {
            var player = FindAnyObjectByType<PlayerEntity>().EntityStats;
            _text.text =
                $"Vitality: {player.Health.Value}" +
                $"\nPower: {player.Attack.Value}" +
                $"\nAttack speed: {player.AttackSpeed.Value}" +
                $"\nSpeed: {player.Speed.Value}" +
                $"\nOmnivamp: {player.Omnivamp.Value}" +
                $"\nDrop: {player.BonusChance.Value}";
        }
    }
}