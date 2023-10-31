using DG.Tweening;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    [SerializeField] Tooltip _tooltip;
    private const float TweeningDuration = 0.1f;
    private void Start()
    {
        Hide();
    }
    public void Show(string content, string header = "")
    {
        _tooltip.SetText(content, header);
        _tooltip.transform.DOScale(1, TweeningDuration).SetUpdate(true); 
    }
    public void Hide()
    {
        if (_tooltip)
            _tooltip.transform.DOScale(0, TweeningDuration).SetUpdate(true);
    }
}
