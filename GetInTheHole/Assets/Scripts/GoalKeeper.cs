using UnityEngine;
using UnityEngine.Events;

public class GoalKeeper : MonoBehaviour
{
    private ParticleSystem winParticle;

    private AudioSource audioSource;

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
            Manager.instance.FinishLevel(true);
            enter.Invoke();
        }
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
