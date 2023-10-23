using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTimeFreezer : MonoBehaviour
{
     EntityBase entity;
    [SerializeField] float time = .5f;
    private void OnEnable()
    {
        entity = GetComponent<EntityBase>();
        entity.onDamage += FreezeTime;
    }
    private void FreezeTime(DamageBreakDown  fixIt)
    {
        TimeControl.FreezeTime(time);
    }
    private void OnDisable()
    {
        entity.onDamage -= FreezeTime;
    }
}
