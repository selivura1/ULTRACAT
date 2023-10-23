using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnPoint : MonoBehaviour
{
    [SerializeField] private EntityBase mob;
    public EntityBase Mob
    {
        private set
        {

        }
        get
        {
            return mob;
        }
    }
}
