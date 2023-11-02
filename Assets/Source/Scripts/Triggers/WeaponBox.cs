using UnityEngine;

namespace Ultracat
{
    public class WeaponBox : Trigger
    {
        public WeaponBase[] Weapons { get; private set; }
        public Inventory PlayerInventory { get; private set; }
        //UIManager HUDActivator;
        private GunShopUI GunSelector;
        private MenuControllerUI UIController;
        [SerializeField] private int _amountOfWeapons = 2;
        [SerializeField] private float _unlockDelay = 0.5f;
        private bool _locked;
        private void Awake()
        {
            UIController = FindAnyObjectByType<MenuControllerUI>();
            GunSelector = UIController.GunSelectWindow;
            PlayerInventory = FindAnyObjectByType<PlayerEntity>().GetComponent<Inventory>();
        }
        public void Initialize()
        {
            _locked = true;
            Invoke(nameof(RemoveLock), _unlockDelay);
            RandomizeWeapons();
        }
        private void RemoveLock()
        {
            _locked = false;
        }
        private void RandomizeWeapons()
        {
            var weapons = FindAnyObjectByType<Database>().UnlockedWeapons;
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].name == PlayerInventory.Weapon.name)
                    weapons.RemoveAt(i);
            }
            Weapons = GameManager.GetRandomObjectsFromList(weapons, _amountOfWeapons, false).ToArray();
        }
        public void Equip(int index)
        {
            PlayerInventory.SetWeapon(Weapons[index]);
            gameObject.SetActive(false);
        }
        public bool IsAlreadyEquipped(int index) => PlayerInventory.Weapon == Weapons[index];
        public override void OnTouch(Collision2D collision)
        {
            if (_locked) return;
            if (collision.gameObject.TryGetComponent(out PlayerEntity player))
            {
                UIController.OpenWindow(GunSelector.gameObject);
                GunSelector.SetDisplay(this);
            }
        }
    }
}