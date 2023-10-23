using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHandler : MonoBehaviour
{
    public List<PotionEffect> PotionEffects;
    private EntityBase _entity;
    private void Awake()
    {
        _entity = GetComponent<EntityBase>();
    }
    public void FixedUpdate()
    {
       for (int i = 0; i < PotionEffects.Count; i++)
       {
            PotionEffects[i].timeLeft -= Time.fixedDeltaTime;
            PotionEffects[i].Object.OnUpdate(_entity, PotionEffects[i]);
            if(PotionEffects[i].timeLeft <= 0)
            {
                PotionEffects[i].Object.OnEnd(_entity, PotionEffects[i]);
                PotionEffects.Remove(PotionEffects[i]);
            }
        }
    }
    public void AddEffect(PotionEffect effect)
    {
        for (int i = 0; i < PotionEffects.Count; i++)
        {
            if(PotionEffects[i] == effect)
            {
                PotionEffects[i].timeLeft = effect.timeLeft;
                return;
            }
        }
        var toAdd = new PotionEffect(effect.Object, effect.timeLeft, effect.sprite, effect.name, effect.desc);
        PotionEffects.Add(toAdd);
        toAdd.Object.OnApplied(_entity, toAdd);
        toAdd.Object.onAbort += EndPotion;
    }
    public void EndPotion(PotionEffectBase effect)
    {
        effect.onAbort -= EndPotion;
        for (int i = 0; i < PotionEffects.Count; i++)
        {
            if(PotionEffects[i].Object.name == effect.name)
            {
                PotionEffects[i].Object.OnEnd(_entity, PotionEffects[i]);
                PotionEffects.Remove(PotionEffects[i]);
            }
        }
    }
}
[System.Serializable]
public class PotionEffect
{
    public string name = "Effect";
    public string desc = "Description";
    public Sprite sprite;
    public PotionEffectBase Object;
    public float timeLeft;
    public bool endless = false;
    public PotionEffect
        (PotionEffectBase effect,
        float time,
        Sprite effectSprite,
        string effectName = "Effect",
        string effectDesc = "Description",
        bool endless = false)
    {
        Object = effect;
        timeLeft = time;
        sprite = effectSprite;
        name = effectName;
        desc = effectDesc;
        this.endless = endless;
    }
}

