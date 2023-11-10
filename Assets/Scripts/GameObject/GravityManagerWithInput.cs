using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManagerWithInput : MonoBehaviour
{
    [Range(-1, 1)]
    public int Gravity;
    public bool IsGravityChange;
    [SerializeField] PlayerSOBaseData playerSO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            if(PlayerInputManager.IsTouchEnded || PlayerInputManager.IsTouchStationary)
                playerSO.ApplyChangesGravity(IsGravityChange, Gravity, playerController);



        }
    }
}
