using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	// Use this for initialization
	void Start () {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem!= null)
        {
            GetStat(StatType.Armor).AddModifier(newItem.armorModifier);
            GetStat(StatType.Damage).AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            GetStat(StatType.Armor).RemoveModifier(oldItem.armorModifier);
            GetStat(StatType.Damage).RemoveModifier(oldItem.damageModifier);
            Debug.Log("im here btw");
        }

    }

    public override void Die()
    {
        base.Die();
        // kill the player
        PlayerManager.instance.KillPlayer();
    }
}
