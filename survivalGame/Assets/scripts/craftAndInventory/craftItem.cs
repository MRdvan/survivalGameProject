using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "craft item", menuName = "new craft item")]
public class craftItem : ScriptableObject
{
   
    //recipe for crafting items

    public new string name;
    public Sprite artwork;
    public int newItemAmount;
    public List<Item> materials = new List<Item>();
    public List<int> amounts = new List<int>();
    public GameObject crafteditem;

}
