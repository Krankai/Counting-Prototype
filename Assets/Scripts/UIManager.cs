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

    float uiEffectXOffset = 3f;
    float uiEffectZ = 5f;

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
        }

        winnerText.color = GetCombineColor(winnerColors);
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

    public void ShowCountEffect(BoxColor boxColor)
    {
        var colorCountText = GetColorUI(boxColor);
        if (colorCountText == null) return;

        // Determine screen position for effect
        Vector3 screenShownPosition = colorCountText.transform.position;
        screenShownPosition.x -= colorCountText.rectTransform.rect.width + uiEffectXOffset;
        screenShownPosition.z = uiEffectZ;

        // Transform to world space position
        Vector3 worldShownPosition = Camera.main.ScreenToWorldPoint(screenShownPosition);

        // Show effect
        Instantiate(countEffectPrefab, worldShownPosition, countEffectPrefab.transform.rotation);
    }

    TextMeshProUGUI GetColorUI(BoxColor boxColor)
    {
        switch (boxColor)
        {
            case BoxColor.Red:
                return redCountText;
            case BoxColor.Blue:
                return blueCountText;
            case BoxColor.Green:
                return greenCountText;
            case BoxColor.Yellow:
                return yellowCountText;
            default:
                return null;
        }
    }

    Color GetCombineColor(List<BoxColor> winnerColors)
    {
        Color accumulatedColor = new Color();

        foreach (var color in winnerColors)
        {
            switch (color)
            {
                case BoxColor.Red:
                    accumulatedColor += colorRedWin;
                    break;
                case BoxColor.Blue:
                    accumulatedColor += colorBlueWin;
                    break;
                case BoxColor.Green:
                    accumulatedColor += colorGreenWin;
                    break;
                case BoxColor.Yellow:
                    accumulatedColor += colorYellowWin;
                    break;
            }
        }

        accumulatedColor /= winnerColors.Count;
        return accumulatedColor;
    }
}
