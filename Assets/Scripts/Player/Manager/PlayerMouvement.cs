using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement 
{
    PlayerController _playerController;

    public PlayerMouvement(PlayerController playerController)
    {
        _playerController = playerController;
        GameManager.Instance.ObserverPatternPlayer.Register(GameEventEnum.PlayerGameEvent.DefaultJump, HandleJumpingBaseCharacter);
        GameManager.Instance.ObserverPatternPlayer.Register(GameEventEnum.PlayerGameEvent.ShipJump, HandleJumpingShip);
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
        if (gravity) sprite.transform.Rotate(Vector3.back * _playerController.DefaultCharacterData.RotationSpeed * UnityEngine.Time.deltaTime);
        else sprite.transform.Rotate(Vector3.back * -_playerController.DefaultCharacterData.RotationSpeed * UnityEngine.Time.deltaTime);
    }





    public void HandleMouvementBaseCharacter()
    {
        float speed = _playerController.Data.WalkingSpeed;
        _playerController.PlayerRigidBody2D.velocity = new Vector2(speed * UnityEngine.Time.fixedDeltaTime, _playerController.PlayerRigidBody2D.velocity.y);

    }
    float Time() => _playerController.DefaultCharacterData.Time = UnityEngine.Time.deltaTime;
    public void HandleJumpingBaseCharacter(object[] jumpDirection)
    {
        float jumpTime = Mathf.Sqrt(_playerController.DefaultCharacterData.JumpHeight * 2f * -Physics2D.gravity.y);
        float jump = jumpTime - (9.81f * Time());
        _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, jump * ((float)jumpDirection[0]));

    }

    public void HandleJumpingShip(object[] jumpShip = null)
    {
        //_playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, _playerController.PlayerRigidBody2D.velocity.y * UnityEngine.Time.deltaTime * _playerController.Data.JumpImpulse);
        _playerController.PlayerRigidBody2D.AddRelativeForce(Vector2.up * _playerController.SpaceshipCharacter.JumpImpulse * UnityEngine.Time.deltaTime, ForceMode2D.Impulse);
    } //testing maybe better modify the gravity


}
