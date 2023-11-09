using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Ship UfoCharacterData")]
public class PlayerSOUfoCharacter : PlayerSOBaseData
{
    public Sprite ShipSprite;
    [HideInInspector]
    public GameObject SpriteRotation;
    public PlayerState state;
    public float JumpHeight;
    [Header("PlayerGravity")]
    public float GravityScale;
    [HideInInspector]
    public bool isUfoMeteora;

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
        WalkingSpeed = DefaultWalkingSpeed;
        playerController.PlayerRigidBody2D.gravityScale = GravityScale;
        playerController.PlayerSpriteRenderer.transform.rotation = Quaternion.identity;
        playerController.PlayerSpriteRenderer.sprite = ShipSprite;
        IsGravityChange = false;
        playerController.PlayerSpriteRenderer.flipY = false;
        playerController.transform.localScale = DefaultSize;

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
        isUfoMeteora = true;
    }
}
