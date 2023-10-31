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
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        _tooltipSystem.Show(content, header);
    }
    private void OnDisable()
    {
        _tooltipSystem.Hide();
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        _tooltipSystem.Hide();
    }
    public void OnMouseEnter()
    {
        _tooltipSystem.Show(content, header);
    }
    public void OnMouseExit()
    {
        _tooltipSystem.Hide();
    }
}
