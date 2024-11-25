using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class Login : MonoBehaviour
{
    [SerializeField] private GameObject signInDisplay = default;
    [SerializeField] private TMP_InputField usernameInput = default;
    [SerializeField] private TMP_InputField passwordInput = default;
    [SerializeField] private TextMeshProUGUI errorMessageText = default;
    [SerializeField] public GameObject titleScreenCanvas;
    public EventManager eventManager;
    public float displayErrorDuration = 5f;

    async void Awake()
    {
        await UnityServices.InitializeAsync();
    }
    // Start is called before the first frame update
    void Start()
    {
        eventManager = FindAnyObjectByType<EventManager>();
        usernameInput.text = eventManager.playerName;
        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;

        if (isSignedIn)
        {
            signInDisplay.SetActive(false);
        }
    }

    public async void SignIn()
    {
        string usernameText = usernameInput.text;
        string passwordText = passwordInput.text;
        eventManager.playerName = usernameText;
        await SignInWithUsernameAndPassword(usernameText, passwordText);
        Invoke("GoToTitleScreen", displayErrorDuration);
    }

    public async void SignUp()
    {
        string usernameText = usernameInput.text;
        string passwordText = passwordInput.text;
        eventManager.playerName = usernameText;
        await SignUpWithUsernameAndPassword(usernameText, passwordText);
        Invoke("GoToTitleScreen", displayErrorDuration);
    }

    public async void SignOut()
    {
        await SignOutOfGame();
        usernameInput.text = default;
        passwordInput.text = default;
    }

    async Task SignOutOfGame()
    {
        try
        {
            AuthenticationService.Instance.SignOut(true);
            Debug.Log("User is signed out");
        }
        catch (AuthenticationException ex)
        {
            ShowErrorMessage(ex.Message);
        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

 


    async Task SignUpWithUsernameAndPassword(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            errorMessageText.text = "";
            Debug.Log("Sign-up successful");
        }
        catch (AuthenticationException ex)
        {
            ShowErrorMessage(ex.Message);
        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

    async Task SignInWithUsernameAndPassword(string username, string password)
    {

        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            errorMessageText.text = "";
            Debug.Log("Sign-in successful");
        }
        catch (AuthenticationException ex)
        {
            ShowErrorMessage(ex.Message);
        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }
    public void ShowErrorMessage(string message)
    {
        errorMessageText.text = message;
        errorMessageText.gameObject.SetActive(true);
        Invoke("HideErrorMessage", displayErrorDuration);
    }

    private void HideErrorMessage()
    {
        errorMessageText.gameObject.SetActive(false);
    }

    private void GoToTitleScreen()
    {
        if(errorMessageText.text == "") {
            signInDisplay.SetActive(false);
            titleScreenCanvas.SetActive(true);
        }
        
    }

    public string GetName()
    {
        return usernameInput.text;
    }

}
