using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] Slider _sfx, _music, _enemySFX;
    [SerializeField] Toggle _fullscreen;
    [SerializeField] Toggle _barAbovePlayer;
    [SerializeField] TMP_Dropdown _resolutionDropDown;
    Options options;
    Resolution[] _resolutions;
    private void Start()
    {
        LoadValues();
    }
    public void LoadValues()
    {
        options = FindAnyObjectByType<Options>();
        _resolutions = Screen.resolutions;
        _resolutionDropDown.ClearOptions();
        List<string> listOptions = new List<string>();
        int index = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            listOptions.Add(_resolutions[i].width + "x" + _resolutions[i].height + "@" + _resolutions[i].refreshRateRatio);

            if (_resolutions[i].width == Screen.currentResolution.width
                && _resolutions[i].height == Screen.currentResolution.height
                && _resolutions[i].refreshRateRatio.value == Screen.currentResolution.refreshRateRatio.value)
            {
                index = i;
            }
        }
        _resolutionDropDown.AddOptions(listOptions);
        _resolutionDropDown.value = index;
        _resolutionDropDown.RefreshShownValue();
        _sfx.value = options.CurrentConfig.SFX;
        _music.value = options.CurrentConfig.Music;
        _enemySFX.value = options.CurrentConfig.EnemySFX;
        _fullscreen.isOn = options.CurrentConfig.Fullscreen;
        _barAbovePlayer.isOn = options.CurrentConfig.enableBarAbovePlayer;
    }
    public void DeleteOptions()
    {
        options.DeleteOptions();
    }

    public void Saveoptions()
    {
        options.SetSFX(_sfx.value);
        options.SetMusic(_music.value);
        options.SetEnemySFX(_enemySFX.value);
        options.SetFullscreen(_fullscreen.isOn);
        options.SetResolution(_resolutions[_resolutionDropDown.value]);
        //options.SetBarAbovePlayer(_barAbovePlayer.isOn);
        options.SaveOptions();
    }
}
