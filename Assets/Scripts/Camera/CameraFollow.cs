using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    
    public Transform player;
    public float smoothSpeed;
    public Vector3 offset;
    [Tooltip("The first threshold before the new value")]
    public float StartThresholdY;
    [Tooltip("Threshold value after the first")]
    public float thresholdY;

    private float currentPositionY;
    private Vector3 cameraStartPosition;


    private void OnEnable()
    {
        ActionManager.OnResetCamera += ResetCamera;
    }

    private void OnDisable()
    {
        ActionManager.OnResetCamera -= ResetCamera;
    }

    private void Start()
    {
        cameraStartPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        transform.position = cameraStartPosition;
        currentPositionY = cameraStartPosition.y;
    }

    private void Update()
    {
        if (Mathf.Abs(player.position.y - currentPositionY) >= thresholdY)
        {
            if (player.position.y > StartThresholdY) currentPositionY = player.position.y;
            else currentPositionY = cameraStartPosition.y;
        }
        SmoothFollow();
    }


    private void SmoothFollow()
    {
        Vector3 Position = new Vector3(player.position.x + offset.x, currentPositionY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, Position, smoothSpeed * Time.deltaTime);
    }


    public void ResetCamera()
    {
        transform.position = cameraStartPosition;
    }

}
