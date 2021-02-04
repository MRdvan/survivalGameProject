using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] internal PlayerController player;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //sets moving animation by speed
        animator.SetBool("isMoving", player.agent.velocity.magnitude > 0.1f);
        //sets running animation by input
        animator.SetBool("isRunning", player.playerInputs.runInput());
        animator.SetBool("isAttacking", player.currentState == PlayerController.states.attack);
        


        //switch (player.playerInputs.dodging())
        //{
        //    case PlayerInputs.dodge.forward:
        //        animator.SetTrigger("dodge_F");
        //        player.currentState = player.idleState;
        //        break;
        //    case PlayerInputs.dodge.left:
        //        animator.SetTrigger("dodge_L");
        //        player.currentState = player.idleState;
        //        break;
        //    case PlayerInputs.dodge.back:
        //        animator.SetTrigger("dodge_B");
        //        player.currentState = player.idleState;
        //        break;
        //    case PlayerInputs.dodge.right:
        //        animator.SetTrigger("dodge_R");
        //        player.currentState = player.idleState;
        //        break;
        //    case PlayerInputs.dodge.non:
        //        break;
        //    default:
        //        break;
        //}
    }
    

    internal void GatherAnim()
    {
        animator.SetTrigger("gather");
    }

    internal void ToolAnim(string animName)
    {
        animator.SetTrigger(animName); 
    }

    internal void WeaponAnim(string animName)
    {
        animator.SetTrigger(animName);
    }

    internal void PutWeaponAnim()
    {
        animator.SetTrigger("putBack");
    }

    internal void GetWeaponAnim()
    {
        animator.SetTrigger("getBack");
    }
}
