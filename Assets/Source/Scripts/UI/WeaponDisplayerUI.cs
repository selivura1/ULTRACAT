using UnityEngine;
using UnityEngine.UI;
namespace Ultracat
{
    public class WeaponDisplayerUI : MonoBehaviour
    {
        [SerializeField] Image _weapon;
        [SerializeField] TooltipTrigger _weaponTrigger;
        Inventory inventory;
        EntityBase _player;
        private void Start()
        {
            _player = FindAnyObjectByType<PlayerEntity>();
            inventory = _player.GetComponent<Inventory>();
            inventory.onItemsReferesh += Refresh;
            Refresh();
        }
        public void Refresh()
        {
            _weapon.sprite = inventory.Weapon.WeaponSettings.Icon;
            _weaponTrigger.header = inventory.Weapon.WeaponSettings.Name;
            _weaponTrigger.content = inventory.Weapon.WeaponSettings.Desc;
        }
        private void OnDestroy()
        {
            inventory.onItemsReferesh -= Refresh;
        }
    }
}