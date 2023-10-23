using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : EntitySoundPlayer
{
    [SerializeField] AudioClip[] levelUpSounds;
    [SerializeField] AudioClip itemRechargedSound;
    private PlayerLevels levels;
    private Inventory _inventory;
    protected override void Start()
    {
        _inventory = GetComponent<Inventory>();
        levels = GetComponent<PlayerLevels>();
        base.Start();
        levels.onLevelUp += OnLevelUp;
        _inventory.onActiveChanged += SubToActiveItem;
        SubToActiveItem();
    }
    void SubToActiveItem()
    {
        _inventory.ActiveItem.onActiveCharged += OnItemCharged;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        levels.onLevelUp -= OnLevelUp;
        _inventory.ActiveItem.onActiveCharged -= OnItemCharged;
    }
    protected override void PlayRandomKillSound(EntityBase arg)
    {
        if(killSounds.Length > 0)
            globalSoundSpawner.PlaySound(killSounds[Random.Range(0, killSounds.Length)], SoundType.Effect, 1 ,1);
    }
    protected void OnLevelUp()
    {
        if (levelUpSounds.Length > 0)
            globalSoundSpawner.PlaySound(levelUpSounds[Random.Range(0, levelUpSounds.Length)], SoundType.Effect, 1, 1);
    }
    protected void OnItemCharged()
    {
        globalSoundSpawner.PlaySound(itemRechargedSound, SoundType.Effect, 1,1);
    }
}
