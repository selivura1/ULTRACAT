using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        foreach (var item in GameManager.Database.CurrentSecrets)
        {
            var spawned =  Instantiate(_displayPrefab, _layout);
            spawned.Initialize(item.displayName, item.description, item.Icon);
            _spawned.Add(spawned);
        }
    }
}
