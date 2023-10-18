using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Player State Manager")]
    public PlayerStateManager PlayerStateManager;



    [Header("Player input and Player locomotion")]
    [HideInInspector]
    public Rigidbody2D PlayerRigidBody2D;
    Collider2D _playerCollider2D;
    public PlayerData Data;
    public PlayerMouvement PlayerMouvement;
    [HideInInspector]
    public Vector2 InitialPosition;

    [Header("DefaultPlayerData")]
    public DefaultCharacterData DefaultCharacterData;
    [Header("SpaceShip")]
    public spaceShipCharacter SpaceshipCharacter;



    private void Awake()
    {
        PlayerStateManager = new PlayerStateManager(this);
        PlayerMouvement = new PlayerMouvement(this);
        InitialPosition = transform.position;
        Debug.Log(InitialPosition);
    }

    private void Start()
    {
        _playerCollider2D = GetComponent<Collider2D>();
        PlayerRigidBody2D = GetComponent<Rigidbody2D>();
        PlayerRigidBody2D.gravityScale = DefaultCharacterData.GravityScale;
    }

    private void Update()
    {
        //Debug.Log(data.CanJump);
        PlayerStateManager.CurrentState.OnUpdate();

    }
    private void FixedUpdate()
    {
        PlayerStateManager.CurrentState.OnFixedUpdate();

    }


 

    public void ChangeCharacter(bool isActive,int index)
    {
        Data.Ships[index].SetActive(isActive);
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
        return Physics2D.OverlapBox(center, GroundCheckBox, 0, DefaultCharacterData.GroundLayer);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStateManager.CurrentState.OnTriggerEnter2D(collision);
    }

}




[System.Serializable]
public struct PlayerData
{
    public GameObject[] Ships;
    [Header("PlayerWalkValue")]
    public float WalkingSpeed;
    [Header("PlayerDeath")]
    public float Death;

}
[System.Serializable]
public struct DefaultCharacterData
{
    [Header("PlayerJumpValue")]
    public float JumpHeight;
    public float RotationSpeed;
    public LayerMask GroundLayer;
    [HideInInspector]
    public bool IsGravityChange;
    [Header("PlayerGravity")]
    [HideInInspector]
    public float Time;
    public float GravityScale;

}
[System.Serializable]
public struct spaceShipCharacter
{
    [Header("PlayerShips")]
    public bool IsSpaceShip;
    public float JumpImpulse;
    [Header("PlayerGravity")]
    [HideInInspector]
    public float Time;
    public float GravityScale;
}

