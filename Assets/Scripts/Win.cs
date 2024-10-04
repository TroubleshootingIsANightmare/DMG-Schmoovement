using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Services.Leaderboards;
using UnityEngine.SocialPlatforms.Impl;
using System.Threading.Tasks;
using Unity.Services.Leaderboards.Models;
using Unity.Services.Authentication;
using Unity.Services.Core;

public class Win : MonoBehaviour
{
    public bool win = false;
    public Timer timer;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void FixedUpdate()
    {
        timer = GameObject.Find("/Player GUI/Timer").GetComponent<Timer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FinishLevel();
        }
    }

    public void FinishLevel()
    {
        win = true;
        AddScore();
        
    }
    public async void AddScore()
    {
        
        var playerEntry = await LeaderboardsService.Instance
            .AddPlayerScoreAsync("TutorialSpeed", timer.returnTime());
    }
}
