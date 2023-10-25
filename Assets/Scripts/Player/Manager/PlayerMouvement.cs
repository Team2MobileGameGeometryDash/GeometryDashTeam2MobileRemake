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
    public void RotationNotGroundedBaseCharacter(GameObject sprite, bool gravity)
    {
        if (gravity) sprite.transform.Rotate(Vector3.back * _playerController.DefaultCharacterData.RotationSpeed * _multiplier * UnityEngine.Time.deltaTime);
        else sprite.transform.Rotate(Vector3.back * -_playerController.DefaultCharacterData.RotationSpeed * _multiplier * UnityEngine.Time.deltaTime);
    }


    public void HandleMouvementBaseCharacter()
    {

        float speed = _playerController.PlayerData.WalkingSpeed;
        _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerData.Direction * speed * _multiplier * UnityEngine.Time.fixedDeltaTime, _playerController.PlayerRigidBody2D.velocity.y);

    }

    float Time() => _playerController.DefaultCharacterData.Time = UnityEngine.Time.deltaTime;
    public void HandleJumpingBaseCharacter()
    {
        float jumpTime = Mathf.Sqrt(_playerController.DefaultCharacterData.JumpHeight * 2f * -Physics2D.gravity.y);
        float jump = jumpTime - (9.81f * Time());
        if(_playerController.DefaultCharacterData.IsGravityChange)
            _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, -jump );
        else
            _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, jump);

        PlayerInputManager.IsTouchEnded = false;

    }

    public void HandleJumpingShipCharacter()
    {
        float jumpTime = Mathf.Sqrt(_playerController.SpaceShipCharacterData.JumpHeight * 2f * -Physics2D.gravity.y);
        float jump = jumpTime - (9.81f * Time());
        _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, jump);

        PlayerInputManager.IsTouchEnded = false;

    }

    public void HandleJumpingForceShipCharacter()
    {
        
        _playerController.PlayerRigidBody2D.AddRelativeForce(Vector2.up * _playerController.SpaceShipCharacterData.JumpImpulse * _multiplier * UnityEngine.Time.fixedDeltaTime, ForceMode2D.Impulse);
    } 



    /// <summary>
    /// create a box around the player biggest 0.01f
    /// Physics2D.OverlapBox return if the box collide the GroundLayer
    /// </summary>
    /// <returns></returns>
    public bool isGrounded()
    {
        Vector2 center = _playerController.transform.position;
        Vector2 GroundCheckBox = new Vector2(_playerController.PlayerCollider2D.bounds.size.x + 0.01f, _playerController.PlayerCollider2D.bounds.size.y + 0.01f); //Size of collider + 0.01f
        return Physics2D.OverlapBox(center, GroundCheckBox, 0, _playerController.PlayerData.GroundLayer);
    }

}
