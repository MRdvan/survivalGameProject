using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] internal PlayerController player;
    internal bool isTouching;
    public GameObject touchedObj;
    public bool isPlayerNearFire;


    private void OnTriggerEnter(Collider other)//when the enter collision
    {
        if (other.gameObject.CompareTag("item"))
        {
            isTouching = true;
        }
        else if (other.gameObject.CompareTag("well"))
        {
            touchedObj = other.gameObject;
            //Message.text = "Press E to pick up water";
            //Message.gameObject.SetActive(true);
            isTouching = true;
        }
        if (other.gameObject.CompareTag("campfire"))
        {

            isPlayerNearFire = true;
        }
 
    }

    private void OnTriggerStay(Collider other)//while collision in side
    {

        if (other.gameObject.CompareTag("spaceShip"))
        {
            GetComponent<playerStats>().IncreaseBattery(2f);
        }

        if (other.gameObject.CompareTag("item"))
        {
            touchedObj = other.gameObject;
            //Message.gameObject.SetActive(true);
            //Message.text = "Press E to pick up!";
            isTouching = true;
        }
        if (other.gameObject.CompareTag("campfire"))
        {
            touchedObj = other.gameObject;
            isPlayerNearFire = true;
        }
    }
    private void OnTriggerExit(Collider other)//when the player left collision
    {
        if (other.gameObject.CompareTag("item"))
        {
            //Message.gameObject.SetActive(false);
            isTouching = false;
        }
        if (other.gameObject.CompareTag("campfire"))
        {
            isPlayerNearFire = false;
        }
    }

}
