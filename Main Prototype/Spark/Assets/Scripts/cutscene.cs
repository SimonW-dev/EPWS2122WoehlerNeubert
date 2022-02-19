using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutscene : MonoBehaviour
{
    public Collider2D box;
    public GameObject player;
    public GameObject dialogue;

    
    public Animator animDark;
    public Animator animSpriteLeft;
    public Animator animSpriteRight;
    public Animator animBox;
    

    void OnTriggerEnter2D(Collider2D enter)
    {
        SetAnimOn();

        //remove collider
        Destroy(box);

        player.GetComponent<PlayerMovement>().enabled=false;
    }

    void Update()
    {
        bool isDone = dialogue.GetComponent<InkDialogue>().isDone;
        if(isDone)
        {
            //Input.GetMouseButtonDown(0)
            SetAnimOff();
            player.GetComponent<PlayerMovement>().enabled=true;
            this.enabled=false;
        }

    }

    /*
    void SetAnimOn() 
    {
        canvas.GetComponentInChildren<Animator>().SetBool("isOn", true);
    }
    */
    
    void SetAnimOn()
    {
        animDark.SetBool("IsOn", true);
        animSpriteLeft.SetBool("IsOn", true);
        animSpriteRight.SetBool("IsOn", true);
        animBox.SetBool("IsOn", true);
    }

    void SetAnimOff()
    {
        animDark.SetBool("IsOn", false);
        animSpriteLeft.SetBool("IsOn", false);
        animSpriteRight.SetBool("IsOn", false);
        animBox.SetBool("IsOn", false);
    }
}
