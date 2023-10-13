using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement 
{
    PlayerMovementData _data;
    PlayerController _playerController;

    public PlayerMouvement(PlayerMovementData data,PlayerController playerController)
    {
        _data = data;
        _playerController = playerController;
        GameManager.Instance.observerPattern.Register(GameEventEnum.GameEvent.GravitySwitch, HandleJumping);
    }


    public void RotationWhenGrounded(GameObject sprite)
    {
        Vector3 Rotation = sprite.transform.rotation.eulerAngles;
        Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
        sprite.transform.rotation = Quaternion.Euler(Rotation);
    }
    public void RotationNotGrounded(GameObject sprite, bool gravity)
    {
        if (gravity) sprite.transform.Rotate(Vector3.back * _data.RotationSpeed * UnityEngine.Time.deltaTime);
        else sprite.transform.Rotate(Vector3.back * -_data.RotationSpeed * UnityEngine.Time.deltaTime);
    }


    public void HandleMovement()
    {
        float speed = _playerController.Data.WalkingSpeed;
        _playerController.PlayerRigidBody2D.velocity = new Vector2(speed * UnityEngine.Time.fixedDeltaTime, _playerController.PlayerRigidBody2D.velocity.y);

    }
    float Time() => _playerController.Data.Time = UnityEngine.Time.deltaTime;
    public void HandleJumping(object[] jumpDirection)
    {
        float jumpForce = Mathf.Sqrt(_playerController.Data.JumpHeight * 2f * -Physics2D.gravity.y);
        float jump = jumpForce - (9.81f * Time());
        _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, jump * ((float)jumpDirection[0]));

    }

}
