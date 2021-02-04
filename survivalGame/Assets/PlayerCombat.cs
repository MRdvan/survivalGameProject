using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] internal PlayerController player;
    [SerializeField] private Transform target;

    private int comboCount;
    private float toolDurabilityDropValue = 1f;
    private float nextAttackTime = 0f;
    private float attackRate = 1.5f;

    private GameObject playersTool;
    [SerializeField] private Transform playersHand;


    [SerializeField] private List<GameObject> tools = new List<GameObject>();
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private string[] toolTriggers = { "swing", "mine", "swing" };
    [SerializeField] private string[] weaponTriggers;
    internal string animName;

    private void Start()
    {
    }

    public bool CheckToolAndSetAnimation()//checks for tool is exist and sets the animation for that
    {
        if (playersHand.childCount == 1)
        {
            playersTool = playersHand.GetChild(0).gameObject;
           
            if (tools.Contains(playersTool.GetComponent<itemDisplay>().item.itemobject))
            {
                animName = toolTriggers[tools.IndexOf(playersTool.GetComponent<itemDisplay>().item.itemobject)]; // setting animation trigger
                return true;
            }
            else if (weapons.Contains(playersTool.GetComponent<itemDisplay>().item.itemobject))
            {
                return true;
            }
        }
        else
        {
            Debug.Log("need tool !");
            return false;
        }
        return false;

    }

    public bool canAttack()//checks for is it right tool for right target
    {
        target = player.playerMovement.target;

        if (CheckToolAndSetAnimation() && target != null)
        {
            switch (playersTool.GetComponent<itemDisplay>().item.name) // check tool
            {
                case "axe":
                    if (target.CompareTag("tree") || target.CompareTag("enemy"))
                    {
                        return true;
                    }
                    break;
                case "pickaxe":
                    if (target.CompareTag("rock") || target.CompareTag("enemy"))
                    {
                        return true;
                    }
                    break;
                case "sickle":
                    if (target.CompareTag("bush") || target.CompareTag("enemy"))
                    {
                        return true;
                    }
                    break;
                case "spear":
                    if (target.CompareTag("enemy"))
                    {
                        return true;
                    }
                    break;
                default:
                    return false;

            }
        }
        return false;

    }
    public void Attack()
    {
        target = player.playerMovement.target;

        if (target != null)
        {
            if (target.GetComponent<resource>() != null && target.GetComponent<resource>().Health > 0)
            {
                if (Time.time >= nextAttackTime)
                {
                    if (player.playerInputs.attackInput())
                    {
                        player.playerMovement.lockPlayerToTarget();
                        AttackResource();
                        nextAttackTime = Time.time + 1f / attackRate;
                    }
                    player.playerMovement.unlockPlayer();
                }
            }
            else if (target.GetComponent<enemy>() != null && target.GetComponent<enemy>().Health > 0)
            {
                if (Time.time >= nextAttackTime)
                {
                    if (player.playerInputs.attackInput())
                    {
                        player.playerMovement.lockPlayerToTarget();
                        AttackToEnemy();
                        nextAttackTime = Time.time + 1f / attackRate;
                    }
                    player.playerMovement.unlockPlayer();
                }
            }
            else
            {
                player.playerMovement.target = null;
                target = null;
            }
        }

    }//attacks to enemy or resource

    public void AttackResource()
    {
        
        player.playerAnimations.ToolAnim(animName);
        target.GetComponent<resource>().TakeDamage(playersTool.GetComponent<itemDisplay>().item.attackValue);//use the items damage
        GetComponent<playerStats>().ReduceBattery(4);
        dropDurability();
        
    }
    public void AttackToEnemy()
    {
        
        Vector3 dir = target.transform.position - transform.position + transform.up;

        if (comboCount >= 3)
        {
            comboCount = 0;
        }
        else
        {
            player.playerAnimations.WeaponAnim(weaponTriggers[comboCount]);//animation
            target.GetComponent<enemy>().TakeDamage(playersTool.GetComponent<itemDisplay>().item.attackValue);//use the items damage 
            //target.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
            GetComponent<playerStats>().ReduceBattery(4);//reducing battery
            dropDurability();//reducing weapon durability
            comboCount++;
        }
        

    }//melee attacks to enemy with combo

    public void dropDurability()// drop durability of tool everytime it hits
    {
        playersTool.GetComponent<itemDisplay>().item.durability -= toolDurabilityDropValue;
        if (playersTool.GetComponent<itemDisplay>().item.durability <= 0)
        {
            destroyTool(playersTool);
        }
    }
    private void destroyTool(GameObject tool)//destroy if durability is zero
    {
        Destroy(tool);
    }

    public void eventAttack()
    {
        Debug.Log("attacked by event");
    }
}

