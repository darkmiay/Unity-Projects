using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour {

    public Ability[] abilities;

    public void UseAbility(Ability ability)
    {
        CharacterStats stats = null;
        switch (ability.target)
        {
            
            case AbilityTarget.Self:
               stats = Self(ability);
                break;
            case AbilityTarget.Point:
                break;
            case AbilityTarget.Target:
                stats = Target(ability);
                break;
            default:
                break;
        }
        if (stats!=null)
        ability.effect.ApplyEffect(stats);
    }

    public CharacterStats Target(Ability ability)
    {
        return this.GetComponent<PlayerController>().focus.GetComponent<CharacterStats>();
    }

    public CharacterStats Self(Ability ability)
    {
        return this.GetComponent<CharacterStats>();
    }
}
