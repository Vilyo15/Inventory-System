using UnityEngine;

public enum ItemType
{
    Consumable,
    Helmet,
    Chest,
    Shield,
    Pants,
    Shoes,
    Weapon,
    Material,
    Upgrade
}

public enum Attributes
{
    Attack,
    Defence,
    Speed,
    Dodge
}

/// <summary>
/// scriptable item for item bases, accepts item objects and data associated with displaying the item objects
/// </summary>
public class BaseItemScriptableObject : ScriptableObject
{
    public Sprite Sprite;
    public ItemType Type;
    [TextArea(15, 20)]
    public string Description;
    public Item ItemReference = new Item();
    public int MaxStack;

}

/// <summary>
/// item class, contaits data about items themselves and builders for creating them.
/// </summary>
[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] Buffs;
    public BaseItemScriptableObject Reference;
    public int MaxStack;

    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(BaseItemScriptableObject item)
    {
        Name = item.name;
        Id = item.ItemReference.Id;
        Buffs = new ItemBuff[item.ItemReference.Buffs.Length];
        Reference = item;
        MaxStack = item.MaxStack;
        for (int i = 0; i < Buffs.Length; i++)
        {
            Buffs[i] = new ItemBuff(item.ItemReference.Buffs[i].Value)
            {
                Attribute = item.ItemReference.Buffs[i].Attribute
            };
        }
    }
}
/// <summary>
/// item buff class, attribute and value, simple
/// </summary>
[System.Serializable]
public class ItemBuff
{
    public Attributes Attribute;
    public int Value;



    public ItemBuff(int value)
    {
        Attribute = Attributes.Attack;
        Value = value;
    }

}
