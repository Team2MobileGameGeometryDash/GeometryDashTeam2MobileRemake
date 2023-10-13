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
    public PlayerMovementData Data;
    public PlayerMouvement PlayerMouvement;


    public GameObject[] Ships;
    


    private void Awake()
    {
        PlayerStateManager = new PlayerStateManager(this);
        PlayerMouvement = new PlayerMouvement(Data,this);

    }

    private void Start()
    {
        _playerCollider2D = GetComponent<Collider2D>();
        PlayerRigidBody2D = GetComponent<Rigidbody2D>();
        PlayerRigidBody2D.gravityScale = Data.GravityScale;
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
        Ships[index].SetActive(isActive);
    }



    public bool isGrounded()
    {
        Vector2 center = gameObject.transform.position;
        Vector2 GroundCheckBox = new Vector2(_playerCollider2D.bounds.size.x + 0.01f, _playerCollider2D.bounds.size.y + 0.01f); //Size of collider + 0.01f
        return Physics2D.OverlapBox(center, GroundCheckBox, 0, Data.GroundLayer);
    }

}




[System.Serializable]
public struct PlayerMovementData
{
    [Header("PlayerWalkValue")]
    public float WalkingSpeed;
    [Header("PlayerJumpValue")]
    public float JumpHeight;
    public float RotationSpeed;
    public LayerMask GroundLayer;
    [Header("PlayerGravity")]
    [HideInInspector]
    public float Time;
    public float GravityScale;
    [HideInInspector]
    public bool IsGravityChange;
    [Header("PlayerShips")]
    public bool IsSpaceShip;
    public float JumpImpulse;
}