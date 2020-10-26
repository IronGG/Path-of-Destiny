using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot; // Slot to store equipment in
    public SkinnedMeshRenderer mesh; // Slot to put the mesh of the item
    public EquipmentMeshRegion[] coveredMeshRegions;
    
    public int armorModifier; // Increase/decrease in armor
    public int damageModifier; // Increase/decrease in damage

    public override void Use()
    {
        base.Use();
        // Equip this item
        EquipmentManager.instance.Equip(this);
        
        
        // Remove it from inventory
        RemoveFromInventory();
    }
    
}

public enum EquipmentSlot{ Head, Chest, Legs, Weapon, Shield, Feet}
public enum EquipmentMeshRegion {Legs, Arms, Torso }; // corresponds to body blendshapes