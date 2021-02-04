using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


    public class PlayerController : MonoBehaviour
    {

    [SerializeField] internal playerStats playerStats;
    [SerializeField] internal PlayerCombat playerCombat;
    [SerializeField] internal PlayerInputs playerInputs;
    [SerializeField] internal PlayerCollisions playerCollisions;
    [SerializeField] internal PlayerMovement playerMovement;
    [SerializeField] internal PlayerAnimations playerAnimations;
    [SerializeField] internal itemPickUp itemPickUp;


    [SerializeField] private string stateName;
    //internal State currentState;
    public float distance;


    internal enum states
    {
        idle,
        move,
        run,
        chase,
        attack
    }

    //player states
    internal states currentState = new states();
    //public Idle idleState = new Idle();
    //public move moveState = new move();
    //public run runState = new run();
    //public chase chaseState = new chase();
    //public attack attackState = new attack();

    public NavMeshAgent agent;

    private void Awake()
    {
    /*currentState = idleState;*/// starts with idle state
        currentState = states.idle;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        stateName = currentState.ToString();

        playerMovement.moving();
        playerMovement.running();

        if (playerInputs.runInput())
        {
            currentState = states.run;
        }

        if (playerMovement.target != null)
        {
            distance = Vector3.Distance(transform.position, playerMovement.target.position);
            if (distance < 3f)
            {
                agent.ResetPath();
                if (playerCombat.canAttack())
                {
                    currentState = states.attack;
                    playerCombat.Attack();
                }
            }
            else
            {
                currentState = states.chase;
                playerMovement.chasing();
            }
        }
        else
        {
            distance = 10f;
            currentState = playerMovement.isMoving() ? states.move : states.idle;
        }

    }

    private void FixedUpdate()
        {
        
        /*currentState = currentState.doState(this);*///check and do players state 
        }


}
        

