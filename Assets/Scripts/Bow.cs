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
    private float drawProgress;

   private bool isDrawing = false;
    //private bool fire = false;
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

    private void FireArrow(float drawProgess, Quaternion rotation,float angle, Vector2 dir )
    {
        
        if (arrowPrefab != null)
        {
            //Debug.Log(drawProgress);
            //GameObject arrow = Instantiate(arrowPrefab, rb.position + Vector2.up * 0.20f, Quaternion.identity);
            //Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            //arrow.GetComponent<Rigidbody2D>().velocity = offset * .05f;
            //arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg);

            //add rigibody2D component to arrow prefab
             GameObject arrow = Instantiate(arrowPrefab, rb.position /*+ Vector2.up * 0.20f*/, Quaternion.identity);
            //Physics2D.IgnoreCollision(arrowPrefab.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
            
            Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            

            //set gravity scale to 0
             arrow.GetComponent<Rigidbody2D>().velocity = dir * drawProgess *2f;
           // provides shooting angle based on crosshair placement
             arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
           // logic to add for arrow stoping when near the end of its magnitude



            //float drawDistance = Mathf.Min(maxDrawDistance, Vector2.Distance(transform.position, arrowSpawnPoint.position));
            //arrow.GetComponent<ArrowController>().Fire(drawDistance);
            //Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());

                //add rigibody2D component to arrow prefab
                //GameObject arrow = Instantiate(arrowPrefab, rb.position + Vector2.up * 0.20f, Quaternion.identity); //Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                //Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());

                //set gravity scale to 0
                //arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * .05f;
                //provides shooting angle based on crosshair placement
                //arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                //logic to add for arrow stoping when near the end of its magnitude
        }

    }

}
