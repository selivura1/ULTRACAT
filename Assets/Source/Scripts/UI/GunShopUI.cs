using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Ultracat
{
    public class GunShopUI : MonoBehaviour
    {
        [SerializeField] Button _rightArrow, _leftArrow;
        [SerializeField] Button _equipButton;
        [SerializeField] TMP_Text _weaponName;
        [SerializeField] TMP_Text _weaponDesc;
        [SerializeField] Image _weaponImage;
        WeaponBox currentDealer;
        private int index;
        private void Start()
        {
            _rightArrow.onClick.AddListener(Right);
            _leftArrow.onClick.AddListener(Left);
            _equipButton.onClick.AddListener(Equip);
        }
        public void SetDisplay(WeaponBox dealer)
        {
            currentDealer = dealer;
            Refresh();
        }
        public void Equip()
        {
            currentDealer.Equip(index);
            Refresh();
        }
        public void Right()
        {
            if (index < currentDealer.Weapons.Length - 1)
                index++;
            else index = 0;
            Refresh();
        }
        public void Left()
        {
            if (index > 0)
                index--;
            else index = currentDealer.Weapons.Length - 1;
            Refresh();
        }
        public void Refresh()
        {
            var weapons = currentDealer.Weapons;
            //Debug.Log("Current weapon: " + weapons[index].name);
            _weaponImage.sprite = weapons[index].WeaponSettings.Icon;
            _weaponName.text = weapons[index].WeaponSettings.Name;
            _weaponDesc.text = weapons[index].WeaponSettings.Desc;

            //_leftArrow.interactable = index > 0 ? true : false;
            //_rightArrow.interactable = index < weapons.Length - 1 ? true : false;

            _equipButton.interactable = !currentDealer.IsAlreadyEquipped(index);
        }
    }
}