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
    GameObject gameOverScreen;

    [SerializeField]
    TextMeshProUGUI winnerText;

    [SerializeField]
    Button spawnButton, gateButton;

    GameManager gameManager;

    SpawnButtonBehaviour spawnButtonBehaviour;

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
}
