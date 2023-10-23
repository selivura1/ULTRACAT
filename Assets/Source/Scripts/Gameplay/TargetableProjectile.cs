using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetableProjectile : Projectile
{
    public EntityBase Target;
    public override void MovementPerFixedUpdate()
    {
        if(Target)
            rigidbody.velocity = (Target.transform.position - transform.position).normalized * CurrentSpeed * Time.fixedDeltaTime;
    }
}