using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject ballContainer;

    public int RedCount { get; set; }
    public int BlueCount { get; set; }
    public int GreenCount { get; set; }
    public int YellowCount { get; set; }

    bool onHoldSpawnButton = false;

    float spawnCooldown = 0f;

    private SpawnManager spawnManager;
    private GateBehaviour gateBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gateBehaviour = ballContainer.GetComponent<GateBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            onHoldSpawnButton = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            onHoldSpawnButton = false;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            gateBehaviour.ToggleGate();
        }

        spawnCooldown -= Time.deltaTime;
    }

    void LateUpdate()
    {
        if (onHoldSpawnButton && spawnCooldown < 0)
        {
            spawnManager.SpawnSingle();
            spawnCooldown = spawnManager.spawnCooldown;
        }
    }

    public void ShowColorCount()
    {
        Debug.LogFormat("R: {0}, B: {1}, G: {2}, Y: {3}", RedCount, BlueCount, GreenCount, YellowCount);
    }
}
