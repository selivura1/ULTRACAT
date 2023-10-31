using UnityEngine;

namespace Ultracat
{
    public class Combat : MonoBehaviour
    {
        public System.Action<Vector2> onAttack;
        public void StartAttack(Vector2 direction, WeaponBase weapon)
        {
            if (!weapon.CheckIfCanAttack()) return;
            transform.localScale = SetScaleByDirection(direction);
            weapon.StartAttack(direction);
            onAttack?.Invoke(direction);
        }
        public void StartCharging(Vector2 direction, WeaponBase weapon)
        {
            if (!weapon.CheckIfCanAttack()) return;
            weapon.StartCharging(direction);
        }
        public void StopCharging(Vector2 direction, WeaponBase weapon)
        {
            transform.localScale = SetScaleByDirection(direction);
            weapon.StopCharging(direction);
            onAttack?.Invoke(direction);
        }

        public static Vector3 SetScaleByDirection(Vector2 direction)
        {
            if (direction.x < 0)
            {
                return new Vector3(-1, 1, 1);
            }
            else
            {
                return new Vector3(1, 1, 1);
            }
        }

    }
}