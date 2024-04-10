using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Unity.Mathematics;

public class RotateBody : MonoBehaviour
{
    public Transform head, target;
    public CameraManager cameraManager;

    void LateUpdate()
    {
        GetMiddle();
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


}
