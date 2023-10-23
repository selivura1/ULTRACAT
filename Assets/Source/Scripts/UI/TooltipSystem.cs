using System.Collections;
using System.Collections.Generic;
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
        LeanTween.scale(_tooltip.gameObject, Vector3.one, .1f).setIgnoreTimeScale(true); ;
    }
    public  void Hide()
    {
        if(_tooltip)
        LeanTween.scale(_tooltip.gameObject, Vector3.zero, .1f).setIgnoreTimeScale(true); ;
    }
}
