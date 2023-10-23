using System;
using UnityEngine;

public class Options : MonoBehaviour
{
    public OptionsConfig CurrentConfig { get; private set; } = new OptionsConfig();
    [SerializeField] private string _optionsKey = "options";
    public Action onOptionsUpdate;
    private void Start()
    {
        LoadOptions();
    }
    public void SetOptions(OptionsConfig config)
    {
        CurrentConfig = config;
    }
    public void LoadOptions()
    {
        if (!PlayerPrefs.HasKey(_optionsKey))
        {
            Debug.Log("NO OPTIONS SAVED >>> CREATING NEW");
            CurrentConfig.Resolution = Screen.currentResolution;
            SaveOptions();
        }
        else
            SetOptions(JsonUtility.FromJson<OptionsConfig>(PlayerPrefs.GetString(_optionsKey)));
        Screen.fullScreen = CurrentConfig.Fullscreen;
        Debug.Log("Options Loaded: | SFX: " + CurrentConfig.SFX + " | MUSIC: " + CurrentConfig.Music + " | FULLSCREEN: " + CurrentConfig.Fullscreen);
        onOptionsUpdate?.Invoke();
    }
    public void SaveOptions()
    {
        PlayerPrefs.SetString(_optionsKey, JsonUtility.ToJson(CurrentConfig));
        Debug.Log("Options saved: | SFX: " + CurrentConfig.SFX + " | MUSIC: " + CurrentConfig.Music + " | FULLSCREEN: " + CurrentConfig.Fullscreen);
        onOptionsUpdate?.Invoke();
    }
    public void SetSFX(float value)
    {
        CurrentConfig.SFX = value;
    }
    public void SetMusic(float value)
    {
        CurrentConfig.Music = value;
    }
    public void SetEnemySFX(float value)
    {
        CurrentConfig.EnemySFX = value;
    }
    public void SetFullscreen(bool val)
    {
        CurrentConfig.Fullscreen = val;
        Screen.SetResolution(CurrentConfig.Resolution.width, CurrentConfig.Resolution.height, CurrentConfig.Fullscreen);
    }
    public void SetBarAbovePlayer(bool val)
    {
        CurrentConfig.enableBarAbovePlayer = val;
    }
    public void SetResolution(Resolution res)
    {
        CurrentConfig.Resolution = res;
        Screen.SetResolution(res.width, res.height, CurrentConfig.Fullscreen);
    }
    public void DeleteOptions()
    {
        CurrentConfig = new OptionsConfig();
        CurrentConfig.Resolution = Screen.currentResolution;
        SaveOptions();
    }
}
[System.Serializable]
public class OptionsConfig
{
    public float SFX = .4f;
    public float Music = .2f;
    public bool Fullscreen = true;
    public bool Vsync = false;
    public bool enableBarAbovePlayer = false;
    public Resolution Resolution;
    public float EnemySFX = .2f;
}

