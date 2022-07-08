using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI inStockText;

    [SerializeField]
    TextMeshProUGUI redCountText, greenCountText, blueCountText, yellowCountText;

    [SerializeField]
    GameObject countEffectPrefab;

    [SerializeField]
    GameObject gameOverScreen;

    [SerializeField]
    TextMeshProUGUI winnerText;

    [SerializeField]
    Button spawnButton, gateButton;

    GameManager gameManager;

    SpawnButtonBehaviour spawnButtonBehaviour;

    Color colorRedWin = new Color(245f / 255f, 73f / 255f, 79f / 255f);
    Color colorBlueWin = new Color(0f / 255f, 122f / 255f, 255f / 255f);
    Color colorGreenWin = new Color(0f / 255f, 214f / 255f, 40f / 255f);
    Color colorYellowWin = new Color(197f / 255f, 204f / 255f, 0f / 255f);
    Color colorMultipleWinners = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent <GameManager>();
        spawnButtonBehaviour = spawnButton.GetComponent<SpawnButtonBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnButtonBehaviour.ButtonPressed)
        {
            gameManager.SpawnObject();
        }
    }

    public void UpdateInStockText(int countInStock)
    {
        inStockText.SetText("In Stock: " + countInStock);
    }

    public void UpdateColorCountText(int redCount, int blueCount, int greenCount, int yellowCount)
    {
        redCountText.SetText("Red: " + redCount);
        blueCountText.SetText("Blue: " + blueCount);
        greenCountText.SetText("Green: " + greenCount);
        yellowCountText.SetText("Yellow: " + yellowCount);
    }

    public void UpdateCongratText(List<BoxColor> winnerColors)
    {
        if (winnerColors.Count == 1)
        {
            var winnerColor = winnerColors[0];
            winnerText.SetText("CONGRATULATION\n" + winnerColor + " WINS");

            switch (winnerColor)
            {
                case BoxColor.Red:
                    winnerText.color = colorRedWin;
                    break;
                case BoxColor.Blue:
                    winnerText.color = colorBlueWin;
                    break;
                case BoxColor.Green:
                    winnerText.color = colorGreenWin;
                    break;
                case BoxColor.Yellow:
                    winnerText.color = colorYellowWin;
                    break;
            }
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CONGRATULATION\n");
            
            for (int i = 0, max = winnerColors.Count; i < max; ++i)
            {
                sb.Append(winnerColors[i]);
                
                if (i != max - 1)
                {
                    sb.Append(" & ");
                }
            }

            sb.Append(" WIN");

            winnerText.SetText(sb.ToString());
            winnerText.color = colorMultipleWinners;
        }
    }

    public void OnGateButtonPressed()
    {
        gameManager.ToggleGate();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void OnRestartButtonPressed()
    {
        gameManager.RestartGame();
    }

    public void DisableMainScreenButton()
    {
        spawnButton.interactable = false;
        gateButton.interactable = false;
    }

    void ShowCountEffect(BoxColor boxColor)
    {
        // Determine screen position for effect
        Vector3 screenShownPosition = redCountText.transform.position;
        switch (boxColor)
        {
            case BoxColor.Red:
                screenShownPosition = redCountText.transform.position;
                break;
            case BoxColor.Blue:
                screenShownPosition = blueCountText.transform.position;
                break;
            case BoxColor.Green:
                screenShownPosition = greenCountText.transform.position;
                break;
            case BoxColor.Yellow:
                screenShownPosition = yellowCountText.transform.position;
                break;
        }

        // Transform to world space position
        Vector3 worldShownPosition = Camera.main.ScreenToWorldPoint(screenShownPosition);

        // Show effect
        Instantiate(countEffectPrefab, worldShownPosition, countEffectPrefab.transform.rotation);
    }
}
