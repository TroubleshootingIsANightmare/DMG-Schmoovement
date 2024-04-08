using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;
    public Transform playerCam;
    public Transform camPos;
    public Transform playerPos;
    public Camera cam;
    public float yRotation;
    public float xRotation;
    public static CameraManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(playerCam.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        SetPosition();
    }



    public void SetPosition()
    {
        playerCam.position = camPos.position;
    }

    public void LockCursor()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void TurnCam()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;

        yRotation += mouseX;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        SetPosition();
        playerCam.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        playerPos.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    
}
