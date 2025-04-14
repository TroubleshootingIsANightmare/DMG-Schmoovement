using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchProjectile : MonoBehaviour
{
    public EventManager eventManager;

    public GameObject projectile;
    public GameObject player;
    public GameObject shootFX;

    public Rigidbody rb;

    public Camera cam;

    public Transform camPos;
    public Transform shootPosition;
    public Transform gunHolder;

    public float bulletsFired = 1f;
    public float bulletSpeed;
    public float shootCooldown;
    public float recoil;
    public float chargeMultiplier = 100f;
    public float chargeTime;
    float shake = 0f;
    float decreaseFactor = 1f; 
    float shakeAmount = 0.05f;

    private AudioSource audioSource;

    public bool shooting;
    public bool canShoot;
    public bool useProjectile;
    public bool chargeable;
    public bool isCharging;
    public Vector3 targetPoint;
    // Start is called before the first frame update
    void Start()
    {
        shooting = false;
        canShoot = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!eventManager.paused && SceneManager.GetActiveScene().buildIndex != 0) {
            if (!chargeable) { CheckShooting(); }
            if (chargeable) { CheckCharge(); }
            ShakeCamera(); 
            shootPosition.gameObject.SetActive(canShoot); 
        }
    }

    void CheckShooting()
    {
        if(Input.GetMouseButtonDown(0) && canShoot && !eventManager.died)
        {
            shooting = true;
        } else
        {
            shooting = false;
        }
        if (shooting)
        {
            Shoot();
        }
    }

    void CheckCharge()
    {
        Debug.Log("CHARGE RUNS");
        if (Input.GetMouseButton(0))
        {
            isCharging = true;
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0f, 5f); // Prevent overcharging
        } else
        {
            isCharging = false;
        }

        if (!isCharging && chargeTime > 0f)
        {
            ApplyChargeForce();
            chargeTime -= Time.deltaTime;
        }
    }

    void ApplyChargeForce()
    {
            float appliedForce = recoil * chargeMultiplier;
            GetTarget();
            Vector3 direction = targetPoint - shootPosition.position;
            Instantiate(shootFX, shootPosition.position, gunHolder.rotation);
            rb.AddForce(-direction.normalized * appliedForce, ForceMode.VelocityChange);
        
    }
void Shoot()
    {
        GetTarget();
        shake = 0.2f;
        Vector3 direction = targetPoint - shootPosition.position;

        GameObject currentBullet = Instantiate(projectile, shootPosition.position, Quaternion.identity);
        Instantiate(shootFX, shootPosition.position, gunHolder.rotation);
        audioSource.Play();

        currentBullet.transform.forward = direction.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * (bulletSpeed), ForceMode.Impulse);

        canShoot = false;

        Invoke("ResetShooting", shootCooldown);
    }

    void GetTarget()
    {
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;

        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }
    }

    void ResetShooting()
    {
        canShoot = true;
    }

    public void ShakeCamera()
    {
        if (shake > 0f)
        {
            camPos.localPosition = new Vector3(-0.7f, 0f, 0f) + Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0.0f;
            camPos.localPosition = new Vector3(-0.7f,0f,0f);
        }
        
    }
}
