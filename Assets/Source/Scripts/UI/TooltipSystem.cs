using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    [SerializeField] Tooltip _tooltip;
    private void Start()
    {
        Hide();
    }
    public void Show(string content, string header = "")
    {
        //if (GameManager.UIManager.MenuActive) return;
        _tooltip.SetText(content, header);
        transform.localScale = Vector3.one; ;
    }
    public void Hide()
    {
        if (_tooltip)
            transform.localScale = Vector3.zero; 
    }
}
