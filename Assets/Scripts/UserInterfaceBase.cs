using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public abstract class UserInterfaceBase : MonoBehaviour
{
    [SerializeField] private InventoryScriptableObject _inventory;
    
    [SerializeField] private RectTransform _mouseObject;
    [SerializeField] private ExistingItemsScriptableObject list;
    [SerializeField] private GameObject _interfaceSlotPrefab;
    [SerializeField] private ItemObject itemPrefab;
    private Image _mouseImage;

    private Dictionary<GameObject, InventorySlot> _inventoryUI;
    private MouseItem _mouseItem = new MouseItem();

    public ExistingItemsScriptableObject List { get { return list; } }
    public InventoryScriptableObject Inventory { get { return _inventory; } set { _inventory = value; } }
    public GameObject InterfaceSlotPrefab { get { return _interfaceSlotPrefab; } }
    public Dictionary<GameObject, InventorySlot> InventoryUI { get { return _inventoryUI; } }

    public event Action onUpdate,beforeUpdate;

    // Start is called before the first frame update
    private void Start()
    {
        _inventoryUI = new Dictionary<GameObject, InventorySlot>();
        _mouseImage = _mouseObject.GetComponent<Image>();
        PopulateInventory();
        for (int i = 0; i < _inventory.Inventory.InventoryObject.Length; i++)
        {
            //_inventory.Inventory.InventoryObject[i].Parent = this;
            _inventory.Inventory.InventoryObject[i].OnAfterUpdate += OnSlotUpdate;
            _inventory.Inventory.InventoryObject[i].OnBeforeUpdate += OnBeforeUpdate;

        }
        
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    protected abstract void PopulateInventory();

    protected  void OnBeforeUpdate()
    {
        beforeUpdate?.Invoke();
    }

    protected void OnSlotUpdate(InventorySlot _slot)
    {
        if (_slot.Item.Id >= 0)
        {
            _slot.gameObjectParent.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.Sprite;
            _slot.gameObjectParent.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.gameObjectParent.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = _slot.Amount == 1 ? "" : _slot.Amount.ToString("n0");
        }
        else
        {
            _slot.gameObjectParent.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.gameObjectParent.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.gameObjectParent.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        onUpdate?.Invoke();
    }

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    protected void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
    }
    protected void OnExit(GameObject obj)
    {
        _mouseItem.HoverObj = null;
        _mouseItem.HoverItem = null;
    }
    //protected void OnDragStart(GameObject obj)
    //{
    //    _mouseObject.gameObject.SetActive(true);
    //    _mouseObject.sizeDelta = new Vector2(50, 50);
    //    _mouseObject.transform.SetParent(transform.parent);
    //    if (_inventoryUI[obj].Item.Id >= 0)
    //    {

    //        _mouseImage.sprite = list.GetItem[_inventoryUI[obj].Item.Id].Sprite;
    //        _mouseImage.raycastTarget = false;
    //    }
    //    _mouseItem.Obj = _mouseObject.gameObject;
    //    _mouseItem.Item = _inventoryUI[obj];
    //    MouseData.tempItemBeingDragged = _mouseObject.gameObject;
    //}
    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
    }
    protected void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);
        if (_mouseItem.HoverObj)
        {
            _inventory.MoveItem(_inventoryUI[obj], _inventoryUI[_mouseItem.HoverObj]);
        }


        if (MouseData.interfaceMouseIsOver == null)
        {
            SpawnDroppedItem(obj);
            _inventoryUI[obj].RemoveItem();
            
            
            
        }
        if (MouseData.slotHoveredOver && MouseData.interfaceMouseIsOver != null)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver._inventoryUI[MouseData.slotHoveredOver];
            _inventory.MoveItem(_inventoryUI[obj], mouseHoverSlotData);
        }

        //_mouseItem.Obj.SetActive(false);
        
        //_mouseItem.Item = null;
    }
    protected void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    protected void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterfaceBase>();
    }
    protected void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }

    protected void SpawnDroppedItem(GameObject obj)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 Worldpos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log(itemPrefab.Item);
        itemPrefab.Item = _inventoryUI[obj].Item.Reference;
        GameObject temp = Instantiate(itemPrefab.gameObject, Worldpos, Quaternion.identity, null);



    }
    private GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if (_inventoryUI[obj].Item.Id >= 0)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            tempItem.transform.SetParent(transform.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = _inventoryUI[obj].ItemObject.Sprite;
            img.raycastTarget = false;
        }
        return tempItem;
    }

}

public class MouseItem
{
    private GameObject _obj;
    private InventorySlot _item;
    private InventorySlot _hoverItem;
    private GameObject _hoverObj;

    public GameObject Obj { get { return _obj; } set { _obj = value; } }
    public InventorySlot Item { get { return _item; } set { _item = value; } }
    public InventorySlot HoverItem { get { return _hoverItem; } set { _hoverItem = value; } }
    public GameObject HoverObj { get { return _hoverObj; } set { _hoverObj = value; } }
}

public static class MouseData
{
    public static UserInterfaceBase interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHoveredOver;
}
