using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShipComponent : MonoBehaviour
{

	public bool IsBaseCharacter;
	public bool IsSpaceShip;
	public bool IsGearMode;



	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerController playerController))
		{
			if(IsBaseCharacter)
				playerController.DefaultCharacterData.IsDefaultCharacter = true;
			else if (IsSpaceShip)
				playerController.SpaceShipCharacterData.IsSpaceShip = true;
			else if (IsGearMode)
				playerController.GearModeData.IsGearMode = true;

		}
	}

}
