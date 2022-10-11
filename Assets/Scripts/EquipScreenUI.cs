using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// equip screen interface type
/// </summary>
public class EquipScreenUI : UserInterfaceBase
{
    //array containing equipment slots, these are manually made in the editor and not generated like inventory
    [SerializeField] private GameObject[] _equipSlots;

    //reference to player base stats
    [SerializeField] private PlayerBaseScriptableObject _player;

    //getters and setters
    public GameObject[] EquipSlots { get { return _equipSlots; } }
    public PlayerBaseScriptableObject Player { get { return _player; } }


    private void Start()
    {
        onUpdate += UpdatePlayerAttributes;
        beforeUpdate += RemovePlayerAttributes;
        UpdatePlayerAttributes();
    }

    //adds events to equip slots and sets dictionaries
    protected override void PopulateInventory()
    {
        Type = InterfaceType.Equipment;
        Inventory = Instantiate(Inventory);
        for (int i = 0; i < Inventory.Inventory.InventoryObject.Length; i++)
        {

            var obj = _equipSlots[i];

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            var temp = obj.AddComponent<MyRightClickClass>();
            temp.SetParent(Type);

            Inventory.Inventory.InventoryObject[i].GameObjectParent = obj;
            InventoryUI.Add(obj, Inventory.Inventory.InventoryObject[i]);
            InventoryUI[obj].Parent = this;

        }


    }


    //adds values of buffs on items equiped to the player base stats 
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

                }
                else
                {

                }

            }
        }
    }
    //removes values of buffs on items unequiped from the player
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

                }
                else
                {

                }

            }
        }
    }


}
