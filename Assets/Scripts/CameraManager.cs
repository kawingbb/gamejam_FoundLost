using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform targetTransform;

    public float smoothFollow = 1;
    public Vector3 offset = new Vector3(0, 0, -10);

    void Update()
    {
        Vector3 desiredPosition = targetTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothFollow);
        transform.position = desiredPosition;
    }
}
