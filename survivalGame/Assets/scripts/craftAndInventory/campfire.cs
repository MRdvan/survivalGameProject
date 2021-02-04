using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class campfire : MonoBehaviour
{
    Text message {get { return FindObjectOfType<Canvas>().transform.GetChild(8).GetComponent<Text>(); }}
    GameObject campfireInfo { get { return FindObjectOfType<Canvas>().transform.GetChild(7).gameObject; } }
    GameObject fire { get { return transform.GetChild(0).gameObject; } }
    GameObject fireLight { get { return transform.GetChild(1).gameObject; } }

    bool isPlayerHere;
    bool isItBurning;


    //[SerializeField] private float FirePower;
    //public float firePower;
    public List<GameObject> fuels = new List<GameObject>();
    [SerializeField] private float burnRate = 0.5f;
    [SerializeField] private float FuelPower;
    public float fuelPower;
    Text fuelText;

    
    public List<GameObject> foods = new List<GameObject>();
    [SerializeField] private float cookRate = 0.5f;
    [SerializeField] private float CookingTime;
    public Item cookedMeatItem;
    public Material cookedMeatColor;
    public float cookingTime;
    Text cookText;



    // Start is called before the first frame update
    void Start()
    {
        
        FuelPower = 20;
        CookingTime = 30;
        isItBurning = false;
        fuelText = campfireInfo.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        cookText = campfireInfo.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
    }

    private void Awake()
    {
    }

    private void OnTriggerStay(Collider other)//yanında durduğu sürece
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fuelText.text = fuels.Count.ToString();
            cookText.text = foods.Count.ToString();
            //isPlayerHere = true;
            //message.gameObject.SetActive(true);
            //campfireInfo.SetActive(true);
            if (isItBurning)
            {
                message.text = "Press F to extinguish!";
            }
            else
            {
                message.text = "Press F to light it!";
            }
        }
    }
    private void OnTriggerEnter(Collider other)//yanına gelince
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player beside campfire!");
            isPlayerHere = true;
            message.gameObject.SetActive(true);
            campfireInfo.SetActive(true);
        }
        
    }
    private void OnTriggerExit(Collider other)//yanından gidince
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerHere = false;
            message.gameObject.SetActive(false);
            campfireInfo.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fire.GetComponent<ParticleSystem>().isPlaying)
            isItBurning = true;
        else
            isItBurning = false;



        if (Input.GetKeyDown(KeyCode.F) && isPlayerHere)//yakıp söndürme işleri
        {
            if (isItBurning)
            {
                extinguish();
            }
            else
            {
                burn();
            }
            
        }


        if (fuels.Count > 0)//yakıt varsa
        {
            if (isItBurning)//FIRE IS BURNING !!!
            {
                if (foods.Count > 0)
                {
                    cooking();
                }
                
                fuelPower -= burnRate * Time.deltaTime;//yakıtı zamanla yak
                
                if (fuelPower <= 0)
                {
                    fuels.RemoveAt(fuels.Count-1);// bitince yok et
                    fuelPower = 20;
                   
                }

            }

        }
        else// yakıt bittiyse söndür
        {
            isItBurning = false;
            extinguish();
        }

      
        


    }

    public void cooking()
    {
        cookingTime -= cookRate * Time.deltaTime; //yemeği zamanla pişir

        if (cookingTime <= 0)
        {
            
            //yemeği pişmiş şekilde çıkar
            GameObject cookedMeat = Instantiate(foods[foods.Count - 1].gameObject, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity) as GameObject;
            foods.RemoveAt(foods.Count - 1);//pişince ateşin içinden çıkar

            cookedMeat.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1)) * 5, ForceMode.Impulse);
            cookedMeat.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = cookedMeatColor;//renk
            cookedMeat.GetComponent<itemDisplay>().item = cookedMeatItem;

            cookingTime = 30;
        }
    }

    private void extinguish()
    {
        fire.GetComponent<ParticleSystem>().Stop();//söndür
        fireLight.SetActive(false);
        FuelPower = fuelPower;
        CookingTime = cookingTime;
    }

    private void burn()
    {
        fire.GetComponent<ParticleSystem>().Play();//yak
        fireLight.SetActive(true);
        fuelPower = FuelPower;
        cookingTime = CookingTime;
    }



}
