using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;
    [TextArea]
    public string content;

    TooltipSystem _tooltipSystem;
    private void Awake()
    {
        _tooltipSystem = FindAnyObjectByType<TooltipSystem>();
        enabled = false;
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (_tooltipSystem != null)
            _tooltipSystem.Show(content, header);
    }
    private void OnDisable()
    {
        if (_tooltipSystem != null)
            _tooltipSystem.Hide();
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (_tooltipSystem != null)
            _tooltipSystem.Hide();
    }
    public void OnMouseEnter()
    {
        if (_tooltipSystem != null)
            _tooltipSystem.Show(content, header);
    }
    public void OnMouseExit()
    {
        if (_tooltipSystem != null)
            _tooltipSystem.Hide();
    }
}
