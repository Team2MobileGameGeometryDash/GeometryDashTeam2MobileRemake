using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPads : MonoBehaviour
{

    public float JumpForce;
    float _multiplier = 100f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerController))
        {
            playerController.PlayerRigidBody2D.velocity = new Vector2(playerController.PlayerRigidBody2D.velocity.x, 0f);
            playerController.PlayerRigidBody2D.AddRelativeForce(Vector2.up * JumpForce * Time.deltaTime * _multiplier, ForceMode2D.Impulse);

        } 

    }

}

 


