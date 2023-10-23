using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarletDeathWeapon : KillstackingWeapon
{
    [SerializeField] private float _reloadSpeedPerStack = 1;
    protected override void OnStackUpdated()
    {
        reloadSpeed = WeaponSettings.BaseReloadSpeed + stacks * _reloadSpeedPerStack;
    }
}
