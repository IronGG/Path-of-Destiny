using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    // This code is a blueprint to every item that we can pickup

    // Item's variables
    new public string name = "New Item"; // name
    public Sprite icon = null; // icon
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        // Use the item
        // Something might happen

        Debug.Log("Used item : " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
