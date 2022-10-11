using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// scriptable object holding player stats and name
/// </summary>
[CreateAssetMenu(fileName = "PlayerBase", menuName = "Player/BaseAttributes")]
public class PlayerBaseScriptableObject : ScriptableObject
{
    public Player Player = new Player("Todd");



    public void Clear()
    {
        Player = new Player(Player.Name);
    }

    public void IncreaseAttribute(Attributes type, int value)
    {
        Player.ActiveAttributes[type].Value += value;

    }

    public void DecreaseAttribute(Attributes type, int value)
    {
        Player.ActiveAttributes[type].Value -= value;

    }
}


public class Player
{
    public string Name;
    public int Health;
    private Attribute Attack = new Attribute(Attributes.Attack, 0);
    private Attribute Defence = new Attribute(Attributes.Defence, 0);
    private Attribute Speed = new Attribute(Attributes.Speed, 0);
    private Attribute Dodge = new Attribute(Attributes.Dodge, 0);
    public Dictionary<Attributes, Attribute> ActiveAttributes = new Dictionary<Attributes, Attribute>();
    public Player(string name)
    {
        Name = name;
        Health = 20;
        ActiveAttributes.Add(Attributes.Attack, Attack);
        ActiveAttributes.Add(Attributes.Defence, Defence);
        ActiveAttributes.Add(Attributes.Speed, Speed);
        ActiveAttributes.Add(Attributes.Dodge, Dodge);
    }



}

public class Attribute
{
    public Attributes Type;
    public int Value;

    public Attribute(Attributes type, int value)
    {
        this.Type = type;
        this.Value = value;
    }
}
