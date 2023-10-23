using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class EntityStats
{
    public CharacterStat Attack;
    public CharacterStat Health;
    public CharacterStat Speed;
    public CharacterStat BonusChance;
    public CharacterStat Omnivamp;
    public CharacterStat AttackSpeed;
    public CharacterStat AbilityHaste;
}
public enum StatModType
{
    Flat,
    PercentAdd,
    PercentMult,
}
[System.Serializable]
public class CharacterStat
{
    public float BaseValue;
    private bool isDirty = true;
    private float _value; 
    private float lastBaseValue = float.MinValue;
    public float Value
    {
        get
        {
            if (isDirty || lastBaseValue != BaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }
    private readonly List<StatModifier> statModifiers;
    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
    }
    public CharacterStat(float baseValue)
    {
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();
    }
    public void AddModifier(StatModifier mod)
    {
        if (mod.Value == 0)
            return;
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }
    public bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }
    public void RemoveAllModifiers()
    {
        isDirty = true;
        statModifiers.Clear();
    }
    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;
    }
    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier
            {
                sumPercentAdd += mod.Value; // Start adding together all modifiers of this type

                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            }
            else if (mod.Type == StatModType.PercentMult) // Percent renamed to PercentMult
            {
                finalValue *= 1 + mod.Value;
            }
        }

        return finalValue;
    }
}
[System.Serializable]
public class StatModifier
{
    public float Value;
    public StatModType Type;
    public int Order;
    public StatModifier(float value, StatModType type, int order, object source)
    {
        Value = value;
        Type = type;
        Order = order;
    }
    public new string ToString()
    {
        if(Value > 0)
        {
            switch (Type)
            {
                case StatModType.Flat:
                    return Value.ToString();
                case StatModType.PercentAdd:
                    return Value + "%";
                case StatModType.PercentMult:
                    return "x" + Value + "%";
                default:
                    break;
            }
        }
        return "";
    }
    public static string ToDescription(StatModifier stat, string name)
    {
        if (stat.Value > 0)
        {
            return $"+{stat.ToString()} {name}\n";
        }
        return "";
    }
}
