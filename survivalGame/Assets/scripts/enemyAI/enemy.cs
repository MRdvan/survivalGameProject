using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemy : MonoBehaviour,Ihuntable
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject enemyWeapon;
    [SerializeField] private Animator animator;
     
    [SerializeField] private float attackRate = 3;
    [SerializeField] private float cooldown = 0;
    [SerializeField] private int health;
    public float distanceToPlayer;
    public float distanceToChild;
    public int Health { get{ return health; } set { health = value; } }

    [SerializeField] public Transform child;
    public Transform player;
    [SerializeField] private Transform target;

    [SerializeField]
    public enum state
    {
        chase,
        attack
    };
    public state currentState;
    private IEnumerator enumerator;
    public float waitTime;
    private bool playerHit;

    public NavMeshAgent agent { get { return GetComponent<NavMeshAgent>(); } }

    // Start is called before the first frame update
    void Start()
    {
        child = FindObjectOfType<child>().transform;
        player = FindObjectOfType<playerStats>().transform;
        health = 100;
        currentState = state.chase;
        target = child;
    }


    private void Update()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);
        distanceToChild =  Vector3.Distance(child.position, transform.position);

        if (currentState == state.chase)
        {
            animator.SetBool("attack", false);
            agent.SetDestination(target.position);
        }

        if (distanceToChild < attackRange)
        {
            agent.ResetPath();
            animator.SetBool("attack",true);
            currentState = state.attack;

            if (cooldown > attackRate)
            {
                cooldown = 0;
                attack();
            }
            else
            {
                cooldown += Time.deltaTime;

            }
        }
        else
        {
            currentState = state.chase;
        }
        
        if(target == player)
        {
            if (distanceToPlayer < attackRange)
            {
                agent.ResetPath();
                animator.SetBool("attack", true);
                currentState = state.attack;
                if (cooldown > attackRate)
                {
                    cooldown = 0;
                    attack();
                }
                else
                {
                    cooldown += Time.deltaTime;

                }
            }
            else if (distanceToPlayer > 15)
            {
                target = child;
            }
            else
            {
                currentState = state.chase;
            }
        }
        if (playerHit)
        {
            target = player;
        }
        if (distanceToChild < 7)
        {
            target = child;
            currentState = state.chase;
        }



    }

//    public void attackChild()
//    {
//        child.gameObject.GetComponent<child>().sleepMode.isOn = false; // childs wake up when enemy attacks
        
//        if (timer < cooldown)
//        {
//            timer += Time.deltaTime;
//        }
//        else if (timer >= cooldown)
//        {
//            animator.SetTrigger("enemyAttack");
//            enumerator = attack(waitTime);
//            StartCoroutine(enumerator);
//            timer = 0.0f;
//        }
//    Debug.Log("attacking to child..");

//}

//    public  void attackPlayer()
//    {
//        agent.ResetPath();
        
//        if (timer < cooldown)
//        {
//            timer += Time.deltaTime;
//        }
//        else if (timer >= cooldown)
//        {
//            animator.SetTrigger("enemyAttack");
//            Debug.Log("before func");
//            enumerator = attack(waitTime);
//            StartCoroutine(enumerator);
           
//            timer = 0.0f;
//        }


//        float dis = Vector3.Distance(player.position, transform.position);
//        animator.SetFloat("distanceToPlayer", dis);
//        //attack to player
//    }

//    public void chargeChild()
//    {
      
        
//        float dis = Vector3.Distance(child.position, transform.position);
//        animator.SetFloat("distanceToChild", dis);
//        agent.SetDestination(child.position);
        
//    }

//    public void chasePlayer()
//    {
//        //agent.speed = chargeSpeed;
        
//        float dis2player = Vector3.Distance(player.position, transform.position);
//        animator.SetFloat("distanceToPlayer", dis2player);
//        float dis2child = Vector3.Distance(child.position, transform.position);
//        animator.SetFloat("distanceToChild", dis2child);
//        agent.SetDestination(player.position);
//    }

    public void attack()
    {
        if (target.GetComponent<child>() != null)
        {
            animator.SetTrigger("hit");
            target.GetComponent<child>().TakeDamage(enemyWeapon.GetComponent<itemDisplay>().item.attackValue);
            //col.gameObject.GetComponent<child>().isSleeping = false; // enemy awakes child
        }
        if (target.GetComponent<playerStats>() != null)
        {
            animator.SetTrigger("hit");
            target.GetComponent<playerStats>().TakeDamage(enemyWeapon.GetComponent<itemDisplay>().item.attackValue);
        }
    }
    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void TakeDamage(int damage)
    {
        //enemy hit animation
        animator.SetTrigger("getHit");
        playerHit = true;
        health -= damage;
        if (health < 0)
        {
            Die();
        }
    }

    public void SpawnResources()
    {
        // spawn meat and letter on animal position
    }

    public void Die()
   {
        //enemy die animation
        animator.SetTrigger("death");
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject,5f);
   }

    


}
