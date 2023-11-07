using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadsWithInput : MonoBehaviour
{
    public float JumpPadForce;
    float _multiplier = 100f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerController))
        {
            if(PlayerInputManager.IsTouchEnded || PlayerInputManager.IsTouchStationary)
            {
                playerController.PlayerRigidBody2D.velocity = new Vector2(playerController.PlayerRigidBody2D.velocity.x, 0f);
                playerController.PlayerRigidBody2D.AddRelativeForce(Vector2.up * JumpPadForce * Time.deltaTime * _multiplier, ForceMode2D.Impulse);
            }
            

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInputManager.IsTouchEnded = false;
        PlayerInputManager.IsTouchStationary = false;
    }


}
