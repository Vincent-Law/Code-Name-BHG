using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public bool useController;
    Vector2 mousePosition;
    Vector3 screenPoint;
    Vector2 offset;

    public Camera cam;



    void Awake() {
    }

    void Update() {

        ProcessInputs();
        Move();
        Animate();

    }
    void ProcessInputs(){

        if (useController) {
            aim = new Vector3(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"), 0.0f);
            aim.Normalize();
            isAiming = Input.GetButton("Fire");
            endOfAiming = Input.GetButtonUp("Fire");
        }       
        else {
            //mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
            //aim = aim + mouseMovement;
          // if (aim.magnitude> 10.0f)
         //  {
               //aim.Normalize();
         //  }
            //crossHair.transform.position = aim;
           // aim = crossHair.transform.position;
           
            mousePosition = Input.mousePosition;
            screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
            offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
            
            crossHair.transform.position = Vector3.Lerp(crossHair.transform.position, screenPoint, 1  * Time.deltaTime);
            aim = offset;
            
            isAiming = Input.GetButton("Fire");
            endOfAiming = Input.GetButton("Fire1");
        } 
        
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);
        if(Input.GetButtonDown("Fire")) {
                //add rigibody2D component to arrow prefab
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            //set gravity scale to 0
            arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * .05f;
                //provides shooting angle based on crosshair placement
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                //destroys arrow after set time
                Destroy(arrow, 2.0f);   
        
        }
    }


    void Move() {
        movement = new Vector2(Input.GetAxis("MoveHorizontal"), Input.GetAxis("MoveVertical"));
        movementSpeed = Mathf.Clamp(movement.magnitude, 0.0f, 1.0f);
        movement.Normalize();
        rb.velocity = movement * movementSpeed * MOVEMENT_BASE_SPEED;
    }

    void Animate() {
        //sets animator variable to call paramter in animation window
        if (movement != Vector2.zero){
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        //set speed paramter in animator
        animator.SetFloat("Speed", movementSpeed);
    }
}
