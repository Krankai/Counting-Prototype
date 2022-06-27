using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject ballContainer;

    [SerializeField]
    GameObject platform;

    public bool IsGameRunning { get; set; }

    public int BoxedBalls { get; set; }         // number of balls successfully lie within the boxes

    public int RedCount { get; set; }
    public int BlueCount { get; set; }
    public int GreenCount { get; set; }
    public int YellowCount { get; set; }

    bool onHoldSpawnButton = false;

    bool onProcessingFinish = false;

    float spawnCooldown = 0f;

    int totalBalls;

    private SpawnManager spawnManager;

    private GateBehaviour gateBehaviour;

    private AutoRotate containerRotateScript;

    private AutoRotate platformRotateScript;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gateBehaviour = ballContainer.GetComponent<GateBehaviour>();

        containerRotateScript = ballContainer.GetComponent<AutoRotate>();
        platformRotateScript = platform.GetComponent<AutoRotate>();

        totalBalls = spawnManager.totalCount;

        IsGameRunning = true;
        BoxedBalls = 0;
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

        if (IsGameRunning && Input.GetButtonDown("Fire2"))
        {
            gateBehaviour.ToggleGate();
        }

        spawnCooldown -= Time.deltaTime;

        CheckGameFinish();
    }

    void LateUpdate()
    {
        if (IsGameRunning && onHoldSpawnButton && spawnCooldown < 0)
        {
            spawnManager.SpawnSingle();
            spawnCooldown = spawnManager.spawnCooldown;
        }
    }

    public void ShowColorCount()
    {
        Debug.LogFormat("R: {0}, B: {1}, G: {2}, Y: {3}", RedCount, BlueCount, GreenCount, YellowCount);
    }

    public void CheckGameFinish()
    {
        if (IsGameRunning && !onProcessingFinish && BoxedBalls >= totalBalls)
        {
            onProcessingFinish = true;
            StartCoroutine(StopRotationRoutine());
        }
    }

    IEnumerator StopRotationRoutine()
    {
        containerRotateScript.SlowDown(0.5f);
        platformRotateScript.SlowDown(0.5f);

        yield return new WaitForSeconds(2f);
        containerRotateScript.DisableRotation();
        platformRotateScript.DisableRotation();

        onProcessingFinish = false;
        IsGameRunning = false;
    }
}
