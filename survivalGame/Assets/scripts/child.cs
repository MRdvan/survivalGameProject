using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class child : MonoBehaviour,Ikillable {

	//child attributes

	//encapsulated and ranged values
	[SerializeField] [Range(0.0f, 100.0f)] private float health;
	[SerializeField] [Range(0.0f, 100.0f)] private float hunger;
	[SerializeField] [Range(0.0f, 100.0f)] private float thirst;
	[SerializeField] [Range(0.0f, 100.0f)] private float energy;

    public float Health {
		get 
		{ 
			return health; 
		}
		set 
		{ 
			float clapedValue = Mathf.Clamp(value, 0, 100);
			health = clapedValue;		
		}
	}
	public float Hunger
	{
		get
		{
			return hunger;
		}
		set
		{
			float clapedValue = Mathf.Clamp(value, 0, 100);
			hunger = clapedValue;
		}
	}
	public float Thirst
	{
		get
		{
			return thirst;
		}
		set
		{
			float clapedValue = Mathf.Clamp(value, 0, 100);
			thirst = clapedValue;
		}
	}
	public float Energy
	{
		get
		{
			return energy;
		}
		set
		{
			float clapedValue = Mathf.Clamp(value, 0, 100);
			energy = clapedValue;
		}
	}

	private int armor;
	public bool isSleeping;
	public Toggle sleepMode;
	public bool isMomHere = false;
	public bool isChildAlive = true;

	public GameObject timeRef; //time manager referance to gameobject
	public TimeManager timeManager; //referance to time script
	private float energyDropDownValue;
	private float hungerDropDownValue;
	private float thirstDropDownValue;




	public GameObject childInfoPanel;
	public Text healthtext;
	public Text foodtext;
	public Text watertext;
	public Text energytext;
	public Text armortext;
	

	private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag("Player"))
		{
			childInfoPanel.SetActive(true);
			isMomHere = true;
		}



	}
    private void OnTriggerExit(Collider other)
    {

		if (other.gameObject.CompareTag("Player"))
		{
			childInfoPanel.SetActive(false);
			isMomHere = false;
		}
		
	}





    void Start () {

		
		timeManager = timeRef.GetComponent<TimeManager>();
		health = 100f;
		hunger = 100f;
		thirst = 100f;
		energy = 100f;
		armor = 0;
		isSleeping = false;
		energyDropDownValue = 0.10416f;
		hungerDropDownValue = 0.20832f;
		thirstDropDownValue = 0.41664f;
		

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(hunger<15)
        {
			health -= hungerDropDownValue*2 * Time.deltaTime;
			Debug.Log("child needs food");
		}
        if (thirst < 25)
        {
			health -= thirstDropDownValue*2 * Time.deltaTime;
			Debug.Log("child needs water");
		}
		if(energy < 10)
        {
			health -= energyDropDownValue*2 * Time.deltaTime;
			Debug.Log("child needs sleep");
		}


		if (isSleeping == true)//uyuyo enerji artıyo
        {
            if (hunger > 50 && thirst > 75 && energy > 30 && health < 100)
            {
				health += hungerDropDownValue * thirstDropDownValue * energyDropDownValue * Time.deltaTime;
            }
			energy += energyDropDownValue*2f*Time.deltaTime; //dinleniyor
			if(energy == 100.0f)
            {
				isSleeping = false;//uyanıyo
            }
		}
        else if (isSleeping ==false)//uyanık ise 
        {
			energy -= energyDropDownValue * Time.deltaTime; //yoruluyor
			hunger -= hungerDropDownValue * Time.deltaTime; //acıkıyor
			thirst -= thirstDropDownValue * Time.deltaTime; //susuyor
		}


		if(isMomHere == true)
        {
			healthtext.text = "%" + health.ToString();
			foodtext.text = "%" + hunger.ToString();
			watertext.text = "%" + thirst.ToString();
			energytext.text = "%" + energy.ToString();
			armortext.text = armor.ToString();
		}
		

	/*
		*8saat(normal) = 8dk(sleep time)
		*8saat(normal) * 2times = 8dk(hunger time) * 2times in a day
		*4saat(normal) * 4times = 4dk(thirst time) * 4times in a day	 
	*/
	}


	public void changeSleepMode()
    {
		if(timeManager.timeOfDay < 300 && energy > 25.0f)//sabah ve yorgun değilse
        {
			Debug.Log("child can't sleep now");
			sleepMode.isOn = false;
        }
        else
        {
			isSleeping = sleepMode.isOn;
		}

	}

	public void TakeDamage(int damage)
    {
		health -= damage;
        if (health < 0)
        {
			Die();
        }
    }

    public  void Die()
    {
		Debug.Log("child dead! game over! ");
		isChildAlive = false;
    }
}
