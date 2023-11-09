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

    [SerializeField]
    PlayerSOBaseData playerSO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            switch (VelocityPortal)
            {
                case EVelocityPortal.BlueSpeedPortal:
                    playerSO.WalkingSpeed = BlueSpeedPortal;

                    break;
                case EVelocityPortal.GreenSpeedPortal:
                    playerSO.WalkingSpeed = GreenSpeedPortal;

                    break;
                case EVelocityPortal.PinkSpeedPortal:
                    playerSO.WalkingSpeed = PinkSpeedPortal;

                    break;
                case EVelocityPortal.RedSpeedPortal:
                    playerSO.WalkingSpeed = RedSpeedPortal;

                    break;
                case EVelocityPortal.DecreasingSpeedPortal:
                    playerSO.WalkingSpeed = DecreasingSpeedPortal;

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
