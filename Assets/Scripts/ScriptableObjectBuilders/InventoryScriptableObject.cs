    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryScriptableObject : ScriptableObject
{
    public Inventory Inventory;
    public bool UpdateInventory = false;
    public void AddItem(Item _item, int _amount)
    {
        
        if (_item.Reference.Type == ItemType.Material || _item.Reference.Type == ItemType.Consumable)
        {
            for (int i = 0; i < Inventory.InventoryObject.Length; i++)
            {
                if (Inventory.InventoryObject[i].Item.Id == _item.Id && Inventory.InventoryObject[i].Amount < Inventory.InventoryObject[i].Item.MaxStack)
                {
                    Inventory.InventoryObject[i].AddAmount(_amount);
                    return;
                }
                else if(Inventory.InventoryObject[i].Item.Id == _item.Id)
                {
                    if (Inventory.InventoryObject[i].Amount == Inventory.InventoryObject[i].Item.MaxStack)
                    {
                        continue;
                    }
                    else
                    {
                        Inventory.InventoryObject[i].AddAmount(_amount);
                        return;
                    }
                    
                }
                
                
            }
        }
        else
        {

            SetEmptySlot(_item, _amount);
            return;
        }

        SetEmptySlot(_item, _amount);

    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        bool fullness = false;
        for (int i = 0; i < Inventory.InventoryObject.Length; i++)
        {
            if (Inventory.InventoryObject[i].Item.Id <= -1)
            {
                Inventory.InventoryObject[i].UpdateSlot(_item, _amount);
                return Inventory.InventoryObject[i];
                
            }
            
        }
        InventorySlot[] temp = new InventorySlot[32];
        Inventory.InventoryObject.CopyTo(temp, 0);
        Inventory.InventoryObject = temp;
        UpdateInventory = true;
        //set up functionality for full inventory
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
