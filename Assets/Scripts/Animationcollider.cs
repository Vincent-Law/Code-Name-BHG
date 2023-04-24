using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Animationcollider : MonoBehaviour
{
    
    public string direction;
    bool topAnimation;
    bool bottomAnimation;
    bool rightAnimation;
    bool leftAnimation;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("crosshair"))
        {
            Debug.Log(direction);
            
            if (direction == "Top")
            {
                Debug.Log("topAnimation");
                topAnimation = true;
                bottomAnimation = false;
                rightAnimation = false;
                leftAnimation = false;
            }
            if(direction == "Bottom")
            {
                bottomAnimation = true;
                topAnimation = false;
                rightAnimation = false;
                leftAnimation = false;
            }
            if(direction == "Right")
            {
                rightAnimation = true;
                bottomAnimation = false;
                leftAnimation = false;
                topAnimation = false;
            }
            if(direction == "Left")
            {
                leftAnimation = true;
                bottomAnimation = false;
                topAnimation = false;
                rightAnimation = false;
                
            }
        } 
    }





    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("crosshair"))
        {

        }
    }
    
}
