using UnityEngine;


/// <summary>
/// item object component, used for item objects in the game world.
/// </summary>
public class ItemObject : MonoBehaviour
{
    [SerializeField] private BaseItemScriptableObject _item;

    private SpriteRenderer _spriteRenderer;

    public BaseItemScriptableObject Item { get { return _item; } set { _item = value; } }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _item.Sprite;
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
