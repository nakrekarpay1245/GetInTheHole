using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float throwForce;

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

    [SerializeField]
    private AudioSource audioSource;
    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
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
                directionDisplay.gameObject.SetActive(false);
                direction = previousPosition - currentPosition;
                arrowDisplay.gameObject.SetActive(false);
                ThrowCylinder();
            }
        }
    }

    private void ThrowCylinder()
    {
        rigidbodyComponent.AddForce(direction * throwForce, ForceMode.Impulse);
        PlayKickSound();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("WrongWall"))
        {
            Manager.instance.DecreaseHealth();
            // Debug.Log("WrongWall"); 
            PlayKickSound();
        }
        if (other.gameObject.CompareTag("NormalWall"))
        {
            // Debug.Log("WrongWall");
            PlayKickSound();
        }
    }

    private void PlayKickSound()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
}
