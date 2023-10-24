using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player State Manager")]
    public PlayerStateManager PlayerStateManager;
    public PlayerInputManager PlayerInputManager;


    [Header("Player input and Player locomotion")]
    [HideInInspector]
    public Rigidbody2D PlayerRigidBody2D;
    [HideInInspector]
    public Collider2D PlayerCollider2D;
    public PlayerData PlayerData;
    public PlayerMouvement PlayerMouvement;
    [HideInInspector]
    public Vector3 InitialPosition;


    public DefaultCharacterData DefaultCharacterData;

    public SpaceShipCharacterData SpaceShipCharacterData;

    public GearModeData GearModeData;

    




    private void Awake()
    {
        PlayerRigidBody2D = GetComponent<Rigidbody2D>();
        PlayerStateManager = new PlayerStateManager(this);
        PlayerMouvement = new PlayerMouvement(this);

        //Debug.Log(InitialPosition);
    }

    private void Start()
    {
        PlayerInputManager = GetComponent<PlayerInputManager>();
        PlayerCollider2D = GetComponent<Collider2D>();
        InitialPosition = transform.position;
        PlayerData.Direction = 1f;
        PlayerRigidBody2D.gravityScale = DefaultCharacterData.GravityScale;
    }

    private void Update()
    {
        //Debug.Log(data.CanJump);
        PlayerStateManager.CurrentState.OnUpdate();
        RaycastDetection();
 
    }
    private void FixedUpdate()
    {
        PlayerStateManager.CurrentState.OnFixedUpdate();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStateManager.CurrentState.OnTriggerEnter2D(collision);
        //  bitwise operations and bit shifting to correctly identify the layer, online solution
        
    }

  
    private void RaycastDetection()
    {
        Vector2 rayCastPosition = transform.position ;
        float maxDistance = 0.6f; 
        if (Physics2D.Raycast(rayCastPosition, Vector2.right, maxDistance , PlayerData.GroundLayer)) 
            PlayerStateManager.ChangeState(PlayerState.Death);  
    }


    public void ChangeCharacter(bool isActive,int index)
    {
        PlayerData.Ships[index].SetActive(isActive);
    }


    




   



   



}




[System.Serializable]
public struct PlayerData
{
    public GameObject[] Ships;
    [Header("PlayerWalkValue")]
    public float WalkingSpeed;
    [HideInInspector]
    public float Direction;
    [Header("PlayerLayer")]
    public LayerMask GroundLayer;
    [Header("PlayerDeath")]
    public float Death;

}
[System.Serializable]
public struct DefaultCharacterData
{
    [Header("PlayerShips")]
    public bool IsDefaultCharacter;
    [Header("PlayerJumpValue")]
    public float JumpHeight;
    public float RotationSpeed;
    
    [Header("PlayerGravity")]
    public float GravityScale;
    [HideInInspector]
    public bool IsGravityChange;
    [HideInInspector]
    public float Time;

}
[System.Serializable]
public struct SpaceShipCharacterData
{
    [Header("PlayerShips")]
    public bool IsSpaceShip;
    public float JumpImpulse;
    
    [Header("PlayerGravity")]
    public float GravityScale;
    [HideInInspector]
    public float Time;
}

[System.Serializable]
public struct GearModeData
{
    [Header("PlayerShips")]
    public bool IsGearMode;    

    [Header("PlayerGravity")]
    public float GravityScale;
    public float GravityVelocity;
    [HideInInspector]
    public bool IsGravityChange;
    [HideInInspector]
    public float Time;
}


