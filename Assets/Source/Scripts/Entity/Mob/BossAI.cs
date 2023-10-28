using UnityEngine;
namespace Ultracat
{
    public class BossAI : AIControl
    {
        [SerializeField] WeaponBase[] _weapons;
        public override void ExecuteAttack()
        {
            _weapon = _weapons[Random.Range(0, _weapons.Length)];
            _inventory.SetWeapon(_weapon);
            base.ExecuteAttack();
        }
    }
}