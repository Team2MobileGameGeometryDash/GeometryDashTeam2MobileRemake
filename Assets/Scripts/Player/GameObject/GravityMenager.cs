using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMenager : MonoBehaviour
{
	[Range(-1, 1)]
	public int Gravity;
	public bool isGravityChange;

	
	

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.TryGetComponent(out PlayerController playerController))
		{
			playerController.PlayerRigidBody2D.gravityScale = playerController.data.GravityScale * Gravity;
			playerController.data.isGravityChange = isGravityChange;
			
		}
	}


}
