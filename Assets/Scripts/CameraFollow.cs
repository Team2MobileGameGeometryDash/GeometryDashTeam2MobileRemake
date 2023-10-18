using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float smoothSpeed = 0.125f;
    public Vector3 Offset;



    private void Update()
    {
        HandleCameraFollow();
    }


    private void HandleCameraFollow()
    {
        Vector3 desiredPosition = Target.position + Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        smoothedPosition.z = transform.position.z;

        transform.position = smoothedPosition;
    }

}
