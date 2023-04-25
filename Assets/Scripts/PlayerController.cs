using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Character Attributes:")]
    public float MOVEMENT_BASE_SPEED = 5f;

    [Space]
    [Header("Character Statistics:")]
    public float movementSpeed;
    public Vector2 movement;
    public Vector3 mouseMovement;
    public Vector3 aim;
    bool isAiming;
    bool endOfAiming;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;

    public GameObject crossHair;
    public GameObject arrowPrefab;
    public GameObject player;

    public bool useController;
    Vector3 playerPos;
    Vector2 mousePosition;
    Vector3 screenPoint;
    Vector2 offset;
    Vector2 lookDirection;

    void Awake() 
    {

    void Awake() 
    {

    }

    void Update() 
    {
        ProcessInputs();
        Move();
        Animate();
        
    }

    void ProcessInputs()
    {
        //playerPos = GameObject.Find("Player").transform.position;
        mousePosition = Input.mousePosition;
        screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        Debug.Log(screenPoint);
        mousePosition = Input.mousePosition;
        //screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
       
        //offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);

        crossHair.transform.position = screenPoint; // Vector3.Lerp(crossHair.transform.position, screenPoint, 1  * Time.deltaTime);
        aim = offset;
            
        isAiming = Input.GetButton("Fire");
        endOfAiming = Input.GetButton("Fire1");
       
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        if(Input.GetButtonUp("Fire")) {
                //add rigibody2D component to arrow prefab
                GameObject arrow = Instantiate(arrowPrefab, rb.position + Vector2.up * 0.20f, Quaternion.identity); //Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            
                //set gravity scale to 0
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * .05f;
                //provides shooting angle based on crosshair placement
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                //logic to add for arrow stoping when near the end of its magnitude
            //add rigibody2D component to arrow prefab
            GameObject arrow = Instantiate(arrowPrefab, rb.position + Vector2.up * 0.20f, Quaternion.identity); //Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            
            //set gravity scale to 0
            arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * .05f;
            //provides shooting angle based on crosshair placement
            arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            //logic to add for arrow stoping when near the end of its magnitude
        }
    }


    void Move() {
        movement = new Vector2(Input.GetAxis("MoveHorizontal"), Input.GetAxis("MoveVertical"));
        movementSpeed = Mathf.Clamp(movement.magnitude, 0.0f, 1.0f);
        movement.Normalize();
        
        if (Input.GetButtonDown("Fire"))
        {
          rb.velocity = movement * movementSpeed * MOVEMENT_BASE_SPEED;
        }
        else
        {
         rb.velocity = movement * movementSpeed * MOVEMENT_BASE_SPEED;
        }       
    }

    void Animate() {
        Vector3 mouseLocationPixels = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

        Vector3 mouseLocationWorld = Camera.main.ScreenToWorldPoint(mouseLocationPixels);

        lookDirection = (mouseLocationWorld - transform.position).normalized;

        //set which direction the animations should play in based on the direction the player is looking/direction of the mouse
        animator.SetFloat("Horizontal", lookDirection.x);
        animator.SetFloat("Vertical", lookDirection.y);
        //leave speed calculated by movement vector since we don't want to switch off idle animation unless we are actually moving

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    
}
