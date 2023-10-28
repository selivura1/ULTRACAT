using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ultracat
{
    public class SecretsUI : MonoBehaviour
    {
        [SerializeField] private SecretDisplay _displayPrefab;
        [SerializeField] private Transform _layout;
        [SerializeField] private TMP_Text _titleText;
        private List<SecretDisplay> _spawned = new List<SecretDisplay>();
        private void OnEnable()
        {
            Refresh();
        }
        public void Refresh()
        {
            _titleText.text = "SECRETS (" + GameManager.Database.SecretProgress + "/" + GameManager.Database.SecretAmount + ")";
            for (int i = 0; i < _spawned.Count; i++)
            {
                Destroy(_spawned[i].gameObject);
            }
            _spawned.Clear();
            foreach (int index in GameManager.Database.CurrentSecrets)
            {
                Secret secret = GameManager.Database.Secrets[index];
                SecretDisplay spawned = Instantiate(_displayPrefab, _layout);
                spawned.Initialize(secret.displayName, secret.description, secret.Icon);
                _spawned.Add(spawned);
            }
        }
    }
}
