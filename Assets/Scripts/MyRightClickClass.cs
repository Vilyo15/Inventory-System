using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MyRightClickClass : MonoBehaviour, IPointerClickHandler
{
    private InterfaceType _parentType;
    private UIController _mainParent;
    private InventoryUI _referenceInventory;
    private EquipScreenUI _referenceEquip;


    private void Start()
    {
        _mainParent = transform.parent.parent.GetComponent<UIController>();
        _parentType = transform.parent.GetComponent<UserInterfaceBase>().Type;
        _referenceInventory = _mainParent.InventoryScreen;
        _referenceEquip = _mainParent.EquipmentScreen;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (_parentType == InterfaceType.Inventory)
        {

            if (eventData.button == PointerEventData.InputButton.Right)
            {

                for (int i = 0; i < _referenceEquip.Inventory.Inventory.InventoryObject.Length; i++)
                {
                    var obj = _referenceEquip.EquipSlots[i];

                    for (int j = 0; j < _referenceEquip.InventoryUI[obj].AllowedItems.Length; j++)
                    {
                        if (_referenceInventory.InventoryUI[gameObject].Item.Id != -1)
                        {
                            if (_referenceEquip.InventoryUI[obj].AllowedItems[j] == _referenceInventory.InventoryUI[gameObject].Item.Reference.Type)
                            {
                                _referenceInventory.Inventory.MoveItem(_referenceInventory.InventoryUI[gameObject], _referenceEquip.InventoryUI[obj]);
                                break;
                            }
                        }

                    }
                }
            }
        }
        else if (_parentType == InterfaceType.Equipment)
        {

            if (eventData.button == PointerEventData.InputButton.Right)
            {

                for (int i = 0; i < _referenceInventory.Inventory.Inventory.InventoryObject.Length; i++)
                {

                    var obj = _referenceInventory.InventorySlots[i];

                    if (_referenceInventory.InventoryUI[obj].Item.Id == -1)
                    {
                        _referenceInventory.Inventory.MoveItem(_referenceInventory.InventoryUI[obj], _referenceEquip.InventoryUI[gameObject]);
                        break;
                    }


                }
            }
        }

    }

}