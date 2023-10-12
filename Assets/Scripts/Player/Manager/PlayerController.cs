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
    public PlayerMovementData data;



    public GameObject[] Ships;
    

    private void Awake()
    {
        _playerStateManager = new PlayerStateManager(this);


    }

    private void Start()
    {
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


  


}




[System.Serializable]
public struct PlayerMovementData
{
    [Header("PlayerWalkingValue")]
    public float WalkingSpeed;
    public float JumpHeight;
    [HideInInspector]
    public float Time;
    public float GravityScale;
    [HideInInspector]
    public bool isGravityChange;


   



    public float Distance;
    
}