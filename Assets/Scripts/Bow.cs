using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public Animator animator;
    
    public Transform arrowSpawnPoint;
    public GameObject arrowPrefab;
    public float drawTime = 1.0f;
    public float maxDrawDistance = 2.0f;

    private bool isDrawing = false;
    private float drawStartTime = 0.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetButtonDown("Fire1"))
        {
            StartDrawing();
        }
        else if (Input.GetButtonUp("Fire1"))
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
    }

    private void FireArrow()
    {
        if (arrowPrefab != null)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            float drawDistance = Mathf.Min(maxDrawDistance, Vector2.Distance(transform.position, arrowSpawnPoint.position));
            arrow.GetComponent<ArrowController>().Fire(drawDistance);
        }
    }

}
