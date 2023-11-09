using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Ship CubeCharacterData")]
public class PlayerSOCubeCharacter : PlayerSOBaseData
{
    public Sprite ShipSprite;
    [HideInInspector]
    public GameObject SpriteRotation;
    public PlayerState state;
    [Header("PlayerJumpValue")]
    public float JumpHeight;
    public float RotationSpeed;
    [Header("PlayerGravity")]
    public float GravityScale;
    [HideInInspector]
    public bool isCubeMeteora;
 

    public override void ApplyChangeShip(PlayerController playerController)
    {
        base.ApplyChangeShip(playerController);
        playerController.PlayerStateManager.ChangeState(state);
        playerController.PlayerSpriteRenderer.sprite = ShipSprite;
        playerController.PlayerSpriteRenderer.transform.rotation = Quaternion.identity;
        playerController.PlayerRigidBody2D.gravityScale = GravityScale;

    }

    public override void ApplyDefaultParameters(PlayerController playerController)
    {
        base.ApplyDefaultParameters(playerController);
        playerController.PlayerSpriteRenderer.transform.rotation = Quaternion.identity;
        playerController.PlayerRigidBody2D.gravityScale = GravityScale;
        SpriteRotation = playerController.PlayerSprite;
        playerController.PlayerSpriteRenderer.sprite = ShipSprite;
        IsGravityChange = false;
        playerController.transform.localScale = DefaultSize;
         
    }

    public override void ApplyChangesGravity(bool isGravityChanges, float gravity,PlayerController playerController)
    {
        base.ApplyChangesGravity(isGravityChanges, gravity, playerController);
        IsGravityChange = isGravityChanges;
        playerController.PlayerRigidBody2D.gravityScale = gravity * GravityScale;
    }

    public override void ApplyChangesMeteora(PlayerController playerController)
    {
        base.ApplyChangesMeteora(playerController);
        playerController.PlayerStateManager.ChangeState(PlayerState.MeteoraMode);
        isCubeMeteora = true;
    }
}