using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public Animator animator;
    
    public Transform arrowSpawnPoint;
    public GameObject arrowPrefab;
    public GameObject crossHair;
    public GameObject player;
    public float drawTime = 1.0f;
    public float maxDrawDistance = 2.0f;

    private bool isDrawing = false;
    private bool fire = false;
    private float drawStartTime = 0.0f;

    Vector2 mousePosition;
    Vector3 screenPoint;
    Vector2 offset;
    public Vector3 bowPos;
    public Vector3 aim;
    public Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
       
        offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);

        crossHair.transform.position = screenPoint; 
        aim = offset;
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        bowPos = aim; 
        bowPos.Normalize();

        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetButtonDown("Fire"))
        {
            StartDrawing();
        }

        if (Input.GetButtonUp("Fire"))
        {
            StopDrawing();
            FireArrow();
        }

        if (isDrawing)
        {
            float drawProgress = Mathf.Clamp01((Time.time - drawStartTime) / drawTime);
            animator.SetFloat("drawProgress", drawProgress);
        }
    }

    private void StartDrawing()
    {
        isDrawing = true;
        drawStartTime = Time.time;
        animator.SetBool("isDrawing", true);
    }

    private void StopDrawing()
    { 
        isDrawing = false;
        animator.SetBool("isDrawing", false);
        animator.SetBool("fire", true);
    }

    private void FireArrow()
    {
        
        if (arrowPrefab != null)
        {
            //GameObject arrow = Instantiate(arrowPrefab, rb.position + Vector2.up * 0.20f, Quaternion.identity);
            //float drawDistance = Mathf.Min(maxDrawDistance, Vector2.Distance(transform.position, arrowSpawnPoint.position));
            //arrow.GetComponent<ArrowController>().Fire(drawDistance);
            //Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());

            //add rigibody2D component to arrow prefab
            GameObject arrow = Instantiate(arrowPrefab, rb.position + Vector2.up * 0.20f, Quaternion.identity); //Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            
            //set gravity scale to 0
            //arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * .05f;
            //provides shooting angle based on crosshair placement
            //arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            //logic to add for arrow stoping when near the end of its magnitude
        }
        
    }

}
