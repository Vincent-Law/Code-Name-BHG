using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public Animator animator;
    public GameObject arrowPrefab;
    public GameObject crossHair;
    public GameObject player;
    public float drawTime = 1.0f;
    public float maxDrawDistance = 2.0f;
    private float drawProgress;
    private float drawStartTime = 0.0f;
    private bool isDrawing = false;
    
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
        //Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        //bowPos = aim; 

        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        bowPos += aim * Time.deltaTime; // Update the position of the bow

        if (Input.GetButtonDown("Fire"))
        {
            StartDrawing();
        }
        if (isDrawing)
        {
            drawProgress = Mathf.Clamp01((Time.time - drawStartTime) / drawTime);
            animator.SetFloat("drawProgress", drawProgress);
        }
        if (Input.GetButtonUp("Fire"))
        {
            StopDrawing();
            FireArrow(drawProgress, rotation, angle, dir);
        }

         animator.SetBool("fire", false);

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

    private void FireArrow(float drawProgess, Quaternion rotation,float angle, Vector2 dir)
    {
        if (arrowPrefab != null)
        {
            //add rigibody2D component to arrow prefab
            GameObject arrow = Instantiate(arrowPrefab, rb.position + Vector2.up * 0.20f, Quaternion.identity);
            Physics2D.IgnoreCollision(arrowPrefab.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            arrow.GetComponent<Rigidbody2D>().gravityScale = 0;

            // Calculate velocity vector
            float magnitude = drawProgress * 10f;
            Vector2 velocity = new Vector2(magnitude * Mathf.Cos(angle * Mathf.Deg2Rad), magnitude * Mathf.Sin(angle * Mathf.Deg2Rad));

            //provides shooting angle based on crosshair placement
            arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

            //set velocity
            arrow.GetComponent<Rigidbody2D>().velocity = velocity;

            // Exponentially decay velocity over time
            float decayFactor = 0.3f; // Decay factor (adjust as needed)
            float decayTime = 5f; // Decay time in seconds (adjust as needed)
            float elapsedTime = 0;
            float initialVelocityMagnitude = arrow.GetComponent<Rigidbody2D>().velocity.magnitude;
            while (elapsedTime < decayTime && arrow != null)
                {
                    elapsedTime += Time.deltaTime;
                    float decayAmount = Mathf.Pow(decayFactor, elapsedTime / decayTime);
                    arrow.GetComponent<Rigidbody2D>().velocity = velocity.normalized * initialVelocityMagnitude * decayAmount;
                    Debug.Log(decayAmount);
                }

            // Destroy arrow when it becomes too slow
            if (arrow != null && decayTime > 5f)
                {
                    Destroy(arrow);
                }
        }
    }

}

