using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryScriptableObject _inventory;
    [SerializeField] private MouseItem _mouseItem = new MouseItem();
    [SerializeField] private RectTransform _mouseObject;
    [SerializeField] private ExistingItemsScriptableObject list;
    [SerializeField] private GameObject _inventoryPrefab;
    [SerializeField] private ItemObject itemPrefab;
    private Image _mouseImage;

    private Dictionary<GameObject, InventorySlot> _inventoryUI;

    private int CONSTANT_X_START = -75;
    private int CONSTANT_Y_START = 150;
    private int CONSTANT_X_SPACE_BETWEEN_ITEM = 50;
    private int CONSTANT_NUMBER_OF_COLUMN = 4;
    private int CONSTANT_Y_SPACE_BETWEEN_ITEMS = 50;

    // Start is called before the first frame update
    private void Start()
    {
        _inventoryUI = new Dictionary<GameObject, InventorySlot>();
        _mouseImage = _mouseObject.GetComponent<Image>();
        PopulateInventory();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateSlots();
    }
    private void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _inventoryUI)
        {
            if (_slot.Value.ID >= 0)
            {

                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = list.GetItem[_slot.Value.Item.Id].Sprite;

                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.Amount == 1 ? "" : _slot.Value.Amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void PopulateInventory()
    {
        for (int i = 0; i < _inventory.Inventory.InventoryObject.Length; i++)
        {
            var obj = Instantiate(_inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });


            _inventoryUI.Add(obj, _inventory.Inventory.InventoryObject[i]);
        }
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(CONSTANT_X_START + (CONSTANT_X_SPACE_BETWEEN_ITEM * (i % CONSTANT_NUMBER_OF_COLUMN)), CONSTANT_Y_START + (-CONSTANT_Y_SPACE_BETWEEN_ITEMS * (i / CONSTANT_NUMBER_OF_COLUMN)), 0f);
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    private void OnEnter(GameObject obj)
    {
        _mouseItem.HoverObj = obj;
        if (_inventoryUI.ContainsKey(obj))
        {
            _mouseItem.HoverItem = _inventoryUI[obj];
        }
    }
    private void OnExit(GameObject obj)
    {
        _mouseItem.HoverObj = null;
        _mouseItem.HoverItem = null;
    }
    private void OnDragStart(GameObject obj)
    {

        _mouseObject.gameObject.SetActive(true);
        _mouseObject.sizeDelta = new Vector2(50, 50);
        _mouseObject.transform.SetParent(transform.parent);
        if (_inventoryUI[obj].ID >= 0)
        {

            _mouseImage.sprite = list.GetItem[_inventoryUI[obj].ID].Sprite;
            _mouseImage.raycastTarget = false;
        }
        _mouseItem.Obj = _mouseObject.gameObject;
        _mouseItem.Item = _inventoryUI[obj];
    }
    private void OnDragEnd(GameObject obj)
    {
        if (_mouseItem.HoverObj)
        {
            _inventory.MoveItem(_inventoryUI[obj], _inventoryUI[_mouseItem.HoverObj]);
        }
        else
        {
            SpawnDroppedItem(obj);
            _inventory.RemoveItem(_inventoryUI[obj].Item);
        }
        _mouseItem.Obj.SetActive(false);
        _mouseItem.Item = null;
    }
    private void OnDrag(GameObject obj)
    {
        if (_mouseItem.Obj != null)
        {
            _mouseItem.Obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    private void SpawnDroppedItem(GameObject obj)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 Worldpos = Camera.main.ScreenToWorldPoint(mousePos);
        itemPrefab.Item = _inventoryUI[obj].Item.Reference;
        GameObject temp = Instantiate(itemPrefab.gameObject, Worldpos, Quaternion.identity, null);
        


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
