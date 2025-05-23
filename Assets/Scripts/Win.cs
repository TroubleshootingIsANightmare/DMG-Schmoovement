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
using Dan.Main;
using UnityEngine.SceneManagement;


public class Win : MonoBehaviour
    {
        public bool win = false;
        public Timer timer;
        public OptionsManager optionsManager;
        public EventManager eventManager;
        float winTime;
        float bestTime;
        private async void Awake()
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

        }
        private void Start()
        {
            eventManager = FindAnyObjectByType<EventManager>();
            optionsManager = FindAnyObjectByType<OptionsManager>();
        }
        private void FixedUpdate()
        {
            timer = GameObject.Find("/Player GUI/Timer").GetComponent<Timer>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                FinishLevel();
            }
        }

        public void FinishLevel()
        {
            win = true;
            winTime = timer.returnTime();
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                break;
            case 1:
                bestTime = optionsManager.GetTutorialTime();
                optionsManager.SetTutorialTime(Mathf.Min(winTime, bestTime));
                break;
            case 2:
                bestTime = optionsManager.GetLevelOneTime();
                optionsManager.SetLevelOneTime(Mathf.Min(winTime, bestTime));
                break;
            case 3:
                bestTime = optionsManager.GetLevelTwoTime();
                optionsManager.SetLevelTwoTime(Mathf.Min(winTime, bestTime));
                break;
        }
            
        }

    }
