using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region MyRegion

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion Singleton


    public Equipment[] defaultItems;
    
    public SkinnedMeshRenderer targetMesh;
    
    private Equipment[] currentEquipment; // Items we currently have equipped

    private SkinnedMeshRenderer[] currentMeshes;

    // Callback when an  item is equipped or unequipped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private Inventory inventory; // reference to our inventory

    private void Start()
    {
        inventory = Inventory.instance; // get the reference to our inventory

        // string array of all our slots in equipmentSlot
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        
        EquipDefaultItems();
    }

    // Equip a new item
    public void Equip(Equipment newItem)
    {
        // create an index to the item's slot, example : Head = 0, helmet of protection.equipslot = 0
        int slotIndex = (int) newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);

        // An item has been equipped so we trigger the callback
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        
        SetEquipmentBlendShapes(newItem, 100);
        
        // add the item to equip to it's place in our equipmentSlot, example : Helmet to Head, Sword to hand, ...
        currentEquipment[slotIndex] = newItem;
        // Instantiate the mesh into the game world
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        
        // Insert the mesh into our equipment
        currentMeshes[slotIndex] = newMesh;
    }
    
    public Equipment Unequip(int slotIndex)
    {
        // if the re is already an equipment in our equipment slot
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            // takes the current item of this equipment slot to a variable
            Equipment oldItem = currentEquipment[slotIndex];
            
            SetEquipmentBlendShapes(oldItem, 0);
            // Add the oldItem to the inventory
            inventory.Add(oldItem);
            // clear the actual equipment slot
            currentEquipment[slotIndex] = null;
            
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }

        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        
        EquipDefaultItems();
        
    }

    public void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
    
}
