using UnityEngine;
namespace Ultracat
{
    public class TargetableProjectile : Projectile
    {
        public EntityBase Target;
        public override void MovementPerFixedUpdate()
        {
            if (Target)
                rigidbody.velocity = (Target.transform.position - transform.position).normalized * CurrentSpeed * Time.fixedDeltaTime;
        }
    }
}