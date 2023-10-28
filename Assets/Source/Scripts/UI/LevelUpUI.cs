using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Ultracat
{
    public class LevelUpUI : MonoBehaviour
    {
        [SerializeField] GameObject _window;
        [SerializeField] List<Image> _images = new List<Image>();
        [SerializeField] List<TooltipTrigger> _triggers = new List<TooltipTrigger>();
        [SerializeField] TMP_Text _text;
        PlayerLevels _levels;
        private void Start()
        {
            _levels = GameManager.PlayerSpawner.GetPlayer().GetComponent<PlayerLevels>();
            _levels.onItemsGenerated += Refresh;
        }
        private void OnDestroy()
        {
            _levels.onItemsGenerated -= Refresh;
        }
        public void SelectItem(int index)
        {
            _levels.UseToken(index);
            Refresh();
        }
        private void Refresh()
        {
            for (int i = 0; i < _images.Count; i++)
            {
                _images[i].sprite = _levels.RandomItems[i].Picture;
                _triggers[i].header = _levels.RandomItems[i].name;
                _triggers[i].content = _levels.RandomItems[i].GetDescription();
            }
            _text.text = "LEVEL UP(" + _levels.Tokens + ")";
            if (_levels.Tokens > 0)
            {
                _window.SetActive(true);
            }
            else
                _window.SetActive(false);
        }
    }
}