using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public GameObject inventoryUI;
    
    public Transform itemsParent;
    
    private Inventory inventory;

    private InventorySlot[] slots;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            // put the reverse state of the last state
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    // Updating the inventory UI
    void UpdateUI()
    {
        // for each slot in the inventory, we are looking if it's used or not to add the image
        for (int i = 0; i < slots.Length; i++)
        {
            // add item because we have something inside the slot
            if (i < inventory.itemsList.Count)
            {
                slots[i].AddItem(inventory.itemsList[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
