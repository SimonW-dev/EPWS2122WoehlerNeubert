using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnd : MonoBehaviour
{
    public Animator animDark;
    public Animator animSpriteLeft;
    public Animator animSpriteRight;
    public Animator animBox;
    public GameObject player;




    void OnTriggerEnter2D(Collider2D enter)
    {
        SetAnimOn();
        player.GetComponent<PlayerMovement>().enabled=false;
    }

    void SetAnimOn()
    {
        animDark.SetBool("IsOn", true);
        animSpriteLeft.SetBool("IsOn", true);
        animSpriteRight.SetBool("IsOn", true);
        animBox.SetBool("IsOn", true);
    }
}
