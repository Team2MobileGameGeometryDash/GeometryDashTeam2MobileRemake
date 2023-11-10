using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    
    public Rigidbody2D playerRigidBody2D;
    public float velocity;


    private void FixedUpdate()
    {
        playerRigidBody2D.velocity = new Vector2(1 * velocity * 1000 * Time.fixedDeltaTime, playerRigidBody2D.velocity.y);
    }

   


  

}
