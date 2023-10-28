using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class GameOverTextUI : MonoBehaviour
{
    [SerializeField] string[] _texts;
    TMPro.TMP_Text _text;
    private void OnEnable()
    {
        _text = GetComponent<TMPro.TMP_Text>();
        _text.text = _texts[Random.Range(0, _texts.Length)];
    }
}
