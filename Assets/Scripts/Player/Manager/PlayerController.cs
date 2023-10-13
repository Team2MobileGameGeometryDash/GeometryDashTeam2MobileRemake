using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Player State Manager")]
    public PlayerStateManager _playerStateManager;



    [Header("Player input and Player locomotion")]
    [HideInInspector]
    public Rigidbody2D PlayerRigidBody2D;
    private Collider2D PlayerCollider2D;
    public PlayerMovementData data;



    public GameObject[] Ships;
    public LayerMask GroundLayer;


    private void Awake()
    {
        _playerStateManager = new PlayerStateManager(this);


    }

    private void Start()
    {
        PlayerCollider2D = GetComponent<Collider2D>();
        PlayerRigidBody2D = GetComponent<Rigidbody2D>();
        PlayerRigidBody2D.gravityScale = data.GravityScale;
    }

    private void Update()
    {
        //Debug.Log(data.CanJump);
        _playerStateManager.CurrentState.OnUpdate();

    }
    private void FixedUpdate()
    {
        _playerStateManager.CurrentState.OnFixedUpdate();

    }


 

    public void ChangeCharacter(bool isActive,int index)
    {
        Ships[index].SetActive(isActive);
    }

    public void RotationWhenGrounded(GameObject sprite)
    {
        Vector3 Rotation = sprite.transform.rotation.eulerAngles;
        Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
        sprite.transform.rotation = Quaternion.Euler(Rotation);
    }
    public void RotationNotGrounded(GameObject sprite, bool gravity)
    {
        if (gravity) sprite.transform.Rotate(Vector3.back * data.RotationSpeed * Time.deltaTime);
        else sprite.transform.Rotate(Vector3.back * -data.RotationSpeed * Time.deltaTime);
    }

    public bool isGrounded()
    {
        Vector2 center = gameObject.transform.position;
        Vector2 GroundCheckBox = new Vector2(PlayerCollider2D.bounds.size.x + 0.01f, PlayerCollider2D.bounds.size.y + 0.01f); //Size of collider + 0.01f
        return Physics2D.OverlapBox(center, GroundCheckBox, 0, GroundLayer);
    }



}




[System.Serializable]
public struct PlayerMovementData
{
    [Header("PlayerWalkingValue")]
    public float WalkingSpeed;
    public float JumpHeight;
    public float RotationSpeed;
    [HideInInspector]
    public float Time;
    public float GravityScale;
    [HideInInspector]
    public bool isGravityChange;


   



    public float Distance;
    
}