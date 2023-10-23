using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProgressBar))]
public class BossBar : MonoBehaviour
{
    ProgressBar _bar;
    EntityBase _entity;
    public void Initialize(EntityBase entity)
    {
        _bar = GetComponent<ProgressBar>();
        _entity = entity;
        _entity.onHealthChanged += UpdateBar;
        _entity.onDeath += HideBar;
    }
    public void UpdateBar(float amount = 0)
    {
        _bar.Max = _entity.EntityStats.Health.Value;
        _bar.CurrentValue = _entity.GetHealth();
    }
    public void HideBar(EntityBase killer)
    {
        _entity.onHealthChanged -= UpdateBar;
        _entity.onDeath -= HideBar;
        Destroy(gameObject);
    }
}
