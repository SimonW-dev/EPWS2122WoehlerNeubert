using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Start() variables (for initializing)
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    //Finite State Machine
    private enum State { idle, running, jumping, falling }
    private State state = State.idle;

    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 8;
    [SerializeField] private float jumpForce = 10;

    private void Start()
    {
        //init
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }


    // Update is called once per frame
    private void Update()
    {
        Movement();

        //tell the Animator in what state the player is
        VelocityState();
        anim.SetInteger("state", (int)state);

    }

    private void Movement()
    {
        //get Horizontal movement (via unity Input)
        float hDireciton = Input.GetAxis("Horizontal");

        //run left
        if (hDireciton < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        //run right
        else if (hDireciton > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        //Jump + check if touching ground layer
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
        }
    }

    private void VelocityState()
    {

        //Jumping
        if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        //Moving
        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        
        //Idle
        else
        {
            state = State.idle;
        }
    }
}