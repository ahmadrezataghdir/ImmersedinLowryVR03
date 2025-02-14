using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.UI;
using System.Collections;

public class FallEffect : MonoBehaviour
{
    public GameObject xrRig; // Reference to the XR Rig
    private float fallHeight = 1f; // Height from which the XR Rig will fall
    private float fallDuration = 1f; // Duration of the fall
    public AudioClip fallSound; // Optional sound effect for falling
    private AudioSource audioSource;

    private Vector3 originalPosition;
    private Vector3 startPosition;

    private float shakeMagnitude = 0.05f; // Magnitude of the camera shake
    private float shakeDuration = 0.2f; // Duration of the shake effect

    private void Start()
    {
        // Set initial position of the XR Rig
        startPosition = xrRig.transform.position;
        originalPosition = xrRig.transform.position;

        audioSource = GetComponent<AudioSource>();



        // Start the fall effect
        StartCoroutine(FallCoroutine());
    }

    private IEnumerator FallCoroutine()
    {
        // Start the fall at the given height
        Vector3 fallEndPosition = startPosition - new Vector3(0, fallHeight, 0); // Falling downward
        float elapsedTime = 0f;

        // Gradually move the XR Rig down to simulate the fall
        while (elapsedTime < fallDuration)
        {
            xrRig.transform.position = Vector3.Lerp(startPosition, fallEndPosition, elapsedTime / fallDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Play fall sound if specified
        if (fallSound != null)
        {
    
            audioSource.PlayOneShot(fallSound);
        }

        // Ensure the final position is set correctly
        xrRig.transform.position = fallEndPosition;
        originalPosition = fallEndPosition;
        StartCoroutine(CameraShake());
    }

    private IEnumerator CameraShake()
    {
        float elapsedTime = 0f;

        // Shake the camera at the point of landing
        while (elapsedTime < shakeDuration)
        {
            // Generate random shake offset within the magnitude range
            Vector3 shakeOffset = new Vector3(
                Random.Range(-shakeMagnitude, shakeMagnitude),
                Random.Range(-shakeMagnitude, shakeMagnitude),
                Random.Range(-shakeMagnitude, shakeMagnitude)
            );

            // Apply the shake to the XR Rig's position
            xrRig.transform.position = shakeOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset position to original once shaking is done
        xrRig.transform.position = originalPosition;
    }
}

