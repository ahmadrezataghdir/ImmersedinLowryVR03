using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public GameObject secondObject; // The second object to appear
    public float triggerFrame = 0.9f; // The normalized time when the second object appears

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool hasTriggered = false;
    private bool resetPending = false;

    private SpriteRenderer secondSpriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (secondObject != null)
        {
            secondSpriteRenderer = secondObject.GetComponent<SpriteRenderer>();
            secondObject.SetActive(false); // Ensure second object starts hidden
        }
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Trigger the second object at the defined frame
        if (stateInfo.normalizedTime >= triggerFrame && !hasTriggered && !animator.IsInTransition(0))
        {
            hasTriggered = true;

            // Hide the first object's sprite and deactivate it
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = null;
            }
            gameObject.SetActive(false);

            // Activate the second object
            if (secondObject != null)
            {
                secondObject.SetActive(true);
                resetPending = true;
            }
        }

        // Reset hasTriggered when animation restarts
        if (stateInfo.normalizedTime < triggerFrame)
        {
            hasTriggered = false;
        }
    }

    public void ResetObjects()
    {
        if (resetPending)
        {
            // Hide the second object's sprite before disabling it
            if (secondSpriteRenderer != null)
            {
                secondSpriteRenderer.sprite = null;
            }

            // Fully deactivate both objects
            gameObject.SetActive(false);
            secondObject.SetActive(false);

            // Small delay before reactivating the first object to ensure clean transition
            Invoke("ReactivateFirstObject", 0.1f);

            resetPending = false;
        }
    }

    private void ReactivateFirstObject()
    {
        gameObject.SetActive(true);
    }
}





