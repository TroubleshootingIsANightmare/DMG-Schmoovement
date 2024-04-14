using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Unity.Mathematics;

public class RotateBody : MonoBehaviour
{
    public Transform target, body, gun;
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
        // Project the direction onto the Y plane (ignore horizontal movement)
        direction.x = 0; direction.z = 0;

        // Get the rotation that looks along the direction
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Apply the rotation to the body's transform
        body.localRotation = Quaternion.Euler(-180, 0, -direction.y);
    }
}
