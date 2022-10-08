using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material Item", menuName = "Inventory System/Items/Material")]
public class MaterialScriptableObject : BaseItemScriptableObject
{
    public void Awake()
    {
        Type = ItemType.Material;
    }
}
