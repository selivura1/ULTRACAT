using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnchancedBar : ProgressBar
{
    public RectTransform _maskTransform;
    public float ValuePerUnit = .32f;
    public float MaxSize = 224;
    public float MaxDelta = 1;
    float timer = 0;
    float _lastCurrent;
    [SerializeField] ProgressBar _deltaBar;
    [SerializeField] float _deltaResetTime = 2f;
    [SerializeField] bool _waitForTimeBeforeBarSync = true;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        ResizeOnValue();
    }
    private void ResizeOnValue()
    {
        _deltaBar.Max = Max;
        _deltaBar.Min = Min;
        _deltaBar.CurrentValue = Mathf.Clamp(_deltaBar.CurrentValue, CurrentValue, Max);
        if (_lastCurrent != CurrentValue)
        {
            timer = _deltaResetTime;
            _lastCurrent = CurrentValue;
        }
        if (_waitForTimeBeforeBarSync)
            if (timer > 0)
            {
                timer -= Time.fixedDeltaTime;
                return;
            }
        _deltaBar.CurrentValue = Mathf.MoveTowards(_deltaBar.CurrentValue, CurrentValue, Time.fixedDeltaTime * MaxDelta * Max / 100);
    }
}