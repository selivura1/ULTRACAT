using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ultracat
{
    public class CoolGlasses : Item
    {
        private const string SpeedParameterName = "Speed";
        Animator _animator;
        Rigidbody2D _userRb;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _userRb = GetComponentInParent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            _animator.SetFloat(SpeedParameterName, _userRb.velocity.magnitude);
        }
    }
}
