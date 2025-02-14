using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRWalkToTarget : MonoBehaviour
{
    public XRController vrController; // Assign the VR controller (Action-based controller)
    public Transform vrCharacter; // VR character's transform
    public NavMeshAgent navAgent; // NavMeshAgent for movement
    public Animator animator; // Animator for character animations
    public LayerMask teleportationLayer; // Layer for valid teleportation targets
    public XRRayInteractor rayInteractor; // Assign your XR Ray Interactor

    private bool isMoving = false;
    private Vector3 targetPosition;

    void Update()
    {
        // Cast a ray to check for teleportation targets
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (((1 << hit.collider.gameObject.layer) & teleportationLayer) != 0)
            {
                targetPosition = hit.point; // Set the target position

                // Detect Grab or Pick button press
                if (IsGrabPressed()) // Replace with your mapped input action
                {
                    StartWalking();
                }
            }
        }

        // Handle walking
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private bool IsGrabPressed()
    {
        if (vrController.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool isPressed))
        {
            return isPressed;
        }
        return false;
    }

    private void StartWalking()
    {
        isMoving = true;
        navAgent.SetDestination(targetPosition); // Set the destination for NavMeshAgent
        animator.SetBool("IsWalking", true); // Trigger walking animation
    }

    private void MoveToTarget()
    {
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            isMoving = false;
            animator.SetBool("IsWalking", false); // Stop walking animation
        }
    }
}
