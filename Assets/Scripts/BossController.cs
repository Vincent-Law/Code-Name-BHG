using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private float maxHealth = 100f;
    private float currentHealth;
    private float moveSpeed = 5f;
    private float attackRange = 2f;
    private float followRange = 10f;
    private int attackDamage = 10;
    private float attackCooldown = 2f;

    private bool isAttacking = false;
    private bool canAttack = true;
    private Transform player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (Vector2.Distance(transform.position, player.position) < followRange && player != null)
        {
            //move towards player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.fixedDeltaTime);
        }

        if (!isAttacking && player != null)
        {
            //check if within attack range
            if (Vector2.Distance(transform.position, player.position) < attackRange)
            {
                StartCoroutine(Attack());
            }
        }

        //set animation parameters

    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        //stop moving
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //select a random attack
        int attackIndex = Random.Range(1, 4);

        //execute corresponding animation and behavior
        switch (attackIndex)
        {
            case 1:
                animator.SetBool("isAttacking", true);
                yield return new WaitForSeconds(attackCooldown);
                break;
            case 2:
                animator.SetBool("useSkill", true);
                yield return new WaitForSeconds(attackCooldown);
                break;
            case 3:
                animator.SetBool("summon", true);
                yield return new WaitForSeconds(attackCooldown);
                break;
        }

        if (canAttack && player != null)
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

        animator.SetBool("isAttacking", false);
        animator.SetBool("useSkill", false);
        animator.SetBool("summon", false);
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
        animator.SetTrigger("fuckingDies");
        //destroy boss
        Destroy(gameObject);
    }
}
