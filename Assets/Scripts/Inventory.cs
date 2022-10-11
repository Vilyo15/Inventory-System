using System;
using UnityEngine;
/// <summary>
/// inventory class, holds an array of inventory slots
/// </summary>
[System.Serializable]
public class Inventory
{
    public InventorySlot[] InventoryObject = new InventorySlot[28];

}
//delegate that triggers on slot update
public delegate void SlotUpdated(InventorySlot _slot);
/// <summary>
/// inventory slot class, holds all data associated with inventory/equip slots
/// </summary>
[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    public Item Item;
    public int Amount;
    public UserInterfaceBase Parent;
    public SlotUpdated OnAfterUpdate;
    public event Action OnBeforeUpdate;
    public GameObject GameObjectParent;

    public InventorySlot()
    {
        UpdateSlot(new Item(), 0);
    }
    public BaseItemScriptableObject ItemObject
    {
        get
        {
            if (Item.Id >= 0)
            {
                var temp = Parent.List.GetItem[Item.Id];
                return temp;
            }
            return null;
        }
    }
    public InventorySlot(Item item, int amount)
    {
        UpdateSlot(item, amount);
    }

    //updates a slot with new item/amount
    public void UpdateSlot(Item item, int amount)
    {
        OnBeforeUpdate?.Invoke();
        Item = item;
        Amount = amount;
        OnAfterUpdate?.Invoke(this);
    }

    //removes an item or amount
    public void RemoveItem()
    {
        if (Amount == 1)
        {
            UpdateSlot(new Item(), 0);
        }
        else if (Amount >= 0)
        {

            int amount = Amount - 1;
            UpdateSlot(Item, amount);
        }

    }

    //increases stack amount
    public void AddAmount(int value)
    {
        if (Item.MaxStack == 0)
        {
            UpdateSlot(Item, Amount += value);

        }
        else
        {

            if (Amount > Item.MaxStack)
            {

                Parent.Inventory.AddItem(Item, 1);
            }
            else
            {

                UpdateSlot(Item, Amount += value);
            }
        }
    }


    public bool CanPlaceInSlot(BaseItemScriptableObject itemObject)
    {
        Debug.Log("allowed items" + (AllowedItems.Length <= 0) + AllowedItems.Length) ;
        Debug.Log("item object" + (itemObject == null));
        //Debug.Log(itemObject.ItemReference.Id < 0);
        if (AllowedItems.Length <= 0 || itemObject == null || itemObject.ItemReference.Id < 0)
        {
            return true;
        }

        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (itemObject.Type == AllowedItems[i])
            {
                return true;
            }
        }
        return false;
    }
}
