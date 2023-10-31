using System.Collections;
using UnityEngine;
namespace Ultracat
{
    public class FastKiller : Item
    {
        [SerializeField] StatModifier _temporarySpeedBuff;
        [SerializeField] float _buffTime = 3;
        [SerializeField] EffectHandler _effectOnSpeedUp;
        protected override void OnStart()
        {
            entity.onKill += () => StartCoroutine(SpeedBuff());
        }
        protected override void OnDespawn()
        {
            entity.onKill -= () => StartCoroutine(SpeedBuff());
        }
        IEnumerator SpeedBuff()
        {
            var spawned = GameManager.EffectsPool.Get(_effectOnSpeedUp);
            spawned.transform.SetParent(transform);
            spawned.transform.localPosition = Vector3.zero;
            entity.EntityStats.Speed.AddModifier(_temporarySpeedBuff);
            yield return new WaitForSeconds(_buffTime);
            entity.EntityStats.Speed.RemoveModifier(_temporarySpeedBuff);
        }
    }
}