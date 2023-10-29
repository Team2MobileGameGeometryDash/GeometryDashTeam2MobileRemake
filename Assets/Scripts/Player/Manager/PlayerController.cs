using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player State Manager")]
    public PlayerStateManager PlayerStateManager;
    [HideInInspector]
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



    private Coins[] coinList;


    private void Awake()
    {
        PlayerRigidBody2D = GetComponent<Rigidbody2D>();
        PlayerInputManager = GetComponent<PlayerInputManager>();
        PlayerStateManager = new PlayerStateManager(this);
        PlayerMouvement = new PlayerMouvement(this);

        //Debug.Log(InitialPosition);
    }

    private void Start()
    {
        
        PlayerCollider2D = GetComponent<Collider2D>();
        InitialPosition = transform.position;
        PlayerData.Direction = 1f;
        PlayerRigidBody2D.gravityScale = DefaultCharacterData.GravityScale;
        coinList = FindObjectsOfType<Coins>();
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
    }

    
    private void RaycastDetection()
    {
        Vector3 offset = new Vector3(0, 0.48f, 0);
        Vector2 rayCastPosition = transform.position + offset;
        float maxDistance = 0.6f;
        Debug.DrawRay(rayCastPosition, Vector2.right * maxDistance, Color.black);
        if (Physics2D.Raycast(rayCastPosition, Vector2.right, maxDistance, PlayerData.GroundLayer))
            PlayerStateManager.ChangeState(PlayerState.Death);

        if (!SpaceShipCharacterData.IsSpaceShip) return;
        Vector3 offsetN2 = new Vector3(0, -0.48f, 0);
        Vector2 raycastPositionN2 = transform.position + offsetN2;
        Debug.DrawRay(raycastPositionN2, Vector2.right * maxDistance, Color.black);
        if (Physics2D.Raycast(raycastPositionN2, Vector2.right, maxDistance, PlayerData.GroundLayer))
            PlayerStateManager.ChangeState(PlayerState.Death);
    }


    public void ChangeCharacter(bool isActive,int index)
    {
        PlayerData.Ships[index].SetActive(isActive);
    }



    //TOOOOOOO FIXXXXXXXXXXX
    private void CoinsDetectionWin()
    {
        foreach (Coins coin in coinList)
        {
            coin.SaveCoins();
        }
    }

    private void CoinsDetectionDeath()
    {
        foreach (Coins coin in coinList)
        {
            coin.ResetCoins();
        }
    }

    private void OnEnable()
    {
        PlayerUIManager.OnDeath += CoinsDetectionDeath;
        PlayerUIManager.OnWin += CoinsDetectionWin;
    }

    private void OnDisable()
    {
        PlayerUIManager.OnDeath -= CoinsDetectionDeath;
        PlayerUIManager.OnWin -= CoinsDetectionWin;
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
    public float JumpHeight;
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


