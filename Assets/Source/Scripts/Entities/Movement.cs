using System;
using System.Collections;
using UnityEngine;
namespace Ultracat
{
    public class Movement : MonoBehaviour
    {
        Rigidbody2D _rb;
        EntityBase entity;
        Animator _anim;
        public bool CanMove { get; private set; } = true;
        private void Awake()
        {
            entity = GetComponent<EntityBase>();
            _anim = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }
        public void Move(Vector2 dir)
        {
            if (dir.magnitude > 0)
                transform.localScale = Combat.SetScaleByDirection(dir);
            if (CanMove)
                _rb.velocity = dir.normalized * entity.EntityStats.Speed.Value * Time.deltaTime;
            if (_anim)
                _anim.SetFloat("Speed", dir.magnitude);
        }
        public void StunForTime(float time)
        {
            StartCoroutine(StunRoutine(time));
        }
        public void Stun(bool value)
        {
            CanMove = !value;
            _rb.velocity = Vector2.zero;
        }
        protected IEnumerator StunRoutine(float time)
        {
            Stun(true);
            yield return new WaitForSeconds(time);
            Stun(false);
        }
        public void Teleport(Vector2 pos)
        {
            transform.position = pos;
        }

        internal void StunForTime(object chargeAttackTime)
        {
            throw new NotImplementedException();
        }
    }
}