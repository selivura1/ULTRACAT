using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponSecret : Secret
{
    [SerializeField] WeaponBase _weaponToUnlock;
    public override void Unlock()
    {
        GameManager.Database.UnlockWeapon(_weaponToUnlock);
    }
}