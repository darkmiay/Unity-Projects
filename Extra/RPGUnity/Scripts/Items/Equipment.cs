using UnityEngine;
[CreateAssetMenu(fileName = "new Equipment",menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

public enum EquipmentSlot { Helmet, Chest, Pants, Weapon, SecondaryWeapon, Boots, Belt, Shoulders, Bracers, LeftRing, RightRing, Neck, Additional}
public enum EquipmentMeshRegion { Legs,  Torso, Hands, Arms, Foots, Head, Neck }