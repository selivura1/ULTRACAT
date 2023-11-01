using UnityEngine;
namespace Ultracat
{
    public class BoomerangProjectile : Projectile
    {
        [SerializeField] float _returnAcceleration = 10;
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (!other.TryGetComponent(out IDamageable damageable))
                timer = 0;
        }
        public override void MovementPerFixedUpdate()
        {
            if (timer <= 0)
            {
                CurrentSpeed += Time.fixedDeltaTime * _returnAcceleration;
                transform.right = (caster.transform.position - transform.position).normalized;
                if (Vector3.Distance(caster.transform.position, transform.position) < .4f)
                    Terminate();
            }
            rigidbody.velocity = transform.right * CurrentSpeed;
        }
        public override void CountdownTimer()
        {
            timer -= Time.fixedDeltaTime;
        }
    }
}