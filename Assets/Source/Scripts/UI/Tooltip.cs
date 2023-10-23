using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _headerField;
    [SerializeField] TextMeshProUGUI _contentField;
    [SerializeField] LayoutElement _layoutElement;
    [SerializeField] int _charWrapLimit;
    RectTransform _rectTransform;
    private float offsetX = 1;
    private float offsetY = 1;
    [SerializeField] bool _followCursor;

    private void Awake()
    { 
        _rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        WrapText();
        if (!_followCursor) return;
        Vector2 pos = Input.mousePosition;

        float pivotX = (pos.x + offsetX) / Screen.width;
        float pivotY = (pos.y + offsetY )/ Screen.height;
        _rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = pos;
    }
    public void SetText(string content, string header = "")
    {
        if(string.IsNullOrEmpty(header))
        {
            _headerField.gameObject.SetActive(false);
        }
        else
        {
            _headerField.gameObject.SetActive(true);
            _headerField.text = header;
        }

        _contentField.text = content;
        WrapText();
    }
    void WrapText()
    {
        int headerLength = _headerField.text.Length;
        int contentLength = _headerField.text.Length;

        _layoutElement.enabled = (headerLength > _charWrapLimit || contentLength > _charWrapLimit) ? true : false;
    }
}

