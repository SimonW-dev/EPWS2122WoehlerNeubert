using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    private enum State {idle, running, jumping, falling, crouching}
    private State state = State.idle;
    public Rigidbody2D rb;
    

    //basic movement variables
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    [SerializeField] public float runSpeed = 40f;


    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        

        //Jump on Button press
        if (Input.GetButtonDown("Jump")) 
        {
            jump = true;
            animator.SetBool("Jump", true);
        }

        //Crouch as long as Button is held down
        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetBool("Crouch", true);
        } 
        else if (Input.GetButtonUp("Crouch"))
        {
            animator.SetBool("Crouch", false);
        }
        
        Debug.Log(rb.velocity.y);
        if (rb.velocity.y < -0.1)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Falling", true);
        } 
    }

    //FixedUpdate has the frequency of the physics system; it is called every fixed frame-rate frame
    void FixedUpdate() 
    {
        //Move character
        //Time.fixedDeltaTime: The interval in seconds at which physics and other fixed frame rate updates
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump); 

        jump = false;
    }
    
    public void OnLanding()
    {
        state = State.idle;
        animator.SetBool("Falling", false);
    }
    
}

