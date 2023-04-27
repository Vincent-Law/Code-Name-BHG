using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forceMultiplier = 100.0f;

    private bool isFired = false;

    public void Fire(float drawDistance)
    {
        if (!isFired)
        {
            isFired = true;
            rb.AddForce(transform.right * drawDistance * forceMultiplier);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFired)
        {
            // Do something when the arrow collides with something
            // For example, you could play a sound, apply damage to an enemy, or create a particle effect.
        }
    }
}
