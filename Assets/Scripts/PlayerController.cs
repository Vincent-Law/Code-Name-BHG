using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [Header("Character Attributes:")]
    private float MOVEMENT_BASE_SPEED = 5f;
    
    [Space]
    [Header("Character Statistics:")]
    public float movementSpeed;
    private Vector2 movement;
    private Vector3 mouseMovement;
    Vector2 lookDirection;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject bow;
    public GameObject player;
   
    public string playerName;
    private int health;

    void Awake() {
    }

    void Update() {

        Move();
        AnimateCharacter();

    }

    void Move()
    {
        movement = new Vector2(Input.GetAxis("MoveHorizontal"), Input.GetAxis("MoveVertical"));
        movementSpeed = Mathf.Clamp(movement.magnitude, 0.0f, 1.0f);
        movement.Normalize();

        if (Input.GetButton("Fire"))
        {
            rb.velocity = movement * movementSpeed * MOVEMENT_BASE_SPEED * 0.5f;
        }
        else
        {
            rb.velocity = movement * movementSpeed * MOVEMENT_BASE_SPEED;
        }

    }

    void AnimateCharacter() 
    {
        Vector3 mouseLocationPixels = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

        Vector3 mouseLocationWorld = Camera.main.ScreenToWorldPoint(mouseLocationPixels);

        lookDirection = (mouseLocationWorld - transform.position).normalized;

        //set which direction the animations should play in based on the direction the player is looking/direction of the mouse
        animator.SetFloat("Horizontal", lookDirection.x);
        animator.SetFloat("Vertical", lookDirection.y);

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(playerName + " takes " + damage + " damage ");
        if (health <=0)
        {
            Debug.Log(playerName + " has been defeated!");
        }
        else
        {
            Debug.Log(playerName + " has " + health + " health remaining.");
        }
    }
    
}
