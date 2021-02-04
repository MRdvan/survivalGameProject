using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class crafting : MonoBehaviour
{

    public List<GameObject> crafts = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < crafts.Count; i++)
        {
            craftItem craftItem=crafts[i].GetComponent<craftItemDisplay>().craftitem;
            crafts[i].transform.GetChild(1).GetComponent<Text>().text = craftItem.newItemAmount.ToString();
            crafts[i].transform.GetChild(0).GetComponent<Image>().sprite = craftItem.artwork;
            crafts[i].transform.GetChild(2).GetComponent<Text>().text = craftItem.name;
        }
    }

}
