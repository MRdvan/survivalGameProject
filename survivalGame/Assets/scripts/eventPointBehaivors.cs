using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventPointBehaivors : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pointer;

    private void Start()
    {
        pointer.SetActive(false);
    }

    private void OnMouseEnter()
    {
        pointer.SetActive(true);
    }


    public void OnMouseExit()//exits
    {
        pointer.SetActive(false);
        pointer.GetComponent<Renderer>().material.color = Color.green;
    }

    public void OnMouseDown()//pressed
    {
        pointer.GetComponent<Renderer>().material.color = Color.red;
    }

}
