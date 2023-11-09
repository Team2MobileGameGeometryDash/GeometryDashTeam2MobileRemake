using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Ship Data")]
public class PlayerSOBaseData : ScriptableObject
{
    
    [Header("PlayerWalkValue")]
    public float DefaultWalkingSpeed;
    [HideInInspector]
    public float WalkingSpeed;
    [HideInInspector]
    public float Direction;
    [Header("PlayerLayer")]
    public LayerMask GroundLayer;
    [HideInInspector]
    public bool isWin;
    [HideInInspector]
    public float Time;
    [HideInInspector]
    public bool IsGravityChange;
    public Vector3 DefaultSize;
    public Vector3 Size;
    public float MeteoraVelocity;


    public virtual void ApplyDefaultParameters(PlayerController playerController) { }
   

    public virtual void ApplyChangeShip(PlayerController playerController) { }
  
    public virtual void Death(PlayerController playerController,bool isEnabled)
    {
        playerController.PlayerSpriteRenderer.enabled = isEnabled;
    }
   
    public virtual void ApplyChangesGravity(bool isGravityChanges,float gravity, PlayerController playerController) { }
   

    public virtual void ApplyChangesMeteora(PlayerController playerController) { }
}
