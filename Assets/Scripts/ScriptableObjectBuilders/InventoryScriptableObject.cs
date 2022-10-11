    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryScriptableObject : ScriptableObject
{
    public Inventory Inventory;
    public bool UpdateInventory = false;
    public Item tempItem;
    public int tempAmount;
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
                else if(Inventory.InventoryObject[i].Item.Id == item.Id)
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
    public InventorySlot SetEmptySlot(Item item, int amount)
    {
        Debug.Log("emptyslot start");

        for (int i = 0; i < Inventory.InventoryObject.Length; i++)
        {
            if (Inventory.InventoryObject[i].Item.Id <= -1)
            {
                Inventory.InventoryObject[i].UpdateSlot(item, amount);
                return Inventory.InventoryObject[i];
                
                
            }
            
            
        }

        Debug.Log("emptyslot end");
        
        
        UpdateInventory = true;
        tempAmount = amount;
        tempItem = item;
        

        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        {
            InventorySlot temp = new InventorySlot(item2.Item, item2.Amount);
            item2.UpdateSlot(item1.Item, item1.Amount);
            item1.UpdateSlot(temp.Item, temp.Amount);
        }
    }


    public void Clear()
    {
        Inventory = new Inventory();
    }


}
