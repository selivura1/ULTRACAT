using System;
using UnityEngine;

public class Options : MonoBehaviour
{
    public OptionsConfig CurrentConfig { get; private set; } = new OptionsConfig();
    public Action onOptionsUpdated;
    private DataService _dataService = new DataService();
    private const string OptionsRelPath = "options.json";
    private void Start()
    {
        LoadOptions();
    }
    public void LoadOptions()
    {
        try
        {
            CurrentConfig = _dataService.LoadData<OptionsConfig>(OptionsRelPath);
        }
        catch
        {
            CurrentConfig = new OptionsConfig();
        }
        Screen.fullScreen = CurrentConfig.Fullscreen;
        onOptionsUpdated?.Invoke();
    }
    public void SaveOptions()
    {
        _dataService.SaveData(OptionsRelPath, CurrentConfig);
        onOptionsUpdated?.Invoke();
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

