using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryScreen;
    [SerializeField] private GameObject _equipmentScreen;
    [SerializeField] private GameObject _attributeScreen;
    [SerializeField] private GameObject _inventoryButton;
    [SerializeField] private GameObject _equipmentButton;
    [SerializeField] private GameObject _attributeButton;


    public InventoryUI InventoryScreen { get { return _inventoryScreen.GetComponent<InventoryUI>(); } }
    public EquipScreenUI EquipmentScreen { get { return _equipmentScreen.GetComponent<EquipScreenUI>(); } }
    private PlayerControls _playerInput;

    // Start is called before the first frame update
    void Awake()
    {
        _playerInput = new PlayerControls();

        _playerInput.UserInterface.InventoryScreen.started += OnIPress;
        
        _playerInput.UserInterface.EquipmentScreen.started += OnCPress;
        
        _playerInput.UserInterface.AttributeScreen.started += OnHPress;
        
    }

    private void OnHPress(InputAction.CallbackContext context)
    {
      
        if (!_attributeScreen.activeSelf)
        {
            AttributeScreenActivate(true);
        }
        else
        {
            AttributeScreenActivate(false);
        }
        
    }

    private void OnCPress(InputAction.CallbackContext context)
    {
        if (!_equipmentScreen.activeSelf)
        {
            EquipmentScreenActivate(true);
        }
        else
        {
            EquipmentScreenActivate(false);
        }
    }

    private void OnIPress(InputAction.CallbackContext context)
    {
        if (!_inventoryScreen.activeSelf)
        {
            InventoryScreenActivate(true);
        }
        else
        {
            InventoryScreenActivate(false);
        }
    }


    public void InventoryScreenActivate(bool state)
    {
        _inventoryScreen.SetActive(state);
        _inventoryButton.SetActive(!state);
    }

    public void EquipmentScreenActivate(bool state)
    {
        _equipmentScreen.SetActive(state);
        _equipmentButton.SetActive(!state);
    }

    public void AttributeScreenActivate(bool state)
    {
        _attributeScreen.SetActive(state);
        _attributeButton.SetActive(!state);
    }


    private void OnEnable()
    {
        _playerInput.UserInterface.Enable();
    }
    private void OnDisable()
    {
        _playerInput.UserInterface.Disable();
    }
}
