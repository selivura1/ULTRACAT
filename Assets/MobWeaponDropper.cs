using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ultracat
{
    [RequireComponent(typeof(EntityBase))]
    public class MobWeaponDropper : MonoBehaviour
    {
        EntityBase _entity;
        [SerializeField] private WeaponBox _drop;
        private void Awake()
        {
            _entity = GetComponent<EntityBase>();
            _entity.onDeath += DropWeaponBox;
        }

        private void DropWeaponBox(EntityBase entity)
        {
            var spawned = GameManager.WeaponBoxPool.Get(_drop);
            spawned.Initialize();
        }
    }
}
