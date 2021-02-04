using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class craftItemDisplay : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public craftItem craftitem;
    public GameObject recipe;
    public inventory inventory;
    public Transform playerLocation;
    Transform craftLocation;
    public Material craftableColor;
    public Material notCraftableColor;
    public List<bool> itemsCheck = new List<bool>();

    private void Start()
    {
        for (int i = 0; i < craftitem.amounts.Count; i++)
        {
            itemsCheck.Add(false);
        }

        craftLocation = playerLocation.GetChild(2);
    }

    private void Update()
    {

        for (int i = 0; i < craftitem.materials.Count; i++)// stick and stone
        {
            if (inventory.items.Contains(craftitem.materials[i]) == true)//material inventory de var mı?
            {
                int indexOfMaterial = inventory.items.IndexOf(craftitem.materials[i]);// olan materialin indexi
                if (inventory.nums[indexOfMaterial] >= craftitem.amounts[i])//indexteki materialin adeti istenilen kadar mı?
                {
                    itemsCheck[i] = true;//malzeme var ve yeteri kadar
                }
                else{
                    itemsCheck[i] = false;
                }
            }
            else
            {
                itemsCheck[i] = false;
            }
                
        }
        
        if (itemsCheck.Contains(false) == false)//malzemeler tam ise
        {
            gameObject.GetComponent<Image>().color = craftableColor.color;
        }
        else if(itemsCheck.Contains(false) == true)//malzemeler tam değil ise
        {
            gameObject.GetComponent<Image>().color = notCraftableColor.color;
        }

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //setactive recipe by its type
        //place it beside item
        recipe.SetActive(true);
        if(recipe.name == "RecipeType1")
        {
            recipe.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = craftitem.materials[0].artwork;
            recipe.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = craftitem.amounts[0].ToString();
        }else if(recipe.name == "RecipeType2")
        {
            recipe.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = craftitem.materials[0].artwork;
            recipe.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = craftitem.amounts[0].ToString();
            recipe.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = craftitem.materials[1].artwork;
            recipe.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = craftitem.amounts[1].ToString();
        }
        
        recipe.transform.position = new Vector3(recipe.transform.position.x, transform.position.y);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //make recipe invisiable
        recipe.SetActive(false);

    }
    public void makeItem()
    {
        
        if(itemsCheck.Contains(false) == false)
        {
            useResources();
            createItem();
            resetItemCheck();
        }
        else
        {
            Debug.Log("you dont have enough resource to craft this item");
        }
    }

    private void createItem()
    {
       Instantiate(craftitem.crafteditem,new Vector3(craftLocation.position.x,craftitem.crafteditem.transform.position.y, craftLocation.position.z), craftitem.crafteditem.transform.rotation);
       craftitem.crafteditem.GetComponent<itemDisplay>().item.durability = 100;
    }
    private void useResources()
    {
        for (int i = 0; i < craftitem.materials.Count; i++)
        {
            for (int j = 0; j < craftitem.amounts[i]; j++)
            {
                inventory.findAndDeleteItem(inventory.items.IndexOf(craftitem.materials[i]));
            }
            
        }
        
    }
    private void resetItemCheck()
    {
        for (int i = 0; i < itemsCheck.Count; i++)
        {
            itemsCheck[i] = false;
        }
    }
}