using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target; 
    public float smoothSpeed;
    public Vector3 Offset;


    private void LateUpdate()
    {
        HandleCameraFollow();
    }


    private void HandleCameraFollow()
    {
        Vector3 desiredPosition =  new Vector3(Target.position.x + Offset.x,Offset.y,0f);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * 100f * Time.deltaTime);
        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;
    }
}
