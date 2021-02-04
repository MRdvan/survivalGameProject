
using System.Collections;
using UnityEngine;


public class playerStats : MonoBehaviour,Ikillable {

    [SerializeField] internal PlayerController controller;  

    //player attributes
    [Range(0.0f, 100.0f), SerializeField] private float health;
    [Range(0.0f, 100.0f), SerializeField] private float exp;
    [Range(100.0f, 0.0f), SerializeField] private float battery;

    public float Health { get => health; set { health = value; } }
    public float Exp { get => Exp; set { Exp = value; } }
    public float Battery { get => Battery; set { Battery = value; } }


    public StatsBar healthBar, batteryBar;

    public int level;

    void Start()
    {
		health = 100f;
		exp = 0.0f;
		level = 1;
        battery = 100f;
        healthBar.setMaxValue(health);
        batteryBar.setMaxValue(battery);
		
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.setValue(health);
        if (health < 0)
        {
            Die();
        }
    }
    public void  ReduceBattery(float dropDownValue)//reduce by time
    {
        battery -= dropDownValue*Time.deltaTime;
        batteryBar.setValue(battery);
        if (battery < 0)
        {
            battery = 0;
            Die();
        }
    }
    public void IncreaseBattery(float increaseValue)
    {
        battery += increaseValue * Time.deltaTime;
        batteryBar.setValue(battery);
        if (battery >= 100)
        {
            battery = 100;
        }
    }
    public void ReduceBattery(int solidValue)//reduce immidiate
    {
        battery -= solidValue;
        batteryBar.setValue(battery);
        if (battery <= 0)
        {
            battery = 0;
            Die();
        }
    }

    public void GetExp()
    {

    }
    

    public void Die()
    {
        //die animation
        // die sound and effect
        // end the game
        Debug.Log("player dead! ");
    }

}
