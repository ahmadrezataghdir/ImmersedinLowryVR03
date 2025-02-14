using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHeadBob : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the XR Rig's camera
    public float bobFrequency = 2f;  // How fast the head bobs
    public float bobHeight = 0.05f;  // How much vertical movement
    public float bobSideMovement = 0.02f; // Horizontal sway during movement
    public float motionThreshold = 0.1f; // Minimum movement to trigger bobbing

    private CharacterController characterController;
    private Vector3 originalCameraPosition;
    private float bobTimer = 0f;

    void Start()
    {
        // Get the CharacterController for movement data
        characterController = GetComponent<CharacterController>();
        if (cameraTransform != null)
        {
            originalCameraPosition = cameraTransform.localPosition;
        }
    }

    void Update()
    {
        // Check if the player is moving
        if (characterController.velocity.magnitude > motionThreshold)
        {
            bobTimer += Time.deltaTime * bobFrequency;

            // Calculate the bobbing effect
            float verticalOffset = Mathf.Sin(bobTimer) * bobHeight;
            float horizontalOffset = Mathf.Cos(bobTimer) * bobSideMovement;

            // Apply the bobbing effect to the camera
            cameraTransform.localPosition = originalCameraPosition + new Vector3(horizontalOffset, verticalOffset, 0);
        }
        else
        {
            // Reset camera position when stationary
            bobTimer = 0f;
            cameraTransform.localPosition = originalCameraPosition;
        }
    }
}
