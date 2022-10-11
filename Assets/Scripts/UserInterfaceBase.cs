using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


/// <summary>
/// base of all user interfaces that contain item slots
/// </summary>
public abstract class UserInterfaceBase : MonoBehaviour
{

    //serialized fields for references and prefabs
    [SerializeField] private InventoryScriptableObject _inventory;
    [SerializeField] private RectTransform _mouseObject;
    [SerializeField] private ExistingItemsScriptableObject list;
    [SerializeField] private GameObject _interfaceSlotPrefab;
    [SerializeField] private ItemObject _itemPrefab;
    [SerializeField] private GameObject _tooltip;
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private EquipScreenUI _referenceEquip;
    [SerializeField] private UIController _mainParent;
    [SerializeField] private InventoryUI _referenceInventory;

    //main inventory dictionary for inventory slots
    private Dictionary<GameObject, InventorySlot> _inventoryUI;

    //define the type of interface, either inventory or equipment (can add more later like storage)
    private InterfaceType _type;

    //variables to detect hovering, used for tooltips
    private bool _isHovering;


    //player input
    private PlayerControls _playerInput;

    //getters and setters
    public InterfaceType Type { get { return _type; } set { _type = value; } }
    public ExistingItemsScriptableObject List { get { return list; } }
    public InventoryScriptableObject Inventory { get { return _inventory; } set { _inventory = value; } }
    public GameObject InterfaceSlotPrefab { get { return _interfaceSlotPrefab; } }
    public Dictionary<GameObject, InventorySlot> InventoryUI { get { return _inventoryUI; } }

    //events for updating the inventory slots
    public event Action onUpdate, beforeUpdate;

    private void Awake()
    {
        _playerInput = new PlayerControls();
        _inventoryUI = new Dictionary<GameObject, InventorySlot>();

        PopulateInventory();
        for (int i = 0; i < _inventory.Inventory.InventoryObject.Length; i++)
        {
            _inventory.Inventory.InventoryObject[i].OnAfterUpdate += OnSlotUpdate;
            _inventory.Inventory.InventoryObject[i].OnBeforeUpdate += OnBeforeUpdate;

        }

        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });

        _playerInput.UserInterface.DropItem.started += DropItemAtPlayer;
        _playerInput.UserInterface.EquipItem.started += EquipHoverItem;
    }
    private void OnEnable()
    {
        _playerInput.UserInterface.Enable();
        UpdateInventoryUI();
    }
    private void OnDisable()
    {
        _playerInput.UserInterface.Disable();
        Destroy(MouseData.tempItemBeingDragged);
    }

    //each interface type uses a different populate function
    protected abstract void PopulateInventory();

    protected void OnBeforeUpdate()
    {
        beforeUpdate?.Invoke();
    }

    protected void OnSlotUpdate(InventorySlot _slot)
    {
        if (_slot.Item.Id >= 0)
        {
            _slot.GameObjectParent.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.Sprite;
            _slot.GameObjectParent.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.GameObjectParent.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = _slot.Amount == 1 ? "" : _slot.Amount.ToString("n0");
        }
        else
        {
            _slot.GameObjectParent.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.GameObjectParent.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.GameObjectParent.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        onUpdate?.Invoke();
    }

    //adds event triggers to inventory slots
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    #region event functions
    protected void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
        if (_inventoryUI[obj].Item.Id != -1)
        {
            StartCoroutine(ShowTooltip(obj));
        }
        _isHovering = true;
        
    }
    protected void OnExit(GameObject obj)
    {

        StopCoroutine(ShowTooltip(obj));
        _tooltip.SetActive(false);
        _isHovering = false;
    }

    protected void OnSelect(GameObject obj)
    {

    }

    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
        obj.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }

    protected void OnDragEnd(GameObject obj)
    {

        Destroy(MouseData.tempItemBeingDragged);



        if (MouseData.interfaceMouseIsOver == null)
        {
            SpawnDroppedItem(obj);

        }
        if (MouseData.slotHoveredOver && MouseData.interfaceMouseIsOver != null)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver._inventoryUI[MouseData.slotHoveredOver];
            _inventory.MoveItem(_inventoryUI[obj], mouseHoverSlotData);
        }
        obj.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

    }
    protected void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    protected void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterfaceBase>();

    }
    protected void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;

    }
    #endregion


    #region event support functions
    protected void SpawnDroppedItem(GameObject obj)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 Worldpos = Camera.main.ScreenToWorldPoint(mousePos);
        _itemPrefab.Item = _inventoryUI[obj].Item.Reference;
        GameObject temp = Instantiate(_itemPrefab.gameObject, Worldpos, Quaternion.identity, null);
        _inventoryUI[obj].RemoveItem();
    }
    private GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        GameObject tempText = null;
        if (_inventoryUI[obj].Item.Id >= 0)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            tempItem.transform.SetParent(transform.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = _inventoryUI[obj].ItemObject.Sprite;
            img.raycastTarget = false;

            tempText = new GameObject();
            var tmpro = tempText.AddComponent<TextMeshProUGUI>();
            var rt2 = tempText.GetComponent<RectTransform>();
            rt2.sizeDelta = new Vector2(22f, 22f);
            rt2.position = new Vector3(14.2f, -14.2f, 0f);
            tmpro.text = _inventoryUI[obj].Amount.ToString();
            tmpro.fontSize = 27.55f;
            tmpro.color = new Color(225, 225, 0, 225);
            //tmpro.enableAutoSizing = true;
            tmpro.alignment = TextAlignmentOptions.BottomRight;
            tempText.transform.SetParent(tempItem.transform);
        }
        return tempItem;
    }
    private void DropItemAtPlayer(InputAction.CallbackContext context)
    {
        if (_isHovering && _inventoryUI[MouseData.slotHoveredOver].Item.Id != -1 && this.Type == InterfaceType.Inventory)
        {

            Vector3 Worldpos = _playerObject.transform.position + new Vector3(2, 2, 0);
            _itemPrefab.Item = _inventoryUI[MouseData.slotHoveredOver].Item.Reference;
            GameObject temp = Instantiate(_itemPrefab.gameObject, Worldpos, Quaternion.identity, null);
            _inventoryUI[MouseData.slotHoveredOver].RemoveItem();
        }
    }
    private IEnumerator ShowTooltip(GameObject obj)
    {
        _tooltip.SetActive(true);
        _tooltip.GetComponentInChildren<TextMeshProUGUI>().text = _inventoryUI[obj].Item.Reference.Description;
        yield return null;
    }

    private void EquipHoverItem(InputAction.CallbackContext context)
    {
        if (_isHovering && _inventoryUI[MouseData.slotHoveredOver].Item.Id != -1 && this.Type == InterfaceType.Inventory && _inventoryUI[MouseData.slotHoveredOver].Item.Reference.Type != ItemType.Consumable)
        {
            if (!_referenceEquip.gameObject.activeSelf)
            {
                _mainParent.EquipmentScreenActivate(true);
                _mainParent.AttributeScreenActivate(true);
            }
            if (_inventoryUI[MouseData.slotHoveredOver].Item.Reference.Type != ItemType.Consumable && _inventoryUI[MouseData.slotHoveredOver].Item.Id != -1)
            {
                for (int i = 0; i < _referenceEquip.Inventory.Inventory.InventoryObject.Length; i++)
                {
                    var obj = _referenceEquip.EquipSlots[i];

                    for (int j = 0; j < _referenceEquip.InventoryUI[obj].AllowedItems.Length; j++)
                    {
                        if (_inventoryUI[MouseData.slotHoveredOver].Item.Id != -1)
                        {
                            if (_referenceEquip.InventoryUI[obj].AllowedItems[j] == _inventoryUI[MouseData.slotHoveredOver].Item.Reference.Type)
                            {
                                _inventory.MoveItem(_inventoryUI[MouseData.slotHoveredOver], _referenceEquip.InventoryUI[obj]);
                                break;
                            }
                        }

                    }
                }
            }
            
        }
        else if (_isHovering && _referenceEquip.InventoryUI[MouseData.slotHoveredOver].Item.Id != -1 && this.Type == InterfaceType.Equipment )
        {

            
            {

                for (int i = 0; i < _inventory.Inventory.InventoryObject.Length; i++)
                {

                    var obj = _referenceInventory.InventorySlots[i];

                    if (_referenceInventory.InventoryUI[obj].Item.Id == -1)
                    {
                       
                        _referenceInventory.Inventory.MoveItem(_referenceInventory.InventoryUI[obj], _referenceEquip.InventoryUI[MouseData.slotHoveredOver]);
                        break;
                    }


                }
            }
        }
    }
    #endregion


    //updates inventoryUI on enable.
    protected void UpdateInventoryUI()
    {
        for (int i = 0; i < _inventory.Inventory.InventoryObject.Length; i++)
        {
            if (this._type == InterfaceType.Inventory)
            {
                OnSlotUpdate(_inventory.Inventory.InventoryObject[i]);
            }
        }
    }

   
}

//support class for events, used to assist with dragging and item interactions
public static class MouseData
{
    public static UserInterfaceBase interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHoveredOver;
}

public enum InterfaceType
{
    Equipment,
    Inventory
}
