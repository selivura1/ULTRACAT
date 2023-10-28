//Trigger to open weapon selector
namespace Ultracat
{
    public class ArmsDealer : Trigger
    {
        public WeaponBase[] Weapons { get; private set; }
        public Inventory PlayerInventory { get; private set; }
        //UIManager HUDActivator;
        private GunShopUI GunSelector;
        private MenuControllerUI UIController;
        private void Setup()
        {
            PlayerInventory = FindAnyObjectByType<PlayerSpawner>().GetPlayer().GetComponent<Inventory>();
            UIController = FindAnyObjectByType<MenuControllerUI>();
            GunSelector = UIController.GunSelectWindow;
            Weapons = FindAnyObjectByType<Database>().UnlockedWeapons.ToArray();
        }
        public void Equip(int index)
        {
            PlayerInventory.SetWeapon(Weapons[index]);
        }
        public bool IsAlreadyEquipped(int index) => PlayerInventory.Weapon == Weapons[index];
        public override void OnTouch()
        {
            Setup();
            UIController.OpenWindow(GunSelector.gameObject);
            GunSelector.SetDisplay(this);
        }
    }
}