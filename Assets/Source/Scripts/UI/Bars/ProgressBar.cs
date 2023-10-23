using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress bar")]
    public static void AddLinearProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/LinearProgressBar"));
        if (Selection.activeGameObject)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif
    public float Max = 32;
    public float Min = 0;
    public float CurrentValue = 32;
    public Image Mask;
    public Image fill;
    public Color color = Color.white;
    public Color maxColor = Color.green;
    public bool useMaxColor = false;
    protected virtual void FixedUpdate()
    {
        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        float currentOffset = CurrentValue - Min;
        float maxOffset = Max - Min;
        float fillAmount = currentOffset / maxOffset;
        Mask.fillAmount = fillAmount;
        fill.color = color;
        if(useMaxColor)
        {
            if (Max <= CurrentValue)
                fill.color = maxColor;
        }
    }
}