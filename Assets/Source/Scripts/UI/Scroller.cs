using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;
    private int _xDirection, _yDirection;
    private void OnEnable()
    {
        if (Random.value < 0.5f)
            _xDirection = -1;
        else
            _xDirection = 1;
        if (Random.value < 0.5f)
            _yDirection = -1;
        else
            _yDirection = 1;
    }
    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x * _xDirection, _y * _yDirection) * Time.unscaledDeltaTime, _img.uvRect.size);
    }
}
