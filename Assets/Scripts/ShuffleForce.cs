using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a small explosion force to shuffle gameobjects
public class ShuffleForce : MonoBehaviour
{
    [SerializeField, Range(0f, 1000f)]
    float explosionForce = 200f;

    [SerializeField, Range(1f, 10f)]
    float explosionRadius = 5f;

    [SerializeField, Range(0f, 5f)]
    float upwardModifier = 0f;

    [SerializeField, Range(0f, 10f)]
    float delay = 2f;

    [SerializeField]
    bool repeat = false;

    [SerializeField, Range(1f, 10f)]
    float interval = 3f;

    // Start is called before the first frame update
    void Start()
    {
        if (repeat)
        {
            InvokeRepeating("ApplyExplosionForce", delay, interval);
        }
        else
        {
            Invoke("ApplyExplosionForce", delay);
        }
    }

    void ApplyExplosionForce()
    {
        Vector3 position = transform.position;
        Collider[] colliders = Physics.OverlapSphere(position, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, position, explosionRadius, upwardModifier);
            }
        }
    }
}
