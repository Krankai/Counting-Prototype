using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDummyTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider collide)
    {
        if (collide.CompareTag("Ball"))
        {
            var bounceBackScript = collide.GetComponent<BounceBack>();
            bounceBackScript.InsideBox = true;
            bounceBackScript.inside = true;
        }
    }
}
