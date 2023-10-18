using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShipComponent : MonoBehaviour
{
	public bool ChangeShip;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out PlayerController playerController))
		{
			playerController.SpaceshipCharacter.IsSpaceShip = ChangeShip;

		}
	}

}
