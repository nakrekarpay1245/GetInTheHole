using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject ball_1;
    [SerializeField]
    private GameObject ball_2;
    [SerializeField]
    private GameObject ball_3;

    [SerializeField]
    private float health;

    public static Manager instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void DecreaseHealth()
    {
        health--;
        DisplayBalls();
    }

    private void DisplayBalls()
    {
        switch (health)
        {
            case 0:
                ball_1.SetActive(false);
                break;
            case 1:
                ball_2.SetActive(false);
                break;
            case 2:
                ball_3.SetActive(false);
                break;
            case 3:
                ball_1.SetActive(true);
                ball_2.SetActive(true);
                ball_3.SetActive(true);
                break;
            default:
                ball_1.SetActive(true);
                ball_2.SetActive(true);
                ball_3.SetActive(true);
                break;
        }
    }
}
