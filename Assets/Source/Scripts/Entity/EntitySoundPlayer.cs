using UnityEngine;
public class EntitySoundPlayer : MonoBehaviour
{
    [SerializeField] protected AudioClip[] killSounds;
    [SerializeField] protected AudioClip[] damagedSounds;
    [SerializeField] protected AudioClip[] healedSounds;
    protected Options options;
    protected GlobalSoundSpawner globalSoundSpawner;
    protected EntityBase entity;
    protected virtual void Start()
    {
        globalSoundSpawner = FindAnyObjectByType<GlobalSoundSpawner>();
        options = FindAnyObjectByType<Options>();
        entity = GetComponent<EntityBase>();
        entity.onDeath += PlayRandomKillSound;
        entity.onDamage += PlayRandomDamagedSound;
        //entity.onHeal += PlayRandomHealedSound;
    }
    protected virtual void PlayRandomKillSound(EntityBase arg)
    {
        if(killSounds.Length > 0)
            globalSoundSpawner.PlaySound(killSounds[Random.Range(0, killSounds.Length)], SoundType.Enemy);
    }
    protected virtual void PlayRandomDamagedSound(DamageBreakDown arg)
    {
        if (damagedSounds.Length > 0)
            globalSoundSpawner.PlaySound(damagedSounds[Random.Range(0, damagedSounds.Length)], SoundType.Enemy);
    }
    protected virtual void PlayRandomHealedSound(float arg)
    {
        if (healedSounds.Length > 0)
            globalSoundSpawner.PlaySound(healedSounds[Random.Range(0, healedSounds.Length)], SoundType.Enemy);
    }
    protected virtual void OnDestroy()
    {
        entity.onDamage -= PlayRandomDamagedSound;
        //entity.onHeal -= PlayRandomHealedSound;
    }
}

