using System.Collections;
using System.Collections.Generic;
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
    Material
}

public enum Attributes
{
    Attack,
    Defence,
    Speed,
    Dodge
}


public class BaseItemScriptableObject : ScriptableObject
{
    public Sprite Sprite;
    public ItemType Type;
    [TextArea(15, 20)]
    public string Description;
    public Item ItemReference = new Item();

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

   
}
[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] Buffs;
    public BaseItemScriptableObject Reference;

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
        for (int i = 0; i < Buffs.Length; i++)
        {
            Buffs[i] = new ItemBuff(item.ItemReference.Buffs[i].Value)
            {
                Attribute = item.ItemReference.Buffs[i].Attribute
            };
        }
    }
}

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
