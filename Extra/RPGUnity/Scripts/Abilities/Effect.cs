using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Abilities/Effect")]
public class Effect : ScriptableObject {

    [SerializeField]
    public ParticleSystem particleSystem;
    [SerializeField]
    StatType stat; //What stat will be affected
    [SerializeField]
    int value; //Damage value, or maybe decrease percent, heal and so on
    [SerializeField]
    bool effectReturn; // return value back after end of the duration
    [SerializeField]
    int duration; // to know when to return value

    // ApplyEffect method
    public void ApplyEffect(CharacterStats cs)
    {
        if (!effectReturn)
        cs.ChangeValue(stat, value);
        ApplyParticle(cs);
        return;
    }

    public void ApplyParticle(CharacterStats cs)
    {
        ParticleSystem ps = Instantiate(particleSystem, Vector3.zero, Quaternion.Euler(0,0,0));
        ps.transform.position = cs.transform.position;
        ps.Play();
        Destroy(ps.gameObject, ps.main.duration);
    }
}
