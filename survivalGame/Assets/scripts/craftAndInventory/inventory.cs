using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.AI;

public class inventory : MonoBehaviour
{
    itemPickUp itemPick;
    Item newItem;
    public Sprite emptySlot;
    public GameObject panel;
    public child child;
    [SerializeField]private PlayerController player;
    public Transform playersHand;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>(); 
    public List<int> nums = new List<int>();

    

    // Start is called before the first frame update
    void Start()
    {
        itemPick = player.GetComponent<itemPickUp>();
        for (int i = 0; i < 7; i++)
        {
            items.Add(null);

        }
        for (int i = 0; i < 7; i++)
        {
            nums.Add(0);

        }
    }
    #region onClickMethods

    public void findAndDeleteItem(int slotIndex)//cancel button 
    {

        if (nums[slotIndex] == 0 && items[slotIndex] == null)
        {
            return;
        }
        else if(nums[slotIndex] == 1 && items[slotIndex] != null)
        {
            items[slotIndex] = null;
            slots[slotIndex].GetComponent<Image>().sprite = emptySlot;
            nums[slotIndex]--;
            slots[slotIndex].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = nums[slotIndex].ToString();
            
        }
        else
        {
            nums[slotIndex]--;
            slots[slotIndex].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = nums[slotIndex].ToString();
        }
        
    }
    public void onItemUsed()//item button
    {
        GameObject usedObj = EventSystem.current.currentSelectedGameObject;

        if (slots.Contains(usedObj))
        {
            int i = slots.IndexOf(usedObj);

            if (playersHand.childCount > 0)//eli doluysa
            {
                
                if (items[i] == null)//boşa dokunursa
                {
                    player.agent.ResetPath();

                    player.playerAnimations.PutWeaponAnim();
                    StartCoroutine(putBackNew(i,0.5f));
                    
                }
                else if (items[i].name.Equals(playersHand.GetChild(0).GetComponent<itemDisplay>().item.name))//koyucağı yerdeki aynı eşyaysa
                {
                    player.agent.ResetPath();
                    player.playerAnimations.PutWeaponAnim();
                    StartCoroutine(putBackSame(i,0.5f));
                }
            }
            playersHand.transform.localRotation = Quaternion.Euler(0, 0, 0);

            if (items[i] != null)
            {
                if (items[i].itemType == Item.Type.Consumable)
                {
                    if (child.isMomHere)//if mom is near child +
                    {

                        Debug.Log("player used:" + items[i].name);
                        child.Hunger += items[i].foodValue;
                        child.Thirst += items[i].waterValue;
                        child.Energy += items[i].energyValue;
                        //child.armor += items[i].armorValue;
                        findAndDeleteItem(i);
                        //pass it to child object
                    }
                    else if (player.playerCollisions.isPlayerNearFire)// food is near campfire
                    {
                        if (items[i].istorchable == true)// if its burnable
                        {
                            player.playerCollisions.touchedObj.GetComponent<campfire>().foods.Add(items[i].itemobject); 
                            findAndDeleteItem(i);
                        }
                    }
                    else
                    {
                        Debug.Log("you are too far from your child to use item !");
                    }
                }
                else if (items[i].itemType == Item.Type.Tool)
                {
                    if (playersHand.childCount == 0)//player hand empty
                    {
                        Debug.Log("player clicked:" + items[i].name);
                        player.agent.ResetPath();
                        player.playerAnimations.GetWeaponAnim();
                        playersHand.transform.localRotation = Quaternion.Euler(-90, 0, 0);//player takes item in her hand
                        StartCoroutine(takeIt(i, 0.20f));

                    }
                }
                else if (items[i].itemType == Item.Type.Material)
                {
                    Debug.Log("player clicked:" + items[i].name);
                    //only can use for craft and fire resource
                    if (player.playerCollisions.isPlayerNearFire)// if player near fire
                    {
                        if (items[i].istorchable)// if its burnable
                        {
                            player.playerCollisions.touchedObj.GetComponent<campfire>().fuels.Add(items[i].itemobject);
                            findAndDeleteItem(i);
                        }
                    }

                }
                else if (items[i].itemType == Item.Type.Weapon)
                {
                    if (playersHand.childCount == 0)//player hand empty
                    {
                        player.agent.ResetPath();
                        player.playerAnimations.GetWeaponAnim();
                        playersHand.transform.localRotation = Quaternion.Euler(180, 0, 0);
                        StartCoroutine(takeIt(i, 0.20f));
                        
                    }
                }
            }
            
        }
        
      
        
    }

    private IEnumerator putBackNew(int i,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        itemPick.addOnEmptySlot(i, playersHand.GetChild(0).GetComponent<itemDisplay>().item);//eşyayı koy
        Destroy(playersHand.GetChild(0).gameObject);//elindekini yok et
    }

    private IEnumerator putBackSame(int i,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        itemPick.additem(i);// adeti artır
        Destroy(playersHand.GetChild(0).gameObject);//elinden yok et
    }

    private IEnumerator takeIt(int i,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject childObject = Instantiate(items[i].itemobject, playersHand.position, playersHand.rotation) as GameObject;
        childObject.transform.parent = playersHand;
        childObject.tag = "tool";
        findAndDeleteItem(i);
    }


    #endregion

    #region onHighLightedMethods
    public void showItemInfo(int index)
    {
       
        if(items[index] != null)
        {
            
            Debug.Log("enter");
            panel.SetActive(true);
            panel.transform.position = new Vector3(slots[index].transform.position.x, 207);
            panel.GetComponent<itemInventoryInfo>().itemNameText.text = items[index].name;
            panel.GetComponent<itemInventoryInfo>().foodtext.text = items[index].foodValue.ToString();
            panel.GetComponent<itemInventoryInfo>().watertext.text = items[index].waterValue.ToString();
            panel.GetComponent<itemInventoryInfo>().energytext.text = items[index].energyValue.ToString();
            panel.GetComponent<itemInventoryInfo>().armortext.text = items[index].armorValue.ToString();
            panel.GetComponent<itemInventoryInfo>().attacktext.text = items[index].attackValue.ToString();
            panel.GetComponent<itemInventoryInfo>().durabilitytext.text = items[index].durability.ToString();
        }

    }
    public void hideItemInfo(int index)
    {
        panel.SetActive(false);
    }


    #endregion


}
