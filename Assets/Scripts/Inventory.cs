using UnityEngine;

[System.Serializable]

public class Inventory 
{
    public InventorySlot[] InventoryObject = new InventorySlot[28];
    
}
public delegate void SlotUpdated(InventorySlot _slot);
[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    public Item Item;
    public int Amount;
    public UserInterfaceBase Parent;
    public SlotUpdated OnAfterUpdate;
    public SlotUpdated OnBeforeUpdate;
    public GameObject gameObjectParent;

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
                return  temp;
            }
            return null;
        }
    }
    public InventorySlot( Item _item, int _amount)
    {
        UpdateSlot(_item, _amount);
    }
    public void UpdateSlot(Item item, int amount)
    {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);
        Item = item;
        Amount = amount;
        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }
    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0);
    }
    public void AddAmount(int value)
    {
        UpdateSlot(Item, Amount += value);
    }


    public bool CanPlaceInSlot(BaseItemScriptableObject _itemObject)
    {
        if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.ItemReference.Id < 0)
            return true;
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_itemObject.Type == AllowedItems[i])
                return true;
        }
        return false;
    }
}
