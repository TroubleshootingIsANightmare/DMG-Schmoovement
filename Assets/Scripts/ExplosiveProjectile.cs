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
    public Enemy enemy;
    public LayerMask canExplode;
    public int exploded = 0;
    // Start is called before the first frame update
    void Start()
    {
        projectile = this.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
            if ((other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy" ) && exploded < 2)
            {
                exploded++;
                Destroy(projectile);
                Explode();
            if (other.gameObject.tag == "Enemy") enemy.EnableRagdoll(true);
            }
       
    }

    private void Update()
    {
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    void Explode()
    {
        Debug.Log("AOKSJDAJKSDASKLDJ");
        Vector3 explosionPos = transform.position;
        Collider[] hits = Physics.OverlapSphere(explosionPos, radius, canExplode.value);
        Instantiate(explosiveFX, projectile.transform.position, projectile.transform.rotation);

        HashSet<Rigidbody> affectedRigidbodies = new HashSet<Rigidbody>();
        foreach (Collider hit in hits)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && !affectedRigidbodies.Contains(rb))
            {
                if (rb.gameObject.tag == "Enemy" && !enemy.ragdolled) enemy.EnableRagdoll(true);
                Vector2 locate = new Vector2(hit.gameObject.transform.position.x, hit.gameObject.transform.position.z);
                Vector2 direction = new Vector2(locate.x - explosionPos.x, locate.y - explosionPos.z);
                direction.Normalize();
                rb.AddForce(new Vector3(direction.x * knockback, 0, direction.y * knockback));
                rb.AddExplosionForce(power, explosionPos, radius, 4);
                affectedRigidbodies.Add(rb);
            }

        }
    }

    
}
