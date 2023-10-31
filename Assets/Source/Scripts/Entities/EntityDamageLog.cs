using UnityEngine;
namespace Ultracat
{
    public class EntityDamageLog : MonoBehaviour
    {
        EntityBase entity;
        [SerializeField] Color _executeColor = Color.cyan;
        [SerializeField] Color _damageColor = Color.white;
        [SerializeField] Color _healColor = Color.green;
        [SerializeField] Color _absorbColor = Color.blue;
        [SerializeField] int _numbersAfterDot = 1;
        private void Start()
        {
            entity = GetComponent<EntityBase>();
            entity.onDamage += SpawnPop;
            entity.onDamageAbsorbed += SpawnAbsorbedPop;
            entity.onHeal += SpawnHealPop;
        }
        private void SpawnAbsorbedPop(DamageBreakDown damage)
        {
            GameManager.PopUpSpawner.SpawnPopUp(transform.position, "-" + damage.Damage.ToString("F" + _numbersAfterDot), _absorbColor);
        }
        private void SpawnPop(DamageBreakDown damage)
        {
            if (damage.Damage > 1)
            {
                if (damage.Type == DamageType.Execute)
                    GameManager.PopUpSpawner.SpawnPopUp(transform.position, "EXECUTED", _executeColor);
                else
                    GameManager.PopUpSpawner.SpawnPopUp(transform.position, damage.Damage.ToString("F" + _numbersAfterDot), _damageColor);
            }
        }

        private void SpawnHealPop(float amount)
        {
            if (amount > 1)
                GameManager.PopUpSpawner.SpawnPopUp(transform.position, amount.ToString("F" + _numbersAfterDot), _healColor);
        }
    }
}