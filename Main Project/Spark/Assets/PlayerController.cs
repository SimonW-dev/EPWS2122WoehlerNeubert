using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        //init
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    private void Update()
    {
        //run left
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            anim.SetBool("running", true);
        }

        //run right
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            anim.SetBool("running", true);
        }

        //Idle
        else
        {
            anim.SetBool("running", false);
        }


        //Jump
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }

    }
}