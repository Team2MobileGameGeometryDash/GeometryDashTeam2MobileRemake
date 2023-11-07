using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShipComponent : MonoBehaviour
{
    public EShip eShip;



    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerController playerController))
		{
            switch (eShip)
            {
                case EShip.BaseShip:
                    playerController.DefaultCharacterData.IsDefaultCharacter = true;
                    
                    break;
                case EShip.SpaceShip:
                    playerController.SpaceShipCharacterData.IsSpaceShip = true;
                    
                    break;
                case EShip.GearMode:
                    playerController.GearModeData.IsGearMode = true;
                    
                    break;

                case EShip.UfoShip:
                    playerController.UfoCharacterData.IsUfo = true;

                    break;

                case EShip.RobotShip:
                    playerController.RobotData.IsRobot = true;

                    break;
            }
        
        }
    }

}


public enum EShip
{
    BaseShip,
    SpaceShip,
    GearMode,
    UfoShip,
    RobotShip

}
