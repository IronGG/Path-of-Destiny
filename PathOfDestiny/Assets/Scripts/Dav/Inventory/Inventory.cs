using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Inventory usable by all other codes
    #region Singleton

    // make the inventory usable by other scripts
    public static Inventory instance;

    private void Awake()
    {
        // in the case that there is a problem with the inventory
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found !");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallback;

    // max inventory space
    public int space = 20;
    
    // create a list of items
    public List<Item> itemsList = new List<Item>();
    
    // Add an item to the items list
    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (itemsList.Count >= space)
            {
                Debug.Log("Not enough space in your inventory !");
                return false;
            }
            
            itemsList.Add(item);

            if (onItemChangedCallback != null)
            {
                // Trigger the event
                onItemChangedCallback.Invoke();
            }
            
        }

        // if item picked up, return true
        return true;
    }
    
    // Remove an item of the items list
    public void Remove(Item item)
    {
        itemsList.Remove(item);
        
        if (onItemChangedCallback != null)
        {
            // Trigger the event
            onItemChangedCallback.Invoke();
        }
    }
}
