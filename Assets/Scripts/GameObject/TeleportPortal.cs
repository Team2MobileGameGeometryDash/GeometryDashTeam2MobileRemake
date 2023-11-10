using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortal : MonoBehaviour
{
    public Transform Portal2;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            playerController.transform.position = Portal2.transform.position;

        }

        if(collision.gameObject.TryGetComponent(out MenuAnimation menuAnimation))
        {
            menuAnimation.transform.position = Portal2.transform.position;
        }
    }





}
