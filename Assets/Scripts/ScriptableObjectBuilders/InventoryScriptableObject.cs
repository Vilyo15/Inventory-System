    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryScriptableObject : ScriptableObject
{
    public Inventory Inventory;
    public void AddItem(Item _item, int _amount)
    {
        if (_item.Buffs.Length > 0)
        {
            SetEmptySlot(_item, _amount);
            return;
        }

        for (int i = 0; i < Inventory.InventoryObject.Length; i++)
        {
            if (Inventory.InventoryObject[i].Item.Id == _item.Id)
            {
                Inventory.InventoryObject[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);

    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Inventory.InventoryObject.Length; i++)
        {
            if (Inventory.InventoryObject[i].Item.Id <= -1)
            {
                Inventory.InventoryObject[i].UpdateSlot(_item, _amount);
                return Inventory.InventoryObject[i];
            }
        }
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


    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Inventory.InventoryObject.Length; i++)
        {
            if (Inventory.InventoryObject[i].Item == _item)
            {
                Inventory.InventoryObject[i].UpdateSlot(null, 0);
            }
        }
    }

    public void Clear()
    {
        Inventory = new Inventory();
    }


}
