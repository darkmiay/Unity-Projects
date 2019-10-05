using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {

    //stat list 
    [SerializeField]
    private Stat[] stats;
    [SerializeField]
    ParticleSystem particle;
    [Header("Unity Staff")]
    public Image healthBar;
    private void Awake()
    {
        stats = new Stat[Stat.statTypeCount];
        for (int i = 0; i < Stat.statTypeCount; i++)
        {
            stats[i] = new Stat(i);
        }
        GetStat(StatType.CurrentHealth).SetBaseValue(100);
        GetStat(StatType.MaxHealth).SetBaseValue(100);
        GetStat(StatType.Damage).SetBaseValue(10);
    }

    public void TakeDamage (int damage)
    {
        Debug.Log(transform.name + " takes" + damage + " damage.");

        if (GetStat(StatType.CurrentHealth).GetValue() <= 0)
        {
            Die();
        }
        if (healthBar!= null)
        {
            healthBar.fillAmount = (float)GetStat(StatType.CurrentHealth).GetValue() / (float)GetStat(StatType.MaxHealth).GetValue();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " die.");
    }

    //Находим нужный стат на персонаже по номеру типа стата
    public Stat GetStat(int n)
    {
        foreach (Stat s in stats)
            if (((int)s.GetStatType() == n))
                return s;

        return null;
    }

    //Находим нужный стат на персонаже по типу стата
    public Stat GetStat(StatType st)
    {
        foreach (Stat s in stats)
            if (s.GetStatType() == st)
                return s;
        return null;
    }

    public void ChangeValue(StatType stat,int value)
    {
        GetStat(stat).SetBaseValue(GetStat(stat).GetValue() + value);
        if (StatType.CurrentHealth == stat) TakeDamage(value);

    }

}
