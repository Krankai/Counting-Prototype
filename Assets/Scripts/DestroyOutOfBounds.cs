using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)]
    float offsetX = 5f, offsetZ = 5f;

    [SerializeField]
    float minY = -10;

    [SerializeField]
    float maxY = 50;

    float rangeX, rangeZ;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetupXZBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        // X axis
        if (transform.position.x < -rangeX || transform.position.x > rangeX)
        {
            //DebugLogDestroyed("x");
            gameManager.RecollectDestroyedObject(gameObject);
        }

        // Y axis
        if (transform.position.y < minY || transform.position.y > maxY)
        {
            //DebugLogDestroyed("y");
            gameManager.RecollectDestroyedObject(gameObject);
        }

        // Z axis
        if (transform.position.z < -rangeZ || transform.position.z > rangeZ)
        {
            //DebugLogDestroyed("z");
            gameManager.RecollectDestroyedObject(gameObject);
        }
    }

    void SetupXZBoundaries()
    {
        GameObject platform = GameObject.Find("Platform");
        BoxCollider collider = platform.GetComponent<BoxCollider>();

        rangeX = collider.size.x + offsetX;
        rangeZ = collider.size.z + offsetZ;
    }

    // Debug
    // void DebugLogDestroyed(string axis)
    // {
    //     var script = gameObject.GetComponent<BallBehaviour>();

    //     Debug.Log("Destroyed: " + axis + ", color: " + script.CurrentColor);
    // }
}
