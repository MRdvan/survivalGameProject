using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.WSA;
using Random = UnityEngine.Random;

public class animalBehaviours : MonoBehaviour,Ihuntable
{
    public animalProperties animal;
    public Transform[] waypoints;

    Animator animator { get { return GetComponent<Animator>(); } }
    NavMeshAgent agent { get { return GetComponent<NavMeshAgent>(); } }
    Transform player { get { return FindObjectOfType<playerStats>().transform; } }

    void Start()
    {

        animal.positions = new Vector3[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++)
        {
            animal.positions[i] = waypoints[i].position;
        }
    }

    void Update()
    {
        float dis2waypoint = Vector3.Distance(transform.position, animal.positions[animal.index]);
        float dis2player = Vector3.Distance(transform.position, player.position);
        animator.SetFloat("distanceWaypoint", dis2waypoint);
        animator.SetFloat("distancePlayer", dis2player);
    }

    internal void fleeFromPlayer()
    {
        float dis2player = Vector3.Distance(transform.position, player.position);
        animator.SetFloat("distancePlayer", dis2player);
        if (dis2player < animal.fleeDistance)
        {
            Vector3 dir = transform.position - player.position;
            agent.speed = animal.fleeSpeed;
            Debug.Log("hız:" + agent.speed);

            if (dis2player < animal.runDistance)
            {
                agent.speed = animal.runSpeed;
                Debug.Log("hız:" + agent.speed);
            }


            Vector3 newPos = transform.position + dir;
            agent.SetDestination(newPos);
        }
    }
    internal void patrol()
    {
        agent.speed = animal.patrolSpeed;
    }
    internal void findNewWaypoint()
    {
        animal.index = Random.Range(0, waypoints.Length);
        agent.SetDestination(animal.positions[animal.index]);
    }
    public  void SpawnResources()
    {
        // spawn meat and letter on animal position
    }
    public  void TakeDamage(int damage)
    {
        animal.health -= damage;
        if (animal.health < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        //animal is dead
        for (int i = 0; i < animal.meats.Count; i++)
        {
            Instantiate(animal.meats[i], new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2),transform.position.y,Random.Range(transform.position.z - 2, transform.position.z + 2)), Quaternion.identity);
        }
        Destroy(gameObject);
        //spawn meat and letter
    }

}
