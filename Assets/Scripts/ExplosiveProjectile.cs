using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ExplosiveProjectile : MonoBehaviour
{
    public float power = 500f;
    public float knockback = 400f;
    public float radius = 6.5f;
    public GameObject explosiveFX;
    private GameObject projectile;
    
    // Start is called before the first frame update
    void Start()
    {
        projectile = this.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {

            if (other.gameObject.tag == "Ground")
            {
                Explode();
            }
       
    }

    void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] hits = Physics.OverlapSphere(explosionPos, radius);
        Instantiate(explosiveFX, projectile.transform.position, projectile.transform.rotation);
        foreach (Collider hit in hits)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();


            if (rb != null)
            {
                Vector2 locate = new Vector2(hit.gameObject.transform.position.x, hit.gameObject.transform.position.z);
                Vector2 direction = new Vector2(locate.x - explosionPos.x, locate.y - explosionPos.z);
                direction.Normalize();
                Destroy(projectile);
                rb.AddForce(new Vector3(direction.x * knockback, 0, direction.y * knockback));
                rb.AddExplosionForce(power, explosionPos, radius, 4);
            }
        }
    }

    
}
