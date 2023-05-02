using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLayerController : MonoBehaviour
{
    public Transform characterTransform;
    public Transform weaponTransform;

    private bool isMovingUp = true;

    void Update()
    {
        // Calculate the direction from the weapon to the character
        Vector3 directionToCharacter = characterTransform.position - weaponTransform.position;

        // Check if the direction is pointing upwards (above the character's y axis)
        if (directionToCharacter.y > 0)
        {
            // If the weapon is facing upwards, move the weapon up by 0.1 units on the y-axis
            weaponTransform.Translate(0f, 0.1f, 0f);

            // Set the flag to indicate that the weapon is moving up
            isMovingUp = true;
        }
        // Check if the direction is pointing downwards (past the character's -y axis)
        else if (directionToCharacter.y < 0)
        {
            // If the weapon is facing downwards, move the weapon down by 0.1 units on the y-axis
            weaponTransform.Translate(0f, 0.1f, 0f);

            // Set the flag to indicate that the weapon is moving down
            isMovingUp = false;
        }
    }
}
