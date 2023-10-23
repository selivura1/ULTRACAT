using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public new string name;
    [TextArea]
    [SerializeField] string Description = "Item desc.";

    public StatModifier Attack;
    public StatModifier Health;
    public StatModifier Speed;
    public StatModifier CollectibleChance;
    public StatModifier Omnivamp;
    public StatModifier AttackSpeed;
    public StatModifier AbilityHaste;

    public Sprite Picture;
    protected EntityBase entity { get; private set; }
    protected void Awake()
    {
        entity = GetComponentInParent<EntityBase>();
    }
    private void Start()
    {
        GameManager.PopUpSpawner.SpawnPopUp(entity.transform.position, "+" + name, Color.yellow);

        entity.EntityStats.Attack.AddModifier(Attack);
        entity.EntityStats.Health.AddModifier(Health);
        entity.EntityStats.Speed.AddModifier(Speed);
        entity.EntityStats.BonusChance.AddModifier(CollectibleChance);
        entity.EntityStats.Omnivamp.AddModifier(Omnivamp);
        entity.EntityStats.AttackSpeed.AddModifier(AttackSpeed);
        entity.EntityStats.AbilityHaste.AddModifier(AbilityHaste);
        OnStart();
    }
    protected virtual void OnStart()
    {

    }
    protected void Update()
    {

    }
    private void OnDestroy()
    {
        entity.EntityStats.Attack.RemoveModifier(Attack);
        entity.EntityStats.Health.RemoveModifier(Health);
        entity.EntityStats.Speed.RemoveModifier(Speed);
        entity.EntityStats.BonusChance.RemoveModifier(CollectibleChance);
        entity.EntityStats.Omnivamp.RemoveModifier(Omnivamp);
        entity.EntityStats.AttackSpeed.RemoveModifier(AttackSpeed);
        entity.EntityStats.AbilityHaste.RemoveModifier(AbilityHaste);
        OnDespawn();
    }
    protected virtual void OnDespawn()
    {

    }
    public string GetDescription()
    {
        string output = $"";
        //output += StatModifier.ToDescription(Attack, "Power");
        //output += StatModifier.ToDescription(Health, "Health");
        //output += StatModifier.ToDescription(Speed, "Move speed");
        //output += StatModifier.ToDescription(CollectibleChance, "Reward chance");
        output += Description;
        return output;
    }
}
