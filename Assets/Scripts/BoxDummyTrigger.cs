using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDummyTrigger : MonoBehaviour
{
    public BoxColor boxColor;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            var ballScript = other.GetComponent<BallBehaviour>();
            ballScript.InsideBox = true;
            ballScript.CurrentColor = boxColor;

            UpdateColorCount(1);
            ++gameManager.BoxedBalls;


            //Debug.Log("in: " + ballScript.BallID + ", count: " + gameManager.BoxedBalls);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            UpdateColorCount(-1);
            --gameManager.BoxedBalls;

            var ballScript = other.GetComponent<BallBehaviour>();
            ballScript.InsideBox = false;
            ballScript.CurrentColor = BoxColor.None;

            //Debug.Log("in: " + ballScript.BallID + ", count: " + gameManager.BoxedBalls);
        }
    }

    void UpdateColorCount(int value)
    {
        gameManager.UpdateColorCount(value, boxColor);
    }
}

public enum BoxColor
{
    None,
    Red,
    Blue,
    Green,
    Yellow,
}
