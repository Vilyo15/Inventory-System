using UnityEngine;

/// <summary>
/// scriptable object that holds all data associated with an inventory object,
/// a unique one must be created for each use (inventory, equip, storage..)
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryScriptableObject : ScriptableObject
{
    public Inventory Inventory;
    public bool UpdateInventory = false;
    public Item tempItem;
    public int tempAmount;

    //adds an item to the inventory (on pickup), if item ID already pressent adds to the stack
    public void AddItem(Item item, int amount)
    {

        if (item.Reference.Type == ItemType.Material || item.Reference.Type == ItemType.Consumable)
        {
            for (int i = 0; i < Inventory.InventoryObject.Length; i++)
            {
                if (Inventory.InventoryObject[i].Item.Id == item.Id && Inventory.InventoryObject[i].Amount < Inventory.InventoryObject[i].Item.MaxStack)
                {
                    Inventory.InventoryObject[i].AddAmount(amount);
                    return;
                }
                else if (Inventory.InventoryObject[i].Item.Id == item.Id)
                {
                    if (Inventory.InventoryObject[i].Amount == Inventory.InventoryObject[i].Item.MaxStack)
                    {
                        continue;
                    }
                    else
                    {
                        Inventory.InventoryObject[i].AddAmount(amount);
                        return;
                    }

                }


            }
        }
        else
        {

            SetEmptySlot(item, amount);
            return;
        }

        SetEmptySlot(item, amount);

    }

    //if no item present or stack is ful, it adds the item to the next empty slot, if no empty slot
    //triggers expand inventory.
    public InventorySlot SetEmptySlot(Item item, int amount)
    {

        for (int i = 0; i < Inventory.InventoryObject.Length; i++)
        {
            if (Inventory.InventoryObject[i].Item.Id <= -1)
            {
                Inventory.InventoryObject[i].UpdateSlot(item, amount);
                return Inventory.InventoryObject[i];


            }


        }

        UpdateInventory = true;
        tempAmount = amount;
        tempItem = item;


        return null;
    }

    //moves item from one slot to a different one
    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        Debug.Log((item2.CanPlaceInSlot(item1.ItemObject) + " " + item1.CanPlaceInSlot(item2.ItemObject)));
        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        {
            InventorySlot temp = new InventorySlot(item2.Item, item2.Amount);
            item2.UpdateSlot(item1.Item, item1.Amount);
            item1.UpdateSlot(temp.Item, temp.Amount);
        }
    }

    //generates a new empty inventory
    public void Clear()
    {
        Inventory = new Inventory();
    }


}
