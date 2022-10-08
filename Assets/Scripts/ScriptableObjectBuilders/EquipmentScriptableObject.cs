using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory System/Items/Equipment")]
public class EquipmentScriptableObject : BaseItemScriptableObject
{
    public void Awake()
    {
        Type = ItemType.Equipment;
    }
}
