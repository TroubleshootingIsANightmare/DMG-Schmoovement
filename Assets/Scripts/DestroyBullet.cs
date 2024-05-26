using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField] float lifetime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Delete", lifetime);
    }

    void Delete()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            Delete();
        }    
    }
}
