using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float force = 15f;
    public GameObject effect;
    private PlayerHealth playerHealth;
    private void Start()
    {

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Enemy.toTarget * force, ForceMode.Impulse);
    }







    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            playerHealth = collision.collider.GetComponent<PlayerHealth>();
            playerHealth.Damage();
            Destroy(gameObject);
        }
    }
}
