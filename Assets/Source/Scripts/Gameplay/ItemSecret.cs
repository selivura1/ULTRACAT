using UnityEngine;
namespace Ultracat
{
    [CreateAssetMenu]
    public class ItemSecret : Secret
    {
        [SerializeField] Item _itemToUnlock;
        public override void AddContent()
        {
            GameManager.Database.AddItem(_itemToUnlock);
        }
    }
}