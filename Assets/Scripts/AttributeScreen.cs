using TMPro;
using UnityEngine;


/// <summary>
/// atribute screen, hosts attributes values.
/// </summary>
public class AttributeScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _attack;
    [SerializeField] private TextMeshProUGUI _defence;
    [SerializeField] private TextMeshProUGUI _speed;
    [SerializeField] private TextMeshProUGUI _dodge;
    [SerializeField] private PlayerBaseScriptableObject _player;

    private void Update()
    {
        _attack.text = "ATK:\n" + _player.Player.ActiveAttributes[Attributes.Attack].Value.ToString();
        _defence.text = "DEF:\n" + _player.Player.ActiveAttributes[Attributes.Defence].Value.ToString();
        _speed.text = "SPD:\n" + _player.Player.ActiveAttributes[Attributes.Speed].Value.ToString();
        _dodge.text = "DDG:\n" + _player.Player.ActiveAttributes[Attributes.Dodge].Value.ToString();
    }
}
