using System;
using System.Collections.Generic;
using UnityEngine;
namespace Ultracat
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] List<Item> _items = new List<Item>();
        [SerializeField] ActiveItem _startActiveItem;
        [SerializeField] WeaponBase _startWeapon;
        WeaponBase _weaponEquipped;
        ActiveItem _activeItemEquipped;
        EntityBase _entity;
        public WeaponBase Weapon => _weaponEquipped;
        public List<Item> Items => _items;
        public ActiveItem ActiveItem => _activeItemEquipped;

        public Action onWeaponChanged;
        public Action onItemsReferesh;
        public Action onActiveChanged;

        private void Start()
        {
            _entity = GetComponent<EntityBase>();
            if (_startActiveItem)
                SetActiveItem(_startActiveItem);
            if (_startWeapon)
                SetWeapon(_startWeapon);
        }
        public void Give(Item item)
        {
            var spawned = Instantiate(item, transform);
            _items.Add(spawned);
            Refresh();
        }
        public void SetActiveItem(ActiveItem item)
        {
            if (_activeItemEquipped)
                Destroy(_activeItemEquipped.gameObject);
            item = Instantiate(item, transform);
            _activeItemEquipped = item;
            Refresh();
            onActiveChanged?.Invoke();
        }
        public void SetWeapon(WeaponBase weapon)
        {
            if (_weaponEquipped)
                Destroy(_weaponEquipped.gameObject);
            _weaponEquipped = Instantiate(weapon, transform);
            Refresh();
            onWeaponChanged?.Invoke();
        }
        public void ActivateArtifact()
        {
            _activeItemEquipped.ExecuteActive();
        }
        public void Refresh()
        {
            onItemsReferesh?.Invoke();
            _entity.CheckIfOverHealed();
        }
    }
}
