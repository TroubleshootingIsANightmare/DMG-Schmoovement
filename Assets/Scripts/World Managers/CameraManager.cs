using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public OptionsManager options;
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
        options = FindObjectOfType<OptionsManager>();

        if (options != null)
           {
              sensX = options.GetSensX();
              sensY = options.GetSensY();
           }
        if (SceneManager.GetActiveScene().buildIndex == 0) { ResetRotation(); }
        if (SceneManager.GetActiveScene().buildIndex != 0) { TurnCam(); }

    }

    public void ResetRotation()
    {
        yRotation = 0f; xRotation = 0f;
    }

    public void SetPosition()
    {
        playerCam.position = camPos.position;
    }



    public void TurnCam()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;
        Debug.Log(mouseX);
        Debug.Log(mouseY);
        yRotation += mouseX;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        SetPosition();
        playerCam.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        playerPos.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    

}
