using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// simple component that handles item spawning when pressing the O button. 
/// </summary>
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private ExistingItemsScriptableObject _itemList;
    [SerializeField] private ItemObject _itemPrefab;
    [SerializeField] private Transform _spawnLocation;

    private PlayerControls _playerInput;


    private void Awake()
    {
        _playerInput = new PlayerControls();

        _playerInput.UserInterface.SpawnItem.started += SpawnItem;
    }



    private void OnEnable()
    {
        _playerInput.UserInterface.Enable();
    }

    private void OnDisable()
    {
        _playerInput.UserInterface.Disable();
    }

    private void SpawnItem(InputAction.CallbackContext context)
    {
        _itemPrefab.Item = _itemList.GetItem[Random.Range(0, _itemList.GetItem.Count)];
        GameObject temp = Instantiate(_itemPrefab.gameObject, _spawnLocation.position, Quaternion.identity, null);
    }

}
