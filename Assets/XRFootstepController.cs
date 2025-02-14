using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRFootstepController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;
    public float stepInterval = 0.5f;

    private float stepTimer;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        stepTimer = stepInterval;
    }

    void Update()
    {
        if (characterController.velocity.magnitude > 0.1f) // Player is moving
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = stepInterval; // Reset timer when stationary
        }
    }

    void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            int index = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[index]);
        }
    }
}

