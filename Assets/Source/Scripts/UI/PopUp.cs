using TMPro;
using UnityEngine;


public class PopUp : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] float speed = 15;
    Vector3 worldPos;
    Vector3 dir;
    public void Initialize(string text, Color color, Vector3 pos)
    {
        _text.text = text;
        _text.color = color;
        dir = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f));
        worldPos = pos;
        Destroy(gameObject, 0.8f);
    }
    private void Update()
    {
        worldPos += (speed * Time.deltaTime * dir);
        transform.position = Camera.main.WorldToScreenPoint(worldPos);
    }
}
