using UnityEngine;
namespace Ultracat
{
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
}