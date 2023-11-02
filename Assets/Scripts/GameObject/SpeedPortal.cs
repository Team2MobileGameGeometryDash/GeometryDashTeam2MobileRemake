using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPortal : MonoBehaviour
{
    public EVelocityPortal VelocityPortal;

    public float BlueSpeedPortal;
    public float GreenSpeedPortal;
    public float PinkSpeedPortal;
    public float RedSpeedPortal;
    public float DecreasingSpeedPortal;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            switch (VelocityPortal)
            {
                case EVelocityPortal.BlueSpeedPortal:
                    playerController.PlayerData.WalkingSpeed = BlueSpeedPortal;

                    break;
                case EVelocityPortal.GreenSpeedPortal:
                    playerController.PlayerData.WalkingSpeed = GreenSpeedPortal;

                    break;
                case EVelocityPortal.PinkSpeedPortal:
                    playerController.PlayerData.WalkingSpeed = PinkSpeedPortal;

                    break;
                case EVelocityPortal.RedSpeedPortal:
                    playerController.PlayerData.WalkingSpeed = RedSpeedPortal;

                    break;
                case EVelocityPortal.DecreasingSpeedPortal:
                    playerController.PlayerData.WalkingSpeed = DecreasingSpeedPortal;

                    break;
            }
        }
    }
}

public enum EVelocityPortal
{
    BlueSpeedPortal,
    GreenSpeedPortal,
    PinkSpeedPortal,
    RedSpeedPortal,
    DecreasingSpeedPortal



}
