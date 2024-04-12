using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Unity.Mathematics;

public class RotateBody : MonoBehaviour
{
    public Transform head, target, body;
    public CameraManager cameraManager;

    void LateUpdate()
    {
        GetMiddle();
        RotBody();
        head.LookAt(target);
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
        var lookPos = target.position - body.position; 
        lookPos.z = 0; var rotation = Quaternion.LookRotation(lookPos); 
        body.rotation = Quaternion.Slerp(body.rotation, rotation, 0.1f);
    }
}
