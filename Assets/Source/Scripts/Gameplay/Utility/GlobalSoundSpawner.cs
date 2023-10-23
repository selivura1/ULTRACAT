using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundType
{
    Effect,
    Music,
    Enemy,
}

public class GlobalSoundSpawner : MonoBehaviour
{
    [SerializeField] AudioClip spawned;
    [SerializeField] AudioSource[] _sources;
    private Options options;
    
    void GetReferences()
    {
        options = GameManager.Options;
    }
    public void PlaySound(AudioClip sound, SoundType type, float pitchMin = 0.9f, float pitchMax = 1.1f)
    {
        if (options == null)
            GetReferences();
        float cfg = 0;
        switch (type)
        {
            case SoundType.Effect:
                cfg = options.CurrentConfig.SFX;
                break;
            case SoundType.Music:
                cfg = options.CurrentConfig.Music;
                break;
            case SoundType.Enemy:
                cfg = options.CurrentConfig.EnemySFX;
                break;
            default:
                break;
        }
        _sources[(int)type].pitch = Random.Range(pitchMin, pitchMax);
        _sources[(int)type].PlayOneShot(sound, cfg);
    }
}
