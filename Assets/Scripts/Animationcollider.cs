using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Animationcollider : MonoBehaviour
{
    public GameObject crosshair;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.transform.CompareTag("crosshair"))
        {

        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("crosshair"))
        {

        }
    }
    
}
