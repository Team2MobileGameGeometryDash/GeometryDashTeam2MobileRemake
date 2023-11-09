using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Ship RobotCharacterData")]
public class PlayerSORobotCharacter : PlayerSOBaseData
{

    public Sprite ShipSprite;
    public PlayerState state;

    [Header("PlayerGravity")]
    public float GravityScale;
    public float JumpHeight;
    public float MaxJumpHight;
    public float TimerHoldJump;
    [HideInInspector]
    public bool isRobotMeteora;

    public override void ApplyChangeShip(PlayerController playerController)
    {
        base.ApplyChangeShip(playerController);
        playerController.PlayerStateManager.ChangeState(state);
        playerController.PlayerSpriteRenderer.sprite = ShipSprite;
        playerController.PlayerSpriteRenderer.transform.rotation = Quaternion.identity;
        playerController.PlayerRigidBody2D.gravityScale = GravityScale;
        playerController.GetComponentInChildren<Animator>().enabled = true;
    }

    public override void ApplyDefaultParameters(PlayerController playerController)
    {
        base.ApplyDefaultParameters(playerController);
        playerController.PlayerSpriteRenderer.transform.rotation = Quaternion.identity;
        playerController.PlayerRigidBody2D.gravityScale = GravityScale;
        playerController.PlayerSpriteRenderer.sprite = ShipSprite;
        IsGravityChange = false;
        playerController.transform.localScale = DefaultSize;
        playerController.PlayerSpriteRenderer.flipY = false;

    }

    public override void ApplyChangesGravity(bool isGravityChanges, float gravity, PlayerController playerController)
    {
        base.ApplyChangesGravity(isGravityChanges, gravity, playerController);
        IsGravityChange = isGravityChanges;
        playerController.PlayerRigidBody2D.gravityScale = gravity * GravityScale;
        playerController.PlayerSpriteRenderer.flipY = isGravityChanges;
    }


    public override void ApplyChangesMeteora(PlayerController playerController)
    {
        base.ApplyChangesMeteora(playerController);
        playerController.PlayerStateManager.ChangeState(PlayerState.MeteoraMode);
        isRobotMeteora = true;
    }

}
