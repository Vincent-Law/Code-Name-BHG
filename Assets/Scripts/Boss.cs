using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 2f;

    private bool isAttacking = false;
    private bool canAttack = true;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            //move towards player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            //check if within attack range
            if (Vector2.Distance(transform.position, player.position) < attackRange)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        //stop moving
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //attack animation


        if (canAttack)
        {
            //deal damage to player
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);

            //set attack cooldown
            canAttack = false;
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }

        //resume movement
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //check if dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die() 
    {
        //death animation

        //destroy boss
        Destroy(gameObject);
    }
}
