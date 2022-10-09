using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryUI : UserInterfaceBase
{
    [SerializeField] private EquipScreenUI _equipInventory; 
    private int CONSTANT_X_START = -75;
    private int CONSTANT_Y_START = 150;
    private int CONSTANT_X_SPACE_BETWEEN_ITEM = 50;
    private int CONSTANT_NUMBER_OF_COLUMN = 4;
    private int CONSTANT_Y_SPACE_BETWEEN_ITEMS = 50;
    private GameObject[] _inventorySlots;
    public EquipScreenUI EquipInventory { get { return _equipInventory; } }
    public GameObject[] InventorySlots { get { return _inventorySlots; } }
    protected override void PopulateInventory()
    {
        _inventorySlots = new GameObject[Inventory.Inventory.InventoryObject.Length];

        for (int i = 0; i < Inventory.Inventory.InventoryObject.Length; i++)
        {
            var obj = Instantiate(InterfaceSlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            obj.AddComponent<MyRightClickClass>();

            _inventorySlots[i] = obj;
            Inventory.Inventory.InventoryObject[i].gameObjectParent = obj;
            InventoryUI.Add(obj, Inventory.Inventory.InventoryObject[i]);
            InventoryUI[obj].Parent = this;
        }
    }
    private Vector3 GetPosition(int i)
    {
        return new Vector3(CONSTANT_X_START + (CONSTANT_X_SPACE_BETWEEN_ITEM * (i % CONSTANT_NUMBER_OF_COLUMN)), CONSTANT_Y_START + (-CONSTANT_Y_SPACE_BETWEEN_ITEMS * (i / CONSTANT_NUMBER_OF_COLUMN)), 0f);
    }

    private void OnApplicationQuit()
    {
        Inventory.Clear();
    }


}

public class MyRightClickClass : MonoBehaviour, IPointerClickHandler
{
    private UIController _mainParent;
    private InventoryUI _referenceInventory;
    private EquipScreenUI _referenceEquip;

    private void Start()
    {
        _mainParent = transform.parent.parent.GetComponent<UIController>();
        _referenceInventory = _mainParent.InventoryScreen;
        _referenceEquip = _mainParent.EquipmentScreen;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        if (gameObject.GetComponentInParent<InventoryUI>())
        {
            Debug.Log("abb");
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Debug.Log("acc");

                for (int i = 0; i < _referenceEquip.Inventory.Inventory.InventoryObject.Length; i++)
                {
                    var obj = _referenceEquip.EquipSlots[i];

                    for (int j = 0; j < _referenceEquip.InventoryUI[obj].AllowedItems.Length; j++)
                    {
                        if (_referenceInventory.InventoryUI[gameObject].Item.Id != -1)
                        {
                            if (_referenceEquip.InventoryUI[obj].AllowedItems[j] == _referenceInventory.InventoryUI[gameObject].Item.Reference.Type)
                            {
                                Debug.Log("i am here");
                                _referenceInventory.Inventory.MoveItem(_referenceInventory.InventoryUI[gameObject], _referenceEquip.InventoryUI[obj]);
                                break;
                            }
                        }
                        
                    }
                }
            }
        }
        else if (gameObject.GetComponentInParent<EquipScreenUI>())
        {
            Debug.Log("amm");
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Debug.Log("amm2");
                for (int i = 0; i < _referenceInventory.Inventory.Inventory.InventoryObject.Length; i++)
                {
                    Debug.Log("mma3");
                    var obj = _referenceInventory.InventorySlots[i];

                    if (_referenceInventory.InventoryUI[obj].Item.Id == -1)
                    {
                        Debug.Log("i am here too");
                        _referenceInventory.Inventory.MoveItem(_referenceInventory.InventoryUI[obj], _referenceEquip.InventoryUI[gameObject]);
                        break;
                    }

                   
                }
            }
        }
        
    }

}