using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShipComponent : MonoBehaviour
{
    public static Action<BaseInput> OnChangeShip;

    public EShip eShip;


    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerController playerController))
		{
            switch (eShip)
            {
                case EShip.BaseShip:
                    playerController.DefaultCharacterData.IsDefaultCharacter = true;
                    OnChangeShip?.Invoke(new BaseCharacterInput());
                    break;
                case EShip.SpaceShip:
                    playerController.SpaceShipCharacterData.IsSpaceShip = true;
                    OnChangeShip?.Invoke(new SpaceShipInput());
                    break;
                case EShip.GearMode:
                    playerController.GearModeData.IsGearMode = true;
                    OnChangeShip?.Invoke(new GearModeInput());
                    break;
            }
        
        }
    }

}


public enum EShip
{
    BaseShip,
    SpaceShip,
    GearMode

}
