using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// inventory user interface type
/// </summary>
public class InventoryUI : UserInterfaceBase
{
    //serialized fields
    [SerializeField] private EquipScreenUI _equipInventory;
    [SerializeField] private Transform _scroller;

    //reference for inventory slots, used by rightclick functions
    private GameObject[] _inventorySlots;

    //getters and setters
    public EquipScreenUI EquipInventory { get { return _equipInventory; } }
    public GameObject[] InventorySlots { get { return _inventorySlots; } }

    private void Update()
    {
        //check if inventory was expanded and requires an update.
        if (Inventory.UpdateInventory)
        {
            Inventory.UpdateInventory = false;
            UpdateInventory();


        }
    }

    //populates inventory on game start, generates inventory slots and assigns events
    protected override void PopulateInventory()
    {
        Type = InterfaceType.Inventory;
        _inventorySlots = new GameObject[Inventory.Inventory.InventoryObject.Length];

        for (int i = 0; i < Inventory.Inventory.InventoryObject.Length; i++)
        {
            var obj = Instantiate(InterfaceSlotPrefab, Vector3.zero, Quaternion.identity, _scroller);
           
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.Select, delegate { OnSelect(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            var temp = obj.AddComponent<MyRightClickClass>();
            temp.SetParent(Type);
            _inventorySlots[i] = obj;
            Inventory.Inventory.InventoryObject[i].GameObjectParent = obj;
            InventoryUI.Add(obj, Inventory.Inventory.InventoryObject[i]);
            InventoryUI[obj].Parent = this;
        }
    }
    
    //adds inventory slots if inventory was expanded due to being full. adds events
    private void UpdateInventory()
    {
        InventorySlot[] tempSlots = new InventorySlot[Inventory.Inventory.InventoryObject.Length + 4];
        Inventory.Inventory.InventoryObject.CopyTo(tempSlots, 0);
        Inventory.Inventory.InventoryObject = tempSlots;

        GameObject[] temp = new GameObject[Inventory.Inventory.InventoryObject.Length];
        _inventorySlots.CopyTo(temp, 0);
        _inventorySlots = temp;
        for (int i = 0; i < Inventory.Inventory.InventoryObject.Length; i++)
        {
            if (_inventorySlots[i] == null)
            {

                var obj = Instantiate(InterfaceSlotPrefab, Vector3.zero, Quaternion.identity, _scroller);

                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.Select, delegate { OnSelect(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
                var tempClick = obj.AddComponent<MyRightClickClass>();
                tempClick.SetParent(Type);



                _inventorySlots[i] = obj;
                Inventory.Inventory.InventoryObject[i] = new InventorySlot();
                Inventory.Inventory.InventoryObject[i].GameObjectParent = obj;
                InventoryUI.Add(obj, Inventory.Inventory.InventoryObject[i]);
                InventoryUI[obj].Parent = this;
                Inventory.Inventory.InventoryObject[i].OnAfterUpdate += OnSlotUpdate;
                Inventory.Inventory.InventoryObject[i].OnBeforeUpdate += OnBeforeUpdate;

            }

        }

        Inventory.AddItem(Inventory.tempItem, Inventory.tempAmount);

    }
    



}

