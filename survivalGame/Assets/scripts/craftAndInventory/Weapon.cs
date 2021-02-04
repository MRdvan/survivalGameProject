using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Weapon : MonoBehaviour
{
    

    public Camera cam;
    public Transform mousePos;


    [SerializeField] [Range(0,100)]private float throwPower;
    [SerializeField] private float PowerIncreaseRate = 5;
    GameObject player{ get { return FindObjectOfType<playerStats>().gameObject; } }
    GameObject weapon;
    Rigidbody rb;
    Quaternion angle;
    Vector3 aimPointPos;
    bool isAiming = false;
    bool playerHasWeapon = false;
    public GameObject crosshair;
    public Transform playersHand;
    Animator animator { get { return player.GetComponent<Animator>(); } }



    private void Start()
    {
        crosshair.SetActive(false);
    }

    private void Update()
    {
        if (playersHand.childCount == 1 && playersHand.GetComponentInChildren<itemDisplay>().item.itemType == Item.Type.Weapon)
        {
            playerHasWeapon = true;
            weapon = playersHand.GetChild(0).gameObject;
            rb = weapon.GetComponent<Rigidbody>();
            angle = weapon.transform.rotation;
        }
        

    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1) && playerHasWeapon == true)//Aiming and charging
        {

            crosshair.SetActive(true);
            isAiming = true;
            //Cursor.lockState = CursorLockMode.Locked; //look cursor

            weapon.transform.parent = transform;
            weapon.transform.position = transform.position;
            weapon.transform.rotation = transform.rotation;

            GetComponent<LookAtConstraint>().constraintActive = true;// aim to cursor

            throwPower += PowerIncreaseRate * Time.deltaTime;
            //aimPointPos += transform.forward/ 2 * Time.deltaTime;
            //transform.position -= transform.forward/2 * Time.deltaTime;

            Debug.Log("aiming");
        }


        if (Input.GetMouseButtonUp(1) && playerHasWeapon == true)//stop aiming
        {


            //transform.position += aimPointPos;
            //aimPointPos = new Vector3(0, 0, 0);
            isAiming = false;

            weapon.transform.parent = playersHand;
            weapon.transform.position = playersHand.position;
            weapon.transform.rotation = angle;
            GetComponent<LookAtConstraint>().constraintActive = false;
            Cursor.lockState = CursorLockMode.None;
            throwPower = 0.0f;
            crosshair.SetActive(false);
        }

        if (isAiming && Input.GetMouseButtonDown(0))//throw
        {
            throwSpear();
        }
        
        if (rb != null)
        {
            if (rb.velocity != Vector3.zero)
            {
                rb.rotation = Quaternion.LookRotation(rb.velocity.normalized);
            }
        }
        

    }

    private void throwSpear()
    {
        
        playerHasWeapon = false;
        //transform.position += aimPointPos;
        //aimPointPos = new Vector3(0, 0, 0);

        isAiming = false;
        weapon.transform.parent = null;
        
        rb.isKinematic = false;
        weapon.GetComponent<Collider>().isTrigger = false;
        rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
        Vector3.Slerp(weapon.transform.forward, rb.velocity.normalized, Time.deltaTime * 2);
        rb.ResetCenterOfMass();

        Cursor.lockState = CursorLockMode.None;
        throwPower = 0.0f;
        crosshair.SetActive(false);

    }


}
