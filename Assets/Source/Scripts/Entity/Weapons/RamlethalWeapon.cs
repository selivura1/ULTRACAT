using UnityEngine;

public class RamlethalWeapon : ProjectileWeapon
{
    [SerializeField] private BoomerangProjectile _holdAttackProjectile;
    [SerializeField] private string _hideKey = "RattleDisappear";
    [SerializeField] private string _showKey = "RattleReapear";
    [SerializeField] private Transform[] _rattleDropPositions;
    private int _blades = 2;
   
    protected override void OnStopCharging(Vector2 direction)
    {
        base.OnStopCharging(direction);
        _blades = 0;
        PlayAttackFX();
        if (animator)
            animator.Play(_hideKey);
        for (int i = 0; i <= 1; i++)
        {
            var spawned = SpawnProjectile(_rattleDropPositions[i].position, _holdAttackProjectile, Vector2.zero);
            spawned.Initialize(user, CalculateDirection(direction, spread), CalculateChargedDamage(ChargeTime));
            spawned.OnTerminated.AddListener(OnBladeReturn);
            spawned.OnHit.AddListener(OnHit);
            spawned.CurrentSpeed *= GetChargeFactor(ChargeTime);
        }
    }
    void OnBladeReturn(Projectile proj)
    {
        _blades++;
        if(_blades >= 2)
            animator.Play(_showKey);
        proj.OnTerminated.RemoveListener(OnBladeReturn);
    }
    public override bool CheckIfCanAttack()
    {
        return base.CheckIfCanAttack() && _blades == 2;
    }
}
