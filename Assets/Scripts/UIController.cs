using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// listens to events and contains button functions for turning on/off user interface windows
/// </summary>
public class UIController : MonoBehaviour
{
    //serialized references
    [SerializeField] private GameObject _inventoryScreen;
    [SerializeField] private GameObject _equipmentScreen;
    [SerializeField] private GameObject _attributeScreen;
    [SerializeField] private GameObject _inventoryButton;
    [SerializeField] private GameObject _equipmentButton;
    [SerializeField] private GameObject _attributeButton;

    //input
    private PlayerControls _playerInput;

    // getters
    public InventoryUI InventoryScreen { get { return _inventoryScreen.GetComponent<InventoryUI>(); } }
    public EquipScreenUI EquipmentScreen { get { return _equipmentScreen.GetComponent<EquipScreenUI>(); } }


    // Start is called before the first frame update
    private void Awake()
    {
        _playerInput = new PlayerControls();

        _playerInput.UserInterface.InventoryScreen.started += OnIPress;

        _playerInput.UserInterface.EquipmentScreen.started += OnCPress;

        _playerInput.UserInterface.AttributeScreen.started += OnHPress;

        _playerInput.UserInterface.ExitGame.started += OnEscapePress;
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

    private void OnEscapePress(InputAction.CallbackContext context)
    {
        Application.Quit();
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
