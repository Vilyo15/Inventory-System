using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// scriptable object used as a database for all items existing in the game, needed for references for creating new items
/// </summary>
[CreateAssetMenu(fileName = "Existing Items List", menuName = "Inventory System/List")]
public class ExistingItemsScriptableObject : ScriptableObject
{
    public BaseItemScriptableObject[] Items;
    public Dictionary<int, BaseItemScriptableObject> GetItem = new Dictionary<int, BaseItemScriptableObject>();

    private void OnEnable()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].ItemReference.Id = i;
            GetItem.Add(i, Items[i]);
        }
    }

}
