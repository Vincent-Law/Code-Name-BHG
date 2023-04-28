using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float moveSpeed = 5f;
    public float attackRange = 0.5f;
    public int attackDamage = 10;
    public float attackCooldown = 2f;

    private bool isAttacking = false;
    private bool canAttack = true;
    private Transform player;
    private Animator animator;

    // Array of attacks
    //public BossAttack[] attacks;
    //private int currentAttackIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
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

        //set animation parameters

    }

    IEnumerator Attack()
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
        animator.SetTrigger("fuckingDies");
        //destroy boss
        Destroy(gameObject);
    }

    public class BossAttack
    {
        public int damage;
        public float range;
        public float cooldown;
    }
}
