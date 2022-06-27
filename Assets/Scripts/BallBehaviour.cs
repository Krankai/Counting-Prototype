using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public bool InsideBox { get; set; }

    [SerializeField]
    float bounceBackForce = 2.0f;

    [SerializeField, Range(0f, 90f)]
    float maxGroundAngle = 30f;

    Rigidbody rb;

    float minGroundNormal;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        rb = GetComponent<Rigidbody>();
        OnValidate();
    }

    void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        HandleCollision(collision);
    }

    // Handle collision behaviour of the ball when comes into contact with the interior of the box
    void HandleCollision(Collision collision)
    {
        if (InsideBox && collision.gameObject.CompareTag("Box"))
        {
            // Apply bouncing force when come into contact with 'wall' only
            for (int i = 0, max = collision.contactCount; i < max; ++i)
            {
                var contactNormal = collision.GetContact(i).normal;
                if (contactNormal.y <= minGroundNormal)     // not 'ground'
                {
                    rb.AddForce(contactNormal * bounceBackForce, ForceMode.Impulse);
                }
                else
                {
                    // Eliminate velocity along the y axis if touch 'ground'
                    float velocityX = rb.velocity.x;
                    float velocityZ = rb.velocity.z;

                    rb.velocity = new Vector3(velocityX, 0f, velocityZ);
                }
            }
        }
    }

    void OnValidate()
    {
        // note: equals min dot product
        minGroundNormal = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
    }
}
