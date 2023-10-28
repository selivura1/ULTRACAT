using UnityEngine;
namespace Ultracat
{
    public class ChannelingBarUI : MonoBehaviour
    {
        Inventory _playerInventory;
        [SerializeField] ProgressBar _bar;

        void Start()
        {
            _playerInventory = GameManager.PlayerSpawner.GetPlayer().GetComponent<Inventory>();
            Sub();
            _playerInventory.onWeaponChanged += Sub;
            HideChannelBar();
        }

        private void Sub()
        {
            _playerInventory.Weapon.onStartCharging += ShowChannelBar;
            _playerInventory.Weapon.onStopCharging += HideChannelBar;
        }

        private void Update()
        {
            if (_playerInventory.Weapon.WeaponState == WeaponState.Charging)
            {
                var weapon = _playerInventory.Weapon;
                _bar.Max = weapon.WeaponSettings.MaxChargeTime;
                _bar.CurrentValue = weapon.ChargeTime;
                _bar.Min = 0;
            }
        }

        private void OnDestroy()
        {
            _playerInventory.Weapon.onStartCharging -= ShowChannelBar;
            _playerInventory.Weapon.onStopCharging -= HideChannelBar;
        }
        void ShowChannelBar()
        {
            _bar.gameObject.SetActive(true);
        }
        void HideChannelBar()
        {
            _bar.gameObject.SetActive(false);
        }
    }
}