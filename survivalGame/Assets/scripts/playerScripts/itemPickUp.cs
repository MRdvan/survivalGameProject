using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class itemPickUp : MonoBehaviour
{
    [SerializeField] internal PlayerController player;

    public Text Message;
    public inventory inventory;
    public GameObject inventoryUI;
    public GameObject craftUI;
    private Item touchedItem;

    // Start is called before the first frame update
    void Start()
    {
        
        //Message.gameObject.SetActive(false);
        //anim = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        if(player.playerCollisions.isTouching && player.playerInputs.PickInput())
        {
            StartCoroutine(pickUp(player.playerCollisions.touchedObj, 0.3f));
        }

        showOrHideObject(inventoryUI, KeyCode.I);
        showOrHideObject(craftUI, KeyCode.C);
    }

    private IEnumerator pickUp(GameObject touchedObj,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (touchedObj != null)//obj doluysa
        {
            player.agent.ResetPath();
            player.playerAnimations.GatherAnim();
            if (touchedObj.CompareTag("item"))
            {
                touchedItem = touchedObj.GetComponent<itemDisplay>().item;
                Destroy(touchedObj);
            }
            if (touchedObj.CompareTag("well"))
            {
                touchedItem = touchedObj.GetComponent<well>().PullWater().GetComponent<itemDisplay>().item;
            }
            if (inventory.items.Contains(touchedItem) == true)
            {
                additem(inventory.items.IndexOf(touchedItem));
            }
            else
            {
                addOnEmptySlot(inventory.items.IndexOf(null), touchedItem);
            }
        }
        Message.gameObject.SetActive(false);
    }
    public void addOnEmptySlot(int i,Item touchedItem)
    {
        inventory.items[i] = touchedItem;  // ekle
        inventory.nums[i] = touchedItem.amount;   //ekle
        inventory.slots[i].GetComponent<Image>().sprite = inventory.items[i].artwork;
        inventory.slots[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = inventory.nums[i].ToString();
    }
    public void additem(int i)
    {
        inventory.nums[i]++;
        inventory.slots[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = inventory.nums[i].ToString();
    }
    private void showOrHideObject(GameObject gameObject,KeyCode key)
    {
        if (player.playerInputs.ShowOrHideInputs(key))
        {
            if (gameObject.activeSelf == true)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}