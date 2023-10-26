using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public static Action OnResetCamera;
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float thresholdY;

    private float startPositionY;
    private float currentPositionY;


    private void OnEnable()
    {
        OnResetCamera += ResetCamera;
    }

    private void OnDisable()
    {
        OnResetCamera -= ResetCamera;
    }



    private void Update()
    {
        if (Mathf.Abs(player.position.y - currentPositionY) >= thresholdY)
        {
            currentPositionY = player.position.y;
        }
        SmoothFollow();
    }


    private void SmoothFollow()
    {
        Vector3 Position = new Vector3(player.position.x + offset.x, currentPositionY + offset.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, Position, smoothSpeed * Time.deltaTime);
    }

    public void ResetCamera()
    {
        Vector3 cameraStart = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        startPositionY = cameraStart.y;
        currentPositionY = startPositionY;
        transform.position = cameraStart;
    }

}
