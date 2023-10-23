using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Database : MonoBehaviour
{
    [SerializeField] List<Secret> _secrets = new List<Secret>();
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
    public List<Secret> CurrentSecrets => save.UnlockedSecrets;
    public int SecretProgress => CurrentSecrets.Count;
    public int SecretAmount => _secrets.Count;
    [SerializeField] SaveFile blankSave;
    [SerializeField] SaveFile save;
    public System.Action onSecretUnlock;
    private void Awake()
    {       
        if (File.Exists(GetSavePath()))
        {
            LoadFile();
        }
        else
        {
            save = blankSave;
            SaveFile();
        }
    }
    public void ResetSave()
    {
        Debug.Log("Save reset...");
        save = blankSave;
        SaveFile();
    }
    public void LoadSave(SaveFile save)
    {
        Debug.Log("Loading save...");
        this.save = save;
        Debug.Log("Save loaded.");
    }
    public void SaveFile()
    {
        Debug.Log("Saving...");
        using StreamWriter writer = new StreamWriter(GetSavePath());
        writer.Write(JsonUtility.ToJson(save));
        Debug.Log("Saved.");
    }
    public void LoadFile()
    {
        Debug.Log("Loading file( "+ GetSavePath()+ " )");
        if (File.Exists(GetSavePath()))
        {
            using StreamReader reader = new StreamReader(GetSavePath());
            save = JsonUtility.FromJson<SaveFile>(reader.ReadToEnd());
        }
        else
            throw new Exception("Failed to load save! Maybe save file does not exists?");
        foreach (var item in save.UnlockedSecrets)
        {
            item.Unlock();
        }
        Debug.Log("File loaded.");
    }
    public bool UnlockSecret(Secret secret)
    {
        if (save.UnlockedSecrets.Contains(secret))
            return false;
        GameManager.PopUpSpawner.SpawnPopUp(GameManager.PlayerSpawner.GetPlayer().transform.position, "SECRET UNLOCKED!", Color.blue);
        save.UnlockedSecrets.Add(secret);
        onSecretUnlock?.Invoke();
        SaveFile();
        return true;
    }
    public bool UnlockItem(Item item)
    {
        if (_usableItems.Contains(item)) return false;
        _usableItems.Add(item);
            return true;
    }
    public bool UnlockWeapon(WeaponBase weapon)
    {
        if (_usableWeapons.Contains(weapon)) return false;
        _usableWeapons.Add(weapon);
        return true;
    }
    public string GetSavePath()
    {
        return Application.persistentDataPath + Path.AltDirectorySeparatorChar + "save.json";
    }
}
[System.Serializable]
public class SaveFile
{
    public List<Secret> UnlockedSecrets = new List<Secret>();
    public int voidTokens = 0;
    public int kills;
    public int restarts;
}

    

