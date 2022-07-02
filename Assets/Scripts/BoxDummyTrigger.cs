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


            Debug.Log("in: " + ballScript.BallID + ", count: " + gameManager.BoxedBalls);
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

            Debug.Log("in: " + ballScript.BallID + ", count: " + gameManager.BoxedBalls);
        }
    }

    void UpdateColorCount(int value)
    {
        //switch (boxColor)
        //{
        //    case BoxColor.Red:
        //        gameManager.RedCount += value;
        //        break;
        //    case BoxColor.Blue:
        //        gameManager.BlueCount += value;
        //        break;
        //    case BoxColor.Green:
        //        gameManager.GreenCount += value;
        //        break;
        //    case BoxColor.Yellow:
        //        gameManager.YellowCount += value;
        //        break;
        //}

        //gameManager.ShowColorCount();

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
