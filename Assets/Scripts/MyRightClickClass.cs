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

    public void SetParent(InterfaceType type)
    {
        _parentType = type;
    }

    private void Start()
    {
        if ( _parentType == InterfaceType.Inventory)
        {
            _mainParent = transform.parent.parent.parent.parent.GetComponent<UIController>();
            _referenceInventory = _mainParent.InventoryScreen;
            _referenceEquip = _mainParent.EquipmentScreen;
        }
        else if (_parentType == InterfaceType.Equipment)
        {
            _mainParent = transform.parent.parent.GetComponent<UIController>();
            _referenceInventory = _mainParent.InventoryScreen;
            _referenceEquip = _mainParent.EquipmentScreen;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (_parentType == InterfaceType.Inventory)
        {

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (!_referenceEquip.gameObject.activeSelf)
                {
                    _mainParent.EquipmentScreenActivate(true);
                    _mainParent.AttributeScreenActivate(true);
                }
                if (_referenceInventory.InventoryUI[gameObject].Item.Reference.Type != ItemType.Consumable && _referenceInventory.InventoryUI[gameObject].Item.Id != -1)
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
            if (eventData.button == PointerEventData.InputButton.Middle)
                if (_referenceInventory.InventoryUI[gameObject].Item.Reference.Type == ItemType.Consumable && _referenceInventory.InventoryUI[gameObject].Item.Id != -1)
                {
                    for (int j = 0; j < _referenceInventory.InventoryUI[gameObject].Item.Buffs.Length; j++)
                    {
                        if (_referenceInventory.InventoryUI[gameObject].Item.Buffs[j] != null)
                        {
                            _referenceEquip.Player.IncreaseAttribute(_referenceInventory.InventoryUI[gameObject].Item.Buffs[j].Attribute, _referenceInventory.InventoryUI[gameObject].Item.Buffs[j].Value);

                        }
                        else
                        {

                        }

                    }
                    _referenceInventory.InventoryUI[gameObject].RemoveItem();
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