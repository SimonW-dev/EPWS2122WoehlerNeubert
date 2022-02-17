using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
TODO Attack needs a Cooldown
You can Jump-cancel Attack
Attack hits instantly (maybe delay but its alright)
*/

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.1f; //testing this
    public int attackPower = 50;
    public float attackSpeed = 2f;
    float nextAttacktime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttacktime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttacktime = Time.time + 1f / attackSpeed;
            }
        }
    }

    void Attack()
    {
        //Play Attack Animation (we dont have one yet)
        animator.SetTrigger("Attack");

        //Detect enemies jumped on
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackPower);
            Debug.Log("We hit " + enemy.name);
        }
    }

    void OnDrawGizmosSelected() 
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
