using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float playerSpeed = 2f;
    Vector3 velocity;
    public float gravity = -19.81f;
    public float jumpHeight = 3f;

    public float turnSmoothTime = 0.1f;
    float smoothVelocity;
    public Transform cam;

    //private bool idle;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator animator;
    public float speed = 0f;
    public float _direction = 0f;
    public float _velocity = 0f;
    public KeyCode sprintKeyboard = KeyCode.Space;
    public bool isSprinting = false;




    // Start is called before the first frame update
    void Start()
    {
        //idle = true;

    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }

        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        speed = Mathf.Abs(hor) + Mathf.Abs(ver);


        speed = Mathf.Clamp(speed, 0f, 1f);
        speed = Mathf.SmoothDamp(animator.GetFloat("Speed"), speed, ref _velocity, 0.1f);
        animator.SetFloat("Speed", speed);

        if (ver < 0f)
            _direction = ver;
        else
            _direction = 0f;

        animator.SetFloat("Direction", _direction);

        isSprinting = (Input.GetKey(sprintKeyboard) && _direction >= 0f);
        animator.SetBool("isSprinting", isSprinting);

       
        Vector3 direction = new Vector3(hor, 0f, ver).normalized;
        if(direction.magnitude >= 0.1f)
        {
            //idle = false;//moving
            //animator.SetBool("isWalking",true);
            //rotating player to direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) *Mathf.Rad2Deg + cam.eulerAngles.y ;// gives us the angle
            //smoothing the rotation
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);//using that angle to rotate

            //move player 
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
            
        }
        
        else
        {
            //idle = true;//not moving
            //animator.SetBool("isWalking", false);

        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
    private void LateUpdate()
    {
        

    }


}
