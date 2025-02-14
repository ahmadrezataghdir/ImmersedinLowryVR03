using UnityEngine;

public class CanvasPivotToCamera : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera

    void Update()
    {
        // Check if the main camera reference is assigned
        if (mainCamera != null)
        {

            // Set canvas rotation to match the camera rotation, but keep it upright
            Vector3 targetRotation = mainCamera.transform.rotation.eulerAngles;
            targetRotation.x = 0f; // Ensure canvas stays upright
            targetRotation.z = 0f; // Ensure canvas stays upright
            transform.rotation = Quaternion.Euler(targetRotation);
        }
        else
        {
            Debug.LogWarning("Please assign the main camera reference in the inspector!");
        }
    }
}

