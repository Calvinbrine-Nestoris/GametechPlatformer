using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerControllerSettings", menuName = "Player Settings")]
public class PlayerControllerSettings : ScriptableObject
{
    public float speedXValue = 10f;
    public float speedY = 15f;
    public float groundCheckDistance = 1.5f;
    public float maxCoyoteTime = 0.3f;
    public LayerMask layerMask;

}
