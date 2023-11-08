using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMenager : MonoBehaviour
{
	[Range(-1, 1)]
	public int Gravity;
	public bool IsGravityChange;
	public EShip eShipGravity;

	private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.TryGetComponent(out PlayerController playerController))
		{
			playerController.PlayerData.IsGravityChange = IsGravityChange;

            switch (eShipGravity)
            {
                case EShip.BaseShip:
					playerController.PlayerRigidBody2D.gravityScale = playerController.DefaultCharacterData.GravityScale * Gravity;
					break;
                case EShip.SpaceShip:
					playerController.PlayerRigidBody2D.gravityScale = playerController.SpaceShipCharacterData.GravityScale * Gravity;
					break;
                case EShip.GearMode:
					playerController.PlayerRigidBody2D.gravityScale = playerController.GearModeData.GravityScale * Gravity;
					break;
                case EShip.UfoShip:
					playerController.PlayerRigidBody2D.gravityScale = playerController.UfoCharacterData.GravityScale * Gravity;
					break;
                case EShip.RobotShip:
					playerController.PlayerRigidBody2D.gravityScale = playerController.RobotData.GravityScale * Gravity;
					break;
            }

        }
    }


}

public enum EShipGravity
{
	BaseShip,
	SpaceShip,
	GearMode,
	UfoShip,
	RobotShip

}

