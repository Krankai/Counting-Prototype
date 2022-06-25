using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField]
    bool isEnable = true;

    [SerializeField]
    Axis pivotAxis = Axis.y;

    [SerializeField]
    float speed = 1.5f;

    Vector3 pivotVector;

    // Start is called before the first frame update
    void Start()
    {
        pivotVector = transform.up;
        if (pivotAxis == Axis.x)
        {
            pivotVector = transform.right;
        }
        else if (pivotAxis == Axis.z)
        {
            pivotVector = transform.forward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnable)
        {
            transform.Rotate(pivotVector, Time.deltaTime * speed * Mathf.Rad2Deg);
        }
    }
}

public enum Axis
{
    x,
    y,
    z,
}
