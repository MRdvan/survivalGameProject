using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resource : MonoBehaviour,Ikillable
{
    Rigidbody rb;
    [SerializeField] private int health;
    public int Health { get { return health; } set{ health = value; } }
    [SerializeField] private List<GameObject> resources = new List<GameObject>();
    private IEnumerator enumerator;
    [SerializeField]private float resourceWaitTime;


    private void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        //spawn woods and fruits
        if(rb != null)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            rb.isKinematic = false;
            rb.AddForce(Vector3.forward*2, ForceMode.Impulse);
        }
        enumerator = waitForSpawn(resourceWaitTime);
        StartCoroutine(enumerator);
    }
    protected IEnumerator waitForSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
        float xpos;
        float zpos;
        for (int i = 0; i < resources.Count; i++)
        {
            if (resources[i] != null)
            {
                xpos = Random.Range(transform.position.x - 2, transform.position.x + 2);
                zpos = Random.Range(transform.position.z - 2, transform.position.z + 2);
                GameObject resource = Instantiate(resources[i], new Vector3(xpos, 2, zpos), resources[i].transform.rotation) as GameObject;
                resource.GetComponent<Collider>().isTrigger = false;
                resource.AddComponent<Rigidbody>();
            }
        }
    }
}
