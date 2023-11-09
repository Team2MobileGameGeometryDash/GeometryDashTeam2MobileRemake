using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement 
{
    PlayerController _playerController;
    float _multiplier = 100f;

    public PlayerMouvement(PlayerController playerController)
    {
        _playerController = playerController;

    }


    /// <summary>
    /// Set the rotation to the 90° rotation more close 
    /// 90° multiple
    /// </summary>
    public void RotationWhenGroundedBaseCharacter(GameObject sprite)
    {
        Vector3 Rotation = sprite.transform.rotation.eulerAngles;
        Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
        sprite.transform.rotation = Quaternion.Euler(Rotation);

    }

    /// <summary>
    /// make the rotation based on the gravity
    /// </summary>
    public void RotationNotGroundedBaseCharacter(PlayerSOCubeCharacter playerSODefaultCharacter)
    {
        if (playerSODefaultCharacter.IsGravityChange) playerSODefaultCharacter.SpriteRotation.transform.Rotate(Vector3.back * -playerSODefaultCharacter.RotationSpeed * _multiplier * UnityEngine.Time.deltaTime);
        else playerSODefaultCharacter.SpriteRotation.transform.Rotate(Vector3.back * playerSODefaultCharacter.RotationSpeed * _multiplier * UnityEngine.Time.deltaTime);
    }


    public void HandleMouvementBaseCharacter(PlayerSOBaseData playerSO)
    {

        float speed = playerSO.WalkingSpeed;
        _playerController.PlayerRigidBody2D.velocity = new Vector2(1 * speed * _multiplier * UnityEngine.Time.fixedDeltaTime, _playerController.PlayerRigidBody2D.velocity.y);

    }

    float Time(PlayerSOBaseData playerSO) => playerSO.Time = UnityEngine.Time.deltaTime;
    public void HandleJumpingBaseCharacter(PlayerSOCubeCharacter playerSODefaultCharacter)
    {
        float jumpTime = Mathf.Sqrt(playerSODefaultCharacter.JumpHeight * 2f * -Physics2D.gravity.y);
        float jump = jumpTime - (9.81f * Time(playerSODefaultCharacter));
        if (playerSODefaultCharacter.IsGravityChange)
            _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, -jump);
        else
            _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, jump);

        PlayerInputManager.IsTouchEnded = false;


    }

    public void HandleJumpingShipCharacter(PlayerSOShipCharacter playerSOShipCharacter)
    {
        float jumpTime = Mathf.Sqrt(playerSOShipCharacter.JumpHeight * 2f * -Physics2D.gravity.y);
        float jump = jumpTime - (9.81f * Time(playerSOShipCharacter));
        if (playerSOShipCharacter.IsGravityChange)
            _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, -jump);
        else
            _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, jump);

        PlayerInputManager.IsTouchEnded = false;

    }


    public void HandleJumpingForceShipCharacter(PlayerSOShipCharacter playerSOShipCharacter)
    {
        if (!playerSOShipCharacter.IsGravityChange)
            _playerController.PlayerRigidBody2D.AddRelativeForce(Vector2.up * playerSOShipCharacter.JumpImpulse * _multiplier * UnityEngine.Time.fixedDeltaTime, ForceMode2D.Impulse);
        else
            _playerController.PlayerRigidBody2D.AddRelativeForce(Vector2.up * playerSOShipCharacter.JumpImpulse * _multiplier * UnityEngine.Time.fixedDeltaTime * -1, ForceMode2D.Impulse);
    }



    public void HandleJumpingUfoCharacter(PlayerSOUfoCharacter playerSOUfoCharacter)
    {
        float jumpTime = Mathf.Sqrt(playerSOUfoCharacter.JumpHeight * 2f * -Physics2D.gravity.y);
        float jump = jumpTime - (9.81f * Time(playerSOUfoCharacter));

        if (playerSOUfoCharacter.IsGravityChange)
            _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, -jump);
        else
            _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, jump);


        PlayerInputManager.IsTouchEnded = false;

    }


    /// <summary>
    /// create a box around the player biggest 0.01f
    /// Physics2D.OverlapBox return if the box collide the GroundLayer
    /// </summary>
    /// <returns></returns>
    public bool isGrounded(PlayerSOBaseData playerSO)
    {
        Vector2 center = _playerController.transform.position;
        Vector2 GroundCheckBox = new Vector2(_playerController.PlayerCollider2D.bounds.size.x + 0.01f, _playerController.PlayerCollider2D.bounds.size.y + 0.01f); //Size of collider + 0.01f
        return Physics2D.OverlapBox(center, GroundCheckBox, 0, playerSO.GroundLayer);
    }


    float ActualJumpTime = 0;
    public void HandleJumpRobotCharacter(PlayerSORobotCharacter playerSORobotCharacter)
    {
        float jumpTime = Mathf.Sqrt(playerSORobotCharacter.JumpHeight * 2f * -Physics2D.gravity.y);
        float jump = jumpTime - (9.81f * Time(playerSORobotCharacter));


        if (PlayerInputManager.IsTouchEnded && isGrounded(playerSORobotCharacter))
        {
            if (ActualJumpTime != 0) return;
            if (playerSORobotCharacter.IsGravityChange)
            {
                _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, -jump);
                
                PlayerInputManager.IsTouchEnded = false;
            }
            else
            {
                _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, jump);
                
                PlayerInputManager.IsTouchEnded = false;
            }
        }


        if (PlayerInputManager.IsTouchStationary)
        {
            if (ActualJumpTime < playerSORobotCharacter.TimerHoldJump)
            {
                ActualJumpTime += Time(playerSORobotCharacter);
                //Debug.Log(ActualJumpTime);
                jump += ActualJumpTime;
                if (jump >= playerSORobotCharacter.MaxJumpHight)
                    jump = playerSORobotCharacter.MaxJumpHight;
                if (playerSORobotCharacter.IsGravityChange)
                {
                    _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, -jump);
                    
                }
                else
                {
                    _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, jump);
                    
                }
            }

        }

        if (!PlayerInputManager.IsTouchEnded && !PlayerInputManager.IsTouchStationary)
            ActualJumpTime = 0;


    }



}
