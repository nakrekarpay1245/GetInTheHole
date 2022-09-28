using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerPlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float changeDuration;

    [SerializeField]
    private GameObject player;

    void Start()
    {
        InvokeRepeating("ChangeSpeed", changeDuration / 2, changeDuration);
    }

    void Update()
    {
        player.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void ChangeSpeed()
    {
        speed *= -1;
    }
}
