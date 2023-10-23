using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for room finish trigger, sends call to DungeonGenerator to proceed to next room/level
public class RoomExit : MonoBehaviour
{
    bool _active = false;
    [SerializeField] Animator _anim;
    [SerializeField] Collider2D _collider;
    [SerializeField] string _opened, _closed;
    [SerializeField] bool _stageEnd;
    [SerializeField] AudioClip _openSFX, _closeSFX;
    DungeonGenerator _dungeonGenerator;
    [SerializeField] private float transitionDelay = 1;
    //UIManager HUDActivator;
    private FadeUI fade;
    public bool mute;
    private void GetReferences()
    {
        //HUDActivator = GameManager.UIManager;
        _dungeonGenerator = GameManager.DungeonGenerator;
        fade = FindAnyObjectByType<FadeUI>();
    }
    public void Activate(bool playSound = true)
    {
        GetReferences();
        if (playSound && !mute)
            GameManager.SoundSpawner.PlaySound(_openSFX, SoundType.Music, 1, 1);
        _anim.Play(_opened);
        _active = true;
        _collider.isTrigger =true;
    }
    public void Deactivate(bool playSound = true)
    {
        GetReferences();
        if (playSound && !mute)
            GameManager.SoundSpawner.PlaySound(_closeSFX, SoundType.Music, 1, 1);
        _active = false;
        _collider.isTrigger = false;
        _anim.Play(_closed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_active) return;
        var player = collision.GetComponent<PlayerEntity>();
        if (player)
        {
            if(_stageEnd)
            {
                _dungeonGenerator.DungeonComplete();
                Deactivate(false);
            }
            else
            {
                fade.Fade();
                Invoke(nameof(ToNextRoom), transitionDelay);     
                Deactivate(true);
            }
        }
    }
    private void ToNextRoom()
    {
        _dungeonGenerator.ProceedToNextRoom();
    }
}
