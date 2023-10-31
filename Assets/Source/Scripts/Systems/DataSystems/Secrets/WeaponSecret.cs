using UnityEngine;
namespace Ultracat
{
    [CreateAssetMenu]
    public class WeaponSecret : Secret
    {
        [SerializeField] WeaponBase _weaponToUnlock;
        public override void AddContent()
        {
            GameManager.Database.AddWeapon(_weaponToUnlock);
        }
    }
}