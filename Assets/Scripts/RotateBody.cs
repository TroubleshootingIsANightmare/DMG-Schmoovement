using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Unity.Mathematics;

public class RotateBody : MonoBehaviour
{
    public Transform target, body;
    public CameraManager cameraManager;

    void LateUpdate()
    {
        GetMiddle();
        RotBody();
    }


    void GetMiddle()
    {
        Camera cam = cameraManager.cam;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 targetPoint;
        if (Physics.Raycast(ray, 75f))
        {
            targetPoint = ray.GetPoint(75);
            target.position = targetPoint;
            
        } else
        {
            targetPoint = ray.origin + (ray.direction * 75f);
            target.position = targetPoint;
        }
    }

    void RotBody()
    {
        // Get the direction from this object to the target
        Vector3 direction = target.position - body.position;

        // Since the body has a -180x rotation, adjust the forward direction accordingly
        Vector3 adjustedDirection = Quaternion.Euler(0, 0, -11) * direction;

        // Project the direction onto the Y plane (ignore horizontal movement)
        adjustedDirection.x = 0; adjustedDirection.z = 0;

        // Get the rotation that looks along the direction
        Quaternion rotation = Quaternion.LookRotation(adjustedDirection);

        // Apply the rotation to the body's transform
        body.localRotation = Quaternion.Euler(-180, 0, -adjustedDirection.y);
    }
}
