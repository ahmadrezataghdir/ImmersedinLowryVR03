using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;

public class InteractableTeleportAnchor : MonoBehaviour
{
    public XRBaseInteractable teleportAnchor; // Reference to the teleport anchor
    public Transform character; // Reference to your character (Player)
    public NavMeshAgent navMeshAgent; // Reference to NavMeshAgent for movement
    public Animator characterAnimator; // Reference to Animator to play walking animation

    public float walkSpeed = 3f; // Speed for walking

    private bool isWalking = false;

void Start()
    {
        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }

    private void OnEnable()
    {
        if (teleportAnchor != null)
        {
            teleportAnchor.onHoverEntered.AddListener(OnHoverEntered);
            teleportAnchor.onSelectEntered.AddListener(OnSelectEntered);
        }
    }

    private void OnDisable()
    {
        if (teleportAnchor != null)
        {
            teleportAnchor.onHoverEntered.RemoveListener(OnHoverEntered);
            teleportAnchor.onSelectEntered.RemoveListener(OnSelectEntered);
        }
    }

    // When the teleport anchor is hovered over
    private void OnHoverEntered(XRBaseInteractor interactor)
    {
        // Optionally, show hover effects or feedback to the player
        Debug.Log("Hovering over teleport anchor.");
    }

    // When the teleport anchor is selected (grabbed or pressed)
    private void OnSelectEntered(XRBaseInteractor interactor)
    {
        // Start walking to the teleport destination instead of teleporting
        Debug.Log("Select over teleport anchor.");
        StartWalkingToAnchor();

        // Disable teleportation by removing the default teleportation behavior
        DisableTeleportation(interactor);
    }

    // Start walking animation and move the character to the anchor position
    private void StartWalkingToAnchor()
    {
        if (teleportAnchor != null && navMeshAgent != null && character != null)
        {
           

            // Set the destination of the NavMeshAgent
            navMeshAgent.SetDestination(teleportAnchor.transform.position);
            navMeshAgent.speed = walkSpeed;
            navMeshAgent.isStopped = false;

            // Start walking, and check if we've reached the destination
            isWalking = true;
        }
    }

    // Stop teleportation from happening
    private void DisableTeleportation(XRBaseInteractor interactor)
    {
        // If you have a teleportation provider, you can disable its teleportation functionality.
        // Assuming teleportation is handled by the XR Interaction Toolkit, we prevent teleportation here.
        interactor.enabled = false; // This stops the teleport interaction from proceeding.
    }

    // Update is called once per frame to check if we should stop walking
    private void Update()
    {
if (!navMeshAgent.hasPath)
{
    Debug.LogError("NavMeshAgent has no valid path.");
}
else
{
    Debug.Log("NavMeshAgent is moving.");
}

        if (isWalking && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // Stop walking when destination is reached
            StopWalking();
        }
    }

    // Stop walking and animation once the destination is reached
    private void StopWalking()
    {
        

        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;  // Stop the NavMeshAgent
        }

        isWalking = false;
    }
}
