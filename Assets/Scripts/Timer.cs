using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using Unity.Services.Leaderboards;


    public class Timer : MonoBehaviour
    {
        public TMP_Text _time;
        public float i;
        public Win win;
        PlayerMovement playerMovement;
        // Start is called before the first frame update
        void Start()
        {
            _time = GetComponent<TMP_Text>();
            i = 0f;
            playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }
        private void Update()
        {
            if (!playerMovement.spawned)
            {
                i = 0f;
            }
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        setTime(0);
        throw new NotImplementedException();   
    }

    // Update is called once per frame
    void FixedUpdate()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                win = GameObject.Find("GoalCube").GetComponent<Win>();
                if (!win.win)
                {
                    i += 1f / 50f;
                    _time.text = "Time: " + String.Format("{0:0.00}", i);
                }
            }

        }


        public float returnTime()
        {
            return i;
        }
        
        public void setTime(float time)
        {
            i = time;
        }
    }
