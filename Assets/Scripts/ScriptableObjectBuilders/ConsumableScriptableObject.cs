using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory System/Items/Consumable")]
public class ConsumableScriptableObject : BaseItemScriptableObject
{
    public void Awake()
    {
        Type = ItemType.Consumable;
    }
}
