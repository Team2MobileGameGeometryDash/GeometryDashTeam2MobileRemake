using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoraPortal : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            if (PlayerInputManager.IsTouchEnded || PlayerInputManager.IsTouchStationary)
            {
                playerController.MeteoraModeData.IsMeteora = true;
            }


        }
    }
    


}


