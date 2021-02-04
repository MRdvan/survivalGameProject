using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spear : MonoBehaviour
{
    Rigidbody rb { get { return GetComponent<Rigidbody>(); } }
    bool isItStuck = false;

    private void Update()
    {
        if (transform.parent == null)
        {
            isItStuck = false;
        }
    }

    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.name);  
        if (isItStuck == false)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                collision.gameObject.GetComponent<enemy>().TakeDamage(GetComponent<itemDisplay>().item.attackValue);
                rb.constraints = RigidbodyConstraints.FreezeAll;
                transform.parent = collision.transform;
                isItStuck = true;
            }
            else if (collision.gameObject.CompareTag("animal"))
            {
                collision.gameObject.GetComponent<animalBehaviours>().TakeDamage(GetComponent<itemDisplay>().item.attackValue * 3);
                rb.constraints = RigidbodyConstraints.FreezeAll;
                transform.parent = collision.transform;
                isItStuck = true;
                //make it child of animal
                //spawn some blood particals
            }
            else if (collision.gameObject.CompareTag("tree"))
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                isItStuck = true;

            }
            else if (collision.gameObject.CompareTag("rock"))
            {
                //spawn some particals and destroy spear
            }
            else if (collision.gameObject.CompareTag("bush"))
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                isItStuck = true;
            }
            else if (collision.gameObject.CompareTag("ground"))
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                isItStuck = true;
            }
        }
    }

    
}
