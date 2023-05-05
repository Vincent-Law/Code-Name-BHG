using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            //stop the arrow and freeze it when it collides with something
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
    }
}
