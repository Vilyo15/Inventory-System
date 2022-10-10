using UnityEngine;
using UnityEngine.EventSystems;

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
        Type = InterfaceType.Inventory;
        _inventorySlots = new GameObject[Inventory.Inventory.InventoryObject.Length];

        for (int i = 0; i < Inventory.Inventory.InventoryObject.Length; i++)
        {
            var obj = Instantiate(InterfaceSlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.Select, delegate { OnSelect(obj); });
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

    private void UpdateInventory()
    {
        GameObject[] temp = new GameObject[Inventory.Inventory.InventoryObject.Length];
        _inventorySlots.CopyTo(temp, 0);
        _inventorySlots = temp;
        for (int i = 0; i < Inventory.Inventory.InventoryObject.Length; i++)
        {
            if (_inventorySlots[i] == null)
            {
                var obj = Instantiate(InterfaceSlotPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.Select, delegate { OnSelect(obj); });
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
    }
    private void Update()
    {
        if (Inventory.UpdateInventory)
        {
            UpdateInventory();
            _inventorySlots = new GameObject[Inventory.Inventory.InventoryObject.Length];
            Inventory.UpdateInventory = false;
        }
    }



}

