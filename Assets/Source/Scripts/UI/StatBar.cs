using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EffectTypes
{
    Channel,
    Warning,
    Danger,
    Confusion,
    Stun,
    Charm,
    Freezed,
    Heal,
}
public class StatBar : MonoBehaviour
{
    AIControl _ownerAI;
    EntityBase owner;
    [SerializeField] ProgressBar _bar;
    [SerializeField] Transform _grid;
    [SerializeField] Sprite[] _icons;
    [SerializeField] Vector2 _offset;
    public void SetOwner(EntityBase newOwner)
    {
        if (_ownerAI != null)
            UnsubFromAIEvents();
        owner = newOwner;
        var newAi = owner.GetComponent<AIControl>();
        if (newAi != null)
            _ownerAI = newAi;
        SubToAIEvents();
    }
    public void SubToAIEvents()
    {
        if(_ownerAI)
        _ownerAI.onAttackPrepare += AIWarningEffectSpawn;
    }
    public void UnsubFromAIEvents()
    {
        if (_ownerAI)
            _ownerAI.onAttackPrepare -= AIWarningEffectSpawn;
    }

    public void Terminate()
    {
        Destroy(gameObject);
    }

    private void AIWarningEffectSpawn(float prepareTime)
    {
        AddEffect(EffectTypes.Warning, prepareTime);
    }
    private void Update()
    {
        if (!owner) return;
        _bar.Max = owner.EntityStats.Health.Value;
        _bar.CurrentValue = owner.GetHealth();
        transform.position = Camera.main.WorldToScreenPoint(owner.transform.position + (Vector3)_offset);
    }
    public void AddEffect(EffectTypes type, float time)
    {
        var spawned = new GameObject();
        spawned.transform.SetParent(_grid);
        spawned.transform.localScale = Vector3.one;
        spawned.AddComponent<Image>().sprite = _icons[(int)type];
        Destroy(spawned, time);
    }
}
