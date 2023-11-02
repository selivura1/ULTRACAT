using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] float speed = 15;
    Camera _camera;
    Vector3 worldPos;
    Vector3 dir;
    private void Awake()
    {
        _camera = Camera.main;
    }
    public void Initialize(string text, Color color, Vector3 pos)
    {
        _text.text = text;
        _text.color = color;
        dir = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f));
        worldPos = pos;
        Invoke(nameof(Despawn), 0.5f);
    }
    private void Despawn()
    {
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        worldPos += (speed * Time.fixedDeltaTime * dir);
        transform.position = _camera.WorldToScreenPoint(worldPos);
    }
}
