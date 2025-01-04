using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Character", menuName ="Playable Character")]
public class SOPlayerChoice : ScriptableObject
{
    public float speed;
    public float runSpeed;
    public float crouchSpeed;
    public float jumpForce;
}
