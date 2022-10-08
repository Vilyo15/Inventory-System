using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private BaseItemScriptableObject _item;
    private SpriteRenderer _spriteRenderer;

    public BaseItemScriptableObject Item { get { return _item; } set { _item = value; } }
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _item.Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
