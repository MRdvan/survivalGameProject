using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] internal PlayerController player;
    [SerializeField] internal Transform target;
    [SerializeField] private Camera cam;
    private RaycastHit hit;

    private void FixedUpdate()
    {

        //switch (player.playerInputs.dodging())
        //{
        //    case PlayerInputs.dodge.forward:
        //        player.agent.ResetPath();
        //        player.currentState = player.idleState;
        //        break;
        //    case PlayerInputs.dodge.left:
        //        player.agent.ResetPath();
        //        player.currentState = player.idleState;
        //        break;
        //    case PlayerInputs.dodge.back:
        //        player.agent.ResetPath();
        //        player.currentState = player.idleState;
        //        break;
        //    case PlayerInputs.dodge.right:
        //        player.agent.ResetPath();
        //        player.currentState = player.idleState;
        //        break;
        //    default:
        //        break;
        //}
    }
    private void Start()
    {
        target = null;
    }
    public void running()
    {
        if (player.playerInputs.runInput())
        {
            //energy reduce
            gameObject.GetComponent<playerStats>().ReduceBattery(0.1f);
            player.agent.speed = 11;
        }
        else
        {
            //stops reducing energy
            player.agent.speed = 4;
        }
    }
    public void moving()
    {
        if (player.playerInputs.mouseClick() && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (target == null)
                {
                    player.agent.SetDestination(hit.point);
                }
                if (hit.transform.CompareTag("ground"))
                {
                    target = null;
                }
                if (hit.transform.CompareTag("enemy"))
                {
                    target = hit.transform;
                }
                if (hit.transform.CompareTag("spaceShip"))
                {
                    target = hit.transform;
                }
                if (hit.transform.GetComponent<resource>() != null)
                {
                    target = hit.transform;
                }
            }

        }
    }
    public void chasing()
    {
        player.agent.SetDestination(target.position);
    }
    public void lockPlayerToTarget()
    {
        transform.LookAt(target.position);
    }
    public void unlockPlayer()
    {
        transform.LookAt(null);
    }
    public bool isMoving()
    {
        if (player.agent.velocity.magnitude < 0.1f)//idle
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
