using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trigger to open weapon selector
public class ArmsDealer : Trigger
{
    public WeaponBase[] Weapons { get; private set; }
    public Inventory PlayerInventory { get; private set; }
    //UIManager HUDActivator;
    private GunShopUI gunShopUI;
    private MenuControllerUI UIController;
    private void Setup()
    {
        PlayerInventory = FindAnyObjectByType<PlayerSpawner>().GetPlayer().GetComponent<Inventory>();
        UIController = FindAnyObjectByType<MenuControllerUI>();
        gunShopUI = UIController.GunSelectWindow;
        Weapons = FindAnyObjectByType<Database>().UnlockedWeapons.ToArray();
    }
    public void Equip(int index)
    {
        PlayerInventory.SetWeapon(Weapons[index]);
    }
    public bool AlreadyEquipped(int index)
    {
        return PlayerInventory.Weapon == Weapons[index];
    }
    public override void OnTouch()
    {
        Setup();
        UIController.OpenWindow(gunShopUI.gameObject);
        gunShopUI.SetDisplay(this);
    }
}
