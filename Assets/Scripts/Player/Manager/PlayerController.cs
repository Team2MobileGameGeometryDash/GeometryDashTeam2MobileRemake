using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static Transform PlayerTransform;

    [Header("Player State Manager")]
    public PlayerStateManager PlayerStateManager;



    [Header("Player input and Player locomotion")]
    [HideInInspector]
    public Rigidbody2D PlayerRigidBody2D;
    Collider2D _playerCollider2D;
    public PlayerData PlayerData;
    public PlayerMouvement PlayerMouvement;
    [HideInInspector]
    public Vector2 InitialPosition;


    public DefaultCharacterData DefaultCharacterData;

    public SpaceShipCharacterData SpaceShipCharacterData;

    public GearModeData GearModeData;

    //to fix
    [HideInInspector] public bool IsTouchBegan;
    [HideInInspector] public bool IsTouchStationary;


    private void Awake()
    {
        PlayerRigidBody2D = GetComponent<Rigidbody2D>();
        PlayerStateManager = new PlayerStateManager(this);
        PlayerMouvement = new PlayerMouvement(this);

        //Debug.Log(InitialPosition);
    }

    private void Start()
    {
        _playerCollider2D = GetComponent<Collider2D>();
        InitialPosition = transform.position;
        PlayerData.Direction = 1f;
        PlayerRigidBody2D.gravityScale = DefaultCharacterData.GravityScale;
    }

    private void Update()
    {
        //Debug.Log(data.CanJump);
        PlayerStateManager.CurrentState.OnUpdate();
        if (!isTouching()) return;
        InputManager();

       
    }
    private void FixedUpdate()
    {
        PlayerStateManager.CurrentState.OnFixedUpdate();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStateManager.CurrentState.OnTriggerEnter2D(collision);
    }

    public void ChangeCharacter(bool isActive,int index)
    {
        PlayerData.Ships[index].SetActive(isActive);
    }


    /// <summary>
    /// create a box around the player biggest 0.01f
    /// Physics2D.OverlapBox return if the box collide the GroundLayer
    /// </summary>
    /// <returns></returns>
    public bool isGrounded()
    {
        Vector2 center = transform.position;
        Vector2 GroundCheckBox = new Vector2(_playerCollider2D.bounds.size.x + 0.01f, _playerCollider2D.bounds.size.y + 0.01f); //Size of collider + 0.01f
        return Physics2D.OverlapBox(center, GroundCheckBox, 0, PlayerData.GroundLayer);
    }




    //to fix
    private bool isTouching()
    {
        if (Input.touchCount <= 0) return false;
        else return true;
    }


    //to fix
    private void InputManager()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began) IsTouchBegan = true;
        else if (touch.phase == TouchPhase.Stationary)
        {
            IsTouchStationary = true;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            IsTouchStationary = false;
            IsTouchBegan = false;

        }
        else IsTouchBegan = false;
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


