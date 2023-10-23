using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ItemSecret : Secret
{
    [SerializeField] Item _itemToUnlock;
    public override void Unlock()
    {
        GameManager.Database.UnlockItem(_itemToUnlock);
    }
}