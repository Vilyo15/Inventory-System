[System.Serializable]
public class Inventory 
{
    public InventorySlot[] InventoryObject = new InventorySlot[28];
    
}

[System.Serializable]
public class InventorySlot
{
    public int ID = -1;
    public Item Item;
    public int Amount;
   
    public InventorySlot()
    {
        ID = -1;
        Item = null;
        Amount = 0;
    }
    public InventorySlot(int _id, Item _item, int _amount)
    {
        this.ID = _id;
        this.Item = _item;
        this.Amount = _amount;
    }
    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        this.ID = _id;
        this.Item = _item;
        this.Amount = _amount;
    }
    public void AddAmount(int value)
    {
        Amount += value;
    }
}
