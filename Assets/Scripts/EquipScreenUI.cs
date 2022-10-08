using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipScreenUI : UserInterfaceBase
{
    [SerializeField] private GameObject[] _equipSlots;
    [SerializeField] private PlayerBaseScriptableObject _player;
    protected override void PopulateInventory()
    {
        Inventory = Instantiate(Inventory);
        for (int i = 0; i < Inventory.Inventory.InventoryObject.Length; i++)
        {
            
            var obj = _equipSlots[i];

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            Inventory.Inventory.InventoryObject[i].gameObjectParent = obj;
            InventoryUI.Add(obj, Inventory.Inventory.InventoryObject[i]);
            InventoryUI[obj].Parent = this;

        }
        

    }

    private void Awake()
    {
        onUpdate += UpdatePlayerAttributes;
        beforeUpdate += RemovePlayerAttributes;
    }

    private void RemovePlayerAttributes()
    {
        for (int i = 0; i < Inventory.Inventory.InventoryObject.Length; i++)
        {

            var obj = _equipSlots[i];
            for (int j = 0; j < InventoryUI[obj].Item.Buffs.Length; j++)
            {
                if (InventoryUI[obj].Item.Buffs[j] != null)
                {
                    _player.DecreaseAttribute(InventoryUI[obj].Item.Buffs[j].Attribute, InventoryUI[obj].Item.Buffs[j].Value);
                    Debug.Log(InventoryUI[obj].Item.Buffs[j].Attribute);
                    Debug.Log(InventoryUI[obj].Item.Buffs[j].Value);
                }
                else
                {

                }

            }
        }
    }

    private void UpdatePlayerAttributes()
    {
        for (int i = 0; i < Inventory.Inventory.InventoryObject.Length; i++)
        {

            var obj = _equipSlots[i];
            for (int j = 0; j < InventoryUI[obj].Item.Buffs.Length; j++)
            {
                if (InventoryUI[obj].Item.Buffs[j] != null)
                {
                    _player.IncreaseAttribute(InventoryUI[obj].Item.Buffs[j].Attribute, InventoryUI[obj].Item.Buffs[j].Value);
                    Debug.Log(InventoryUI[obj].Item.Buffs[j].Attribute);
                    Debug.Log(InventoryUI[obj].Item.Buffs[j].Value);
                }
                else
                {

                }
                
            }
        }
    }

}
    