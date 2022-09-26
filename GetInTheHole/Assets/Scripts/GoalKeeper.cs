using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GoalKeeper : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem winParticle;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private GameObject celebrationDisplay;

    [SerializeField]
    private Image celebrationImage;

    [SerializeField]
    private Sprite[] celebrationSprites;

    [SerializeField]
    private UnityEvent enter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        winParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void EnterHole()
    {
        if (!audioSource.isPlaying)
        {
            winParticle.Play();
            audioSource.Play();
            DisplayCelebration();
            Manager.instance.FinishLevel(true);
            enter.Invoke();
        }
    }

    private void DisplayCelebration()
    {
        celebrationImage.sprite = celebrationSprites[Random.Range(0, celebrationSprites.Length)];
        celebrationDisplay.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            // Debug.Log("Win");
            EnterHole();
        }
    }
}
