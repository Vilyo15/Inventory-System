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
            if (Inventory.InventoryObject[i].ID == _item.Id)
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
            if (Inventory.InventoryObject[i].ID <= -1)
            {
                Inventory.InventoryObject[i].UpdateSlot(_item.Id, _item, _amount);
                return Inventory.InventoryObject[i];
            }
        }
        //set up functionality for full inventory
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.Item, item2.Amount);
        item2.UpdateSlot(item1.ID, item1.Item, item1.Amount);
        item1.UpdateSlot(temp.ID, temp.Item, temp.Amount);
    }


    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Inventory.InventoryObject.Length; i++)
        {
            if (Inventory.InventoryObject[i].Item == _item)
            {
                Inventory.InventoryObject[i].UpdateSlot(-1, null, 0);
            }
        }
    }

    public void Clear()
    {
        Inventory = new Inventory();
    }


}
