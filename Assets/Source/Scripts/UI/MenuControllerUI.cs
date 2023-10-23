using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerUI : MonoBehaviour
{
    public GunShopUI GunSelectWindow;
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _ggWindow;
    [SerializeField] GameObject _winWindow;
    [SerializeField] GameObject _versusWindow;
    [SerializeField] List<GameObject> _openedWindows;
    [SerializeField] List<GameObject> _tabs = new List<GameObject>();
    private void Start()
    {
        PlayerControl.cancelInput += EscapeButton;
        GameManager.PlayerSpawner.GetPlayer().onDeath += OnPlayerDeath;
    }
    private void OnDestroy()
    {
        PlayerControl.cancelInput -= EscapeButton;
    }
    private void OnPlayerDeath(EntityBase ent)
    {
        CloseAllWindows();
        OpenWindow(_ggWindow);
    }
    public void GameWin()
    {
        CloseAllWindows();
        OpenWindow(_winWindow);
    }
    public void EscapeButton()
    {
        if (_versusWindow.activeSelf) return;
        if (_ggWindow.activeSelf) return;
        if (_winWindow.activeSelf) return;
        if (_openedWindows.Count > 0)
            CloseWindow(_openedWindows[_openedWindows.Count - 1]);
        else
            OpenWindow(_menu);
    }
    public void OpenWindow(GameObject window)
    {
        if (_openedWindows.Contains(window))
            return;
        window.SetActive(true);
        _openedWindows.Add(window);
        PauseIfWindowOpened();
    }
    public void CloseWindow(GameObject window)
    {
        if (_openedWindows.Remove(window))
        {
            window.SetActive(false);
        }
        PauseIfWindowOpened();
    }
    private void CloseAllWindows()
    {
        foreach (var item in _openedWindows)
        {
            item.SetActive(false);
        }
        _openedWindows.Clear();
        PauseIfWindowOpened();
    }
    public void OpenTab(int index)
    {
        foreach (var item in _tabs)
        {
            item.SetActive(false);
        }
        _tabs[index].SetActive(true);
    }
    public void PauseIfWindowOpened()
    {
        if (_openedWindows.Count > 0)
            TimeControl.SetPause(true);
        else
            TimeControl.SetPause(false);
    }
}
