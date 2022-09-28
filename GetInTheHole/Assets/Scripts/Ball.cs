using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float throwForce;
    [SerializeField]
    private float minimumVelocity = 0.5f;

    [SerializeField]
    private Rigidbody rigidbodyComponent;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private LineRenderer directionDisplay;
    [SerializeField]
    private GameObject arrowDisplay;

    [SerializeField]
    private Vector3 previousPosition;
    [SerializeField]
    private Vector3 currentPosition;
    [SerializeField]
    private Vector3 direction;

    public Vector3 startPosition;
    public bool canForce;

    [SerializeField]
    private AudioSource audioSource;
    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        startPosition = transform.position;
        canForce = true;
        StartCoroutine(ResetControl());
    }

    private void Update()
    {
        if (!Manager.instance.isLevelFinished)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (canForce)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        previousPosition = new Vector3(raycastHit.point.x, 1, raycastHit.point.z);
                    }

                    else if (Input.GetKey(KeyCode.Mouse0))
                    {
                        currentPosition = new Vector3(raycastHit.point.x, 1, raycastHit.point.z);
                        direction = previousPosition - currentPosition;
                        directionDisplay.gameObject.SetActive(true);

                        directionDisplay.SetPosition(0, transform.position);
                        directionDisplay.SetPosition(1, transform.position - direction);

                        arrowDisplay.gameObject.SetActive(true);
                        arrowDisplay.transform.rotation = Quaternion.LookRotation(direction.normalized);
                    }


                    else if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        CanNotForce();
                        rigidbodyComponent.isKinematic = false;
                        directionDisplay.gameObject.SetActive(false);
                        direction = previousPosition - currentPosition;
                        arrowDisplay.gameObject.SetActive(false);
                        ThrowCylinder();
                    }
                }
            }
        }
    }

    private IEnumerator ResetControl()
    {
        while (!canForce)
        {
            Debug.Log("Reset Control");
            yield return new WaitForSeconds(0.15f);

            if (Mathf.Abs(rigidbodyComponent.velocity.magnitude) <= minimumVelocity && !canForce)
            {
                ResetBall();
            }

            if (Mathf.Abs(rigidbodyComponent.velocity.magnitude) <= (minimumVelocity * 5))
            {
                rigidbodyComponent.velocity = Vector3.MoveTowards(rigidbodyComponent.velocity,
                    Vector3.zero, Time.deltaTime * 0.25f);
            }
        }
    }

    private void ThrowCylinder()
    {
        rigidbodyComponent.AddForce(direction * throwForce, ForceMode.Impulse);
        PlayKickSound();
        StartCoroutine(ResetControl());
    }

    private void CanForce()
    {
        Debug.Log("CAN");
        canForce = true;
    }

    private void CanNotForce()
    {
        // Debug.Log("NOT");
        canForce = false;
    }

    private void ResetBall()
    {
        Debug.Log("RESET");
        transform.DOMove(startPosition, 0);

        rigidbodyComponent.isKinematic = true;

        Manager.instance.DecreaseHealth();

        CanForce();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("OppositePlayer"))
        {
            Debug.Log("OppositePlayer");
            PlayKickSound();
            ResetBall();
        }

        if (other.gameObject.CompareTag("NormalPlayer"))
        {
            Debug.Log("NormalPlayer");
            PlayKickSound();
        }

        if (other.gameObject.CompareTag("NormalWall"))
        {
            Debug.Log("NormalWall");
            PlayKickSound();
        }

        if (other.gameObject.CompareTag("KickerPlayer"))
        {
            Debug.Log("KickerPlayer");
            PlayKickSound();
        }
    }

    private void PlayKickSound()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
}
