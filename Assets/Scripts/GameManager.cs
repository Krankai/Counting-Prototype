using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject ballContainer;

    [SerializeField]
    GameObject platform;

    public bool IsGameRunning { get; set; }

    public int BoxedBalls { get; set; }         // number of balls successfully lie within the boxes

    public int InStockBalls { get; private set; }       // number of balls that can still be spawned

    public int RedCount { get; set; }
    public int BlueCount { get; set; }
    public int GreenCount { get; set; }
    public int YellowCount { get; set; }

    bool onProcessingFinish = false;

    float spawnCooldown = 0f;

    int totalBalls;

    float originalTimeScale;

    SpawnManager spawnManager;

    UIManager uiManager;

    AudioManager audioManager;

    GateBehaviour gateBehaviour;

    AutoRotate containerRotateScript;

    AutoRotate platformRotateScript;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        uiManager = GameObject.Find("UserInterface").GetComponent<UIManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        gateBehaviour = ballContainer.GetComponent<GateBehaviour>();
        containerRotateScript = ballContainer.GetComponent<AutoRotate>();
        platformRotateScript = platform.GetComponent<AutoRotate>();

        totalBalls = spawnManager.totalCount;

        IsGameRunning = true;
        BoxedBalls = 0;
        InStockBalls = totalBalls;

        originalTimeScale = Time.timeScale;

        uiManager.UpdateInStockText(InStockBalls);
        uiManager.UpdateColorCountText(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        spawnCooldown -= Time.deltaTime;

        CheckGameFinish();
    }

    public void UpdateColorCount(int value, BoxColor color)
    {
        switch (color)
        {
            case BoxColor.Red:
                RedCount += value;
                break;
            case BoxColor.Blue:
                BlueCount += value;
                break;
            case BoxColor.Green:
                GreenCount += value;
                break;
            case BoxColor.Yellow:
                YellowCount += value;
                break;
        }

        uiManager.UpdateColorCountText(RedCount, BlueCount, GreenCount, YellowCount);
        uiManager.ShowCountEffect(color);

        if (value > 0)
        {
            audioManager.PlayCountSound();
        }

        // Debug
        //ShowColorCount();
    }

    // void ShowColorCount()
    // {
    //     Debug.LogFormat("R: {0}, B: {1}, G: {2}, Y: {3}", RedCount, BlueCount, GreenCount, YellowCount);
    // }

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

        yield return new WaitForSeconds(0.5f);
        EndGame();
    }

    public void ToggleGate()
    {
        gateBehaviour.ToggleGate();
        audioManager.PlayGateSound();
    }

    public void SpawnObject()
    {
        if (IsGameRunning && spawnCooldown < 0)
        {
            spawnManager.SpawnSingle();
            spawnCooldown = spawnManager.spawnCooldown;

            if (InStockBalls > 0)
            {
                uiManager.UpdateInStockText(--InStockBalls);
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = originalTimeScale;
    }

    public void RecollectDestroyedObject(GameObject gameObject)
    {
        var ballBehaviour = gameObject.GetComponent<BallBehaviour>();
        UpdateColorCount(-1, ballBehaviour.CurrentColor);

        spawnManager.RecollectDestroyedObject(gameObject);
        uiManager.UpdateInStockText(++InStockBalls);
        audioManager.PlayRecollectSound();
    }

    void EndGame()
    {
        uiManager.UpdateCongratText(GetWinnerColor());
        uiManager.ShowGameOverScreen();
        uiManager.DisableMainScreenButton();

        audioManager.StartLoweringBGMVolume(5);
    }

    List<BoxColor> GetWinnerColor()
    {
        var countList = new List<int>();
        countList.Add(RedCount);
        countList.Add(GreenCount);
        countList.Add(BlueCount);
        countList.Add(YellowCount);

        var maxCount = Mathf.Max(countList.ToArray());

        var winnerColors = new List<BoxColor>();
        if (maxCount == RedCount)
        {
            winnerColors.Add(BoxColor.Red);
        }
        if (maxCount == BlueCount)
        {
            winnerColors.Add(BoxColor.Blue);
        }
        if (maxCount == GreenCount)
        {
            winnerColors.Add(BoxColor.Green);
        }
        if (maxCount == YellowCount)
        {
            winnerColors.Add(BoxColor.Yellow);
        }

        return winnerColors;
    }
}
