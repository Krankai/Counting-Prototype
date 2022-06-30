using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshPro inStockText;

    [SerializeField]
    TextMeshPro redCountText, greenCountText, blueCountText, yellowCountText;

    [SerializeField]
    GameObject gameOverScreen;

    [SerializeField]
    TextMeshPro winnerText;

    [SerializeField]
    Button spawnButton, gateButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
