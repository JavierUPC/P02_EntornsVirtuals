using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAssign : MonoBehaviour
{
    public SOPlayerChoice Character;
    private UlisesPlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = this.GetComponent<UlisesPlayerMovement>();
        playerMovement.speed = Character.speed;
        playerMovement.runSpeed = Character.runSpeed;
        playerMovement.crouchSpeed = Character.crouchSpeed;
        playerMovement.jumpForce = Character.jumpForce;
    }

    
}
