using UnityEngine;
namespace Ultracat
{
    public class ActiveDisplayAnimation : MonoBehaviour
    {
        [SerializeField] Animator _graphics;
        [SerializeField] string _animKey = "Charged";
        private PlayerEntity _player;
        Inventory _playerInventory;
        private void Start()
        {
            _player = FindAnyObjectByType<PlayerEntity>();
            _playerInventory = _player.GetComponent<Inventory>();

            _playerInventory.ActiveItem.onActiveCharged += ChangeAnim;
            _player.onDeath += RemoveSub;
        }
        private void ChangeAnim()
        {
            _graphics.Play(_animKey);
        }
        private void RemoveSub(EntityBase abva)
        {
            _playerInventory.ActiveItem.onActiveCharged -= ChangeAnim;
        }
        private void OnDestroy()
        {
            if (_player)
                _playerInventory.ActiveItem.onActiveCharged -= ChangeAnim;
        }
    }
}