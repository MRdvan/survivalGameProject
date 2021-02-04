using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[CreateAssetMenu(fileName ="item",menuName ="new item")]
public class Item : ScriptableObject
{
    public enum Type
    {
        Consumable,
        Tool,
        Material,
        Furniture,
        Weapon,
    };

    public new string name;
    public Sprite artwork;
    public int foodValue;
    public int waterValue;
    public int energyValue;
    public int armorValue;
    public int attackValue;
    public bool istorchable;
    public int amount = 1;
    public Type itemType;
    public float durability;
    public GameObject itemobject;



    

}
