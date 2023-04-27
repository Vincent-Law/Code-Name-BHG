using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Animator animator;
    
    public float maxChargeTime = 2.0f;
    public float minDamage = 1.0f;
    public float maxDamage = 10.0f;
    private GameObject arrowPrefab;
    private float currentHoldTime;
    private float chargeTime = 0.0f;

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

        if (Input.GetButton("Fire"))
        {
            // Charge up the bow
            chargeTime += Time.deltaTime;
            if (chargeTime > maxChargeTime)
            {
                chargeTime = maxChargeTime;
            }

            animator.SetFloat("ChargeSpeed", chargeTime / maxChargeTime);
            animator.SetBool("IsCharging", true);
        }
        else if (Input.GetButtonUp("Fire"))
        {
            // Release the arrow
            animator.SetBool("IsCharging", false);

            //GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            //float damage = Mathf.Lerp(minDamage, maxDamage, chargeTime / maxChargeTime);
            //arrow.GetComponent<ArrowController>().SetDamage(damage);

            chargeTime = 0.0f;
        }
    }

}
