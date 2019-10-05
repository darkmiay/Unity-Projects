using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum StatType
{
    Strength, Dexterity, Intelligence,
    MaxHealth, CurrentHealth, Damage, Armor, HealthRegeneration, MagicResistance,
    AttackSpeed, Evasion, MovementSpeed, CriticalRate, CriticalDamage,
    CooldownDecrease, CastSpeed, SkillDamage, Mana, ManaRegeneraion,
    DropChance, DropQuality, GoldIncrease, ExpirienceIncrease,
    HealthDamage, ArmorIgnore, ResistanceIgnore
}

[System.Serializable]
public class Stat {

    [SerializeField]
    private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    public const int statTypeCount = 26;
    [SerializeField]
    private int baseValue;
    [SerializeField]
    private int baseMultiplier;
    [SerializeField]
    private bool percent;
    [SerializeField]
    private StatType type;

    [SerializeField]
    private List<int> modifiers = new List<int>();
    [SerializeField]
    private List<int> multipliers = new List<int>();

    public int GetValue ()
    {
        float finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);

        float finalMultiplier = baseMultiplier;
        multipliers.ForEach(x => finalMultiplier += x);

        finalValue = (finalValue * ((100 + finalMultiplier) / 100));
        return (int)finalValue;
    }

    public StatType GetStatType()
    {
        return type;
    }

    public Stat(int n)
    {
        type = (StatType)n;
        name = type.ToString();
    }

    public void AddModifier(int modifier)
    {
        if (modifier!=0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier)
    {

        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }

    public void AddMultiplier(int multiplier)
    {
        if (multiplier != 0)
        {
            multipliers.Add(multiplier);
        }
    }

    public void RemoveMultiplier(int multiplier)
    {
        if (multiplier != 0)
        {
            multipliers.Remove(multiplier);
        }
    }

    public void SetBaseValue(int value)
    {
        baseValue = value;
    }

    public void SetBaseMultiplier(int value)
    {
        baseMultiplier = value;
    }
}