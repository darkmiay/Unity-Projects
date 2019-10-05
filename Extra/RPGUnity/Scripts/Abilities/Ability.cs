using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityTarget {Self, Point, Target};

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
public class Ability : ScriptableObject
{

    [SerializeField]
    public string name;
    [SerializeField]
    public string description;
    [SerializeField]
    public AbilityTarget target;
    [SerializeField]
    public Effect effect;

}

