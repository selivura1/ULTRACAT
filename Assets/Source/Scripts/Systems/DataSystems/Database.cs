using System;
using System.Collections.Generic;
using UnityEngine;
namespace Ultracat
{
    public class Database : MonoBehaviour
    {
        [SerializeField] List<Secret> _secrets = new List<Secret>();
        public List<Secret> Secrets { get { return _secrets; } }
        [SerializeField] List<Item> _items = new List<Item>();
        [SerializeField] List<Item> _usableItems = new List<Item>();
        public List<Item> UnlockedItems => _usableItems;

        [SerializeField] List<ActiveItem> _activeItems = new List<ActiveItem>();
        [SerializeField] List<ActiveItem> _usableActiveItems = new List<ActiveItem>();

        public List<ActiveItem> UnlockedActiveItems => _usableActiveItems;

        [SerializeField] List<WeaponBase> _weapons = new List<WeaponBase>();
        [SerializeField] List<WeaponBase> _usableWeapons = new List<WeaponBase>();
        public List<WeaponBase> UnlockedWeapons => _usableWeapons;

        public List<Bonus> Collectibles = new List<Bonus>();
        public List<EffectHandler> Effects = new List<EffectHandler>();
        public List<int> CurrentSecrets => save.SecretIDs;
        public int SecretProgress => CurrentSecrets.Count;
        public int SecretAmount => _secrets.Count;
        [SerializeField] SaveFile blankSave;
        [SerializeField] SaveFile save;
        public Action onSecretUnlock;
        private DataService _dataService = new DataService();
        private const string SaveRelPath = "save.json";
        private void Awake()
        {
            LoadSave();
        }
        private void LoadSave()
        {
            try
            {
                save = _dataService.LoadData<SaveFile>(SaveRelPath);
                for (int i = 0; i < save.SecretIDs.Count; i++)
                {
                    _secrets[save.SecretIDs[i]].AddContent();
                }
            }
            catch
            {
                save = blankSave;
            }
        }
        private void WriteSave()
        {
            _dataService.SaveData(SaveRelPath, save);
        }
        public void ResetSave()
        {
            Debug.Log("Save reset...");
            save = blankSave;
            WriteSave();
        }
        public bool UnlockSecret(int secret)
        {
            if (save.SecretIDs.Contains(secret))
                return false;
            GameManager.PopUpSpawner.SpawnPopUp(FindAnyObjectByType<PlayerEntity>().transform.position, "SECRET UNLOCKED!", Color.blue);
            save.SecretIDs.Add(secret);
            onSecretUnlock?.Invoke();
            WriteSave();
            return true;
        }
        public bool AddItem(Item item)
        {
            if (_usableItems.Contains(item)) return false;
            _usableItems.Add(item);
            return true;
        }
        public bool AddWeapon(WeaponBase weapon)
        {
            if (_usableWeapons.Contains(weapon)) return false;
            _usableWeapons.Add(weapon);
            return true;
        }
    }
    [Serializable]
    public class SaveFile
    {
        public List<int> SecretIDs = new List<int>();
    }


}