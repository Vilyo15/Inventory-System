using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment,
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
    public int Id;
    public Sprite Sprite;
    public ItemType Type;
    [TextArea(15, 20)]
    public string Description;
    public ItemBuff[] Buffs;

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
 
    public Item(BaseItemScriptableObject item)
    {
        Name = item.name;
        Id = item.Id;
        Buffs = new ItemBuff[item.Buffs.Length];
        Reference = item;
        for (int i = 0; i < Buffs.Length; i++)
        {
            Buffs[i] = new ItemBuff(item.Buffs[i].Min, item.Buffs[i].Max)
            {
                Attribute = item.Buffs[i].Attribute
            };
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes Attribute;
    public int Value;
    public int Min;
    public int Max;

   
    public ItemBuff(int min, int max)
    {
        Min = min;
        Max = max;
        GenerateValue();
    }
    private void GenerateValue()
    {
        Value = UnityEngine.Random.Range(Min, Max);
    }
}
