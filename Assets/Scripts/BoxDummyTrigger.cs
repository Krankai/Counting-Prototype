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

            //Rigidbody rb = collide.GetComponent<Rigidbody>();
            //if (rb.velocity.y > 0)
            //{
            //    UpdateColorCount(-1);
            //    --gameManager.BoxedBalls;
            //    Debug.Log("out: " + gameManager.BoxedBalls);
            //}
            //else
            //{
            //    UpdateColorCount(1);
            //    ++gameManager.BoxedBalls;
            //    Debug.Log("in: " + gameManager.BoxedBalls);
            //}

            UpdateColorCount(1);
            ++gameManager.BoxedBalls;
            //Debug.Log("in: " + gameManager.BoxedBalls);
        }
    }

    void UpdateColorCount(int value)
    {
        switch (boxColor)
        {
            case BoxColor.Red:
                gameManager.RedCount += value;
                break;
            case BoxColor.Blue:
                gameManager.BlueCount += value;
                break;
            case BoxColor.Green:
                gameManager.GreenCount += value;
                break;
            case BoxColor.Yellow:
                gameManager.YellowCount += value;
                break;
        }

        gameManager.ShowColorCount();
    }
}

public enum BoxColor
{
    Red,
    Blue,
    Green,
    Yellow,
}
