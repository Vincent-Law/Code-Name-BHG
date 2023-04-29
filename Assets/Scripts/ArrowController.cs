using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject arrow;
    public GameObject player;

   
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            // Do something when the arrow collides with something
            // For example, you could play a sound, apply damage to an enemy, or create a particle effect.
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), arrow.GetComponent<Collider2D>());
        
    }
}
