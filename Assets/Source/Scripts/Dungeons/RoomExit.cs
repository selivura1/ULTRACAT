using UnityEngine;

//Script for room finish trigger, sends call to DungeonGenerator to proceed to next room/level
public class RoomExit : MonoBehaviour
{
    bool _active = false;
    [SerializeField] Animator _anim;
    [SerializeField] Collider2D _collider;
    [SerializeField] string _opened, _closed;
    [SerializeField] AudioClip _openSFX, _closeSFX;
    public bool mute;
    public System.Action onTriggered;
    public void Activate(bool playSound = true)
    {
        if (playSound && !mute)
            GameManager.SoundSpawner.PlaySound(_openSFX, SoundType.Music, 1, 1);
        _anim.Play(_opened);
        _active = true;
        _collider.isTrigger =true;
    }
    public void Deactivate()
    {
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
            onTriggered?.Invoke();
            Deactivate();
        }
    }
}
