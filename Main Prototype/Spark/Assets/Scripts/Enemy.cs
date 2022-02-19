using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/* TODO
Enemies fall through the Ground after We disable their Collider
*/
public class Enemy : MonoBehaviour
{
    public AIPath aiPath;
    public Animator animator;
    [SerializeField] public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        //flip right direction
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else {
            animator.SetTrigger("Damage");
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        animator.SetTrigger("Dead");
        //disable enemy
        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
