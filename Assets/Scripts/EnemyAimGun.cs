using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimGun : MonoBehaviour
{
    public Enemy enemy;
    public float rotationSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = enemy.target.position - transform.position;

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
