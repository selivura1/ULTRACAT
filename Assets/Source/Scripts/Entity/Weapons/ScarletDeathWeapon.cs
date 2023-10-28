using UnityEngine;
namespace Ultracat
{
    public class ScarletDeathWeapon : KillstackingWeapon
    {
        [SerializeField] private float _reloadSpeedPerStack = 1;
        protected override void OnStackUpdated()
        {
            reloadSpeed = WeaponSettings.BaseReloadSpeed + stacks * _reloadSpeedPerStack;
        }
    }
}