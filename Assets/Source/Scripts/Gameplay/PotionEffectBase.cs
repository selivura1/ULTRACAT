using System.Collections;
using UnityEngine;
[CreateAssetMenu(menuName = "Blank effect")]

public class PotionEffectBase : ScriptableObject
{
    public delegate void OnPreEnd(PotionEffectBase effect);
    public event OnPreEnd onAbort;
    public virtual void OnUpdate(EntityBase ent, PotionEffect effect)
    {

    }
    public virtual void OnApplied(EntityBase ent, PotionEffect effect)
    {

    }
    public virtual void OnEnd(EntityBase ent, PotionEffect effect)
    {

    }
    public void Abort()
    {
        onAbort?.Invoke(this);
    }
}