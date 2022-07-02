using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    static int idPool = 0;

    public bool InsideBox { get; set; }
    
    public BoxColor CurrentColor { get; set; }

    public int BallID { get; private set; }

    [SerializeField]
    float bounceBackForce = 2.0f;

    [SerializeField, Range(0f, 90f)]
    float maxGroundAngle = 30f;

    float minGroundNormal;

    Rigidbody rb;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        rb = GetComponent<Rigidbody>();
        OnValidate();

        CurrentColor = BoxColor.None;

        GenerateID();
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
        //if (InsideBox && collision.gameObject.CompareTag("Box"))
        //{
        //    // Apply bouncing force when come into contact with 'wall' only
        //    for (int i = 0, max = collision.contactCount; i < max; ++i)
        //    {
        //        var contactNormal = collision.GetContact(i).normal;
        //        if (contactNormal.y <= minGroundNormal)     // not 'ground'
        //        {
        //            rb.AddForce(contactNormal * bounceBackForce, ForceMode.Impulse);
        //        }
        //        else
        //        {
        //            // Eliminate velocity along the y axis if touch 'ground'
        //            float velocityX = rb.velocity.x;
        //            float velocityZ = rb.velocity.z;

        //            rb.velocity = new Vector3(velocityX, 0f, velocityZ);
        //        }
        //    }
        //}

        // Check all collisions
        for (int i = 0, max = collision.contactCount; i < max; ++i)
        {
            // Versus "box"
            if (InsideBox && collision.gameObject.CompareTag("Box"))
            {
                var contactNormal = collision.GetContact(i).normal;

                // Eliminate velocity along y axis
                float velocityX = rb.velocity.x;
                float velocityZ = rb.velocity.z;
                rb.velocity = new Vector3(velocityX, 0f, velocityZ);

                // Apply bounce back force if touch 'wall' (not 'ground')
                if (contactNormal.y <= minGroundNormal)
                {
                    rb.AddForce(contactNormal * bounceBackForce, ForceMode.Impulse);
                }
            }
            // Versus other "balls"
            //else if (collision.gameObject.CompareTag("Ball"))
            //{
            //    // Eliminate velocity along the y axis if touch 'ball'
            //    float velocityX = rb.velocity.x;
            //    float velocityZ = rb.velocity.z;

            //    rb.velocity = new Vector3(velocityX, 0f, velocityZ);
            //}
        }
    }

    void OnValidate()
    {
        // note: equals min dot product
        minGroundNormal = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
    }

    void GenerateID()
    {
        BallID = idPool++;
    }
}
