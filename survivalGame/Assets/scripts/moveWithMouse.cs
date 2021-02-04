using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveWithMouse : MonoBehaviour
{
  
    Animator animator { get { return GetComponent<Animator>(); } }
    public NavMeshAgent agent { get { return GetComponent<NavMeshAgent>(); } }
    public Transform target;
    // Update is called once per frame

    private void Start()
    {
        //target = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("spaceShip"))
        {
            gameObject.GetComponent<playerStats>().IncreaseBattery(2f);
        }
    }

    void Update()
    {
       

        //if (agent.velocity.magnitude == 0 && target == null)
        //{
        //    animator.SetBool("isWalking", false);
        //}
        //else if(target == null)
        //{
        //    animator.SetBool("isWalking", true);
        //}
        
    }

    //public void chase()
    //{
    //    agent.SetDestination(target.position);
    //}
}
