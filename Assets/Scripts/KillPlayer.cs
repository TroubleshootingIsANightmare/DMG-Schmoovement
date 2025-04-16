using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LeaderboardCreatorDemo
{
    public class KillPlayer : MonoBehaviour
    {
        EventManager eventManager;

        PlayerMovement playerMovement;
        Grapple grapple;
        WeaponManager weaponManager;
        private void Start()
        {
            eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
            playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
            weaponManager = FindFirstObjectByType<WeaponManager>();
            grapple = FindFirstObjectByType<Grapple>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Destroy(grapple.joint);
                if (weaponManager.currentWeapon == 1) {
                    LaunchProjectile projScript = weaponManager.weapons[1].GetComponent<LaunchProjectile>();
                    projScript.isCharging = false;
                    projScript.chargeTime = 0;
                }
                weaponManager.SwitchWeapon(0);
                playerMovement.spawned = false;
                Timer time = eventManager.timer.gameObject.GetComponent<Timer>();
                time.i = 0f;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                eventManager.died = true;
                playerMovement.spawned = false;
                Timer time = eventManager.timer.gameObject.GetComponent<Timer>();
                time.i = 0f;
            }
        }

    }
}