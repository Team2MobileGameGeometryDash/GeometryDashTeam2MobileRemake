using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizePortal : MonoBehaviour
{
    public bool IsSizeChange;
    [SerializeField]
    PlayerSOBaseData playerSO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            if (IsSizeChange)
                playerController.transform.localScale = playerSO.Size;
            else
                playerController.transform.localScale = playerSO.DefaultSize;
        }
    }
}
