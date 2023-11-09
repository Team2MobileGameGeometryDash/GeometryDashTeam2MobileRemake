using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoraPortal : MonoBehaviour
{

    [SerializeField]
    PlayerSOBaseData PlayerSOBaseData;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            if (PlayerInputManager.IsTouchEnded || PlayerInputManager.IsTouchStationary)
            {
                PlayerSOBaseData.ApplyChangesMeteora(playerController);

            }
            
        }
    }



}


