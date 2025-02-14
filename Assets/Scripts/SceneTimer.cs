using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
   [SerializeField] private float timeRemaining = 60f;    
    public TMP_Text timerText;
    [SerializeField] private int nextSceneName ; // Scene to load

    [Header("Audio Clips")]
    [SerializeField] private AudioClip tickSound;
    [SerializeField] private AudioClip warningSound;   
    [SerializeField] private AudioClip timerOverSound;

    [Header("Prefab to Instantiate")]
    public GameObject warningPrefab;     // Prefab to instantiate at 5 seconds
    public Transform instantiatePosition; // Position to spawn prefab

    private AudioSource audioSource;     // AudioSource to play sounds
    private bool warningTriggered = false; // Flag for warning state

    void Start()
    {
        // Add AudioSource component dynamically
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();

            // Trigger warning at 8 seconds
            if (timeRemaining <= 8f && !warningTriggered)
            {
                TriggerWarning();
                warningTriggered = true;
            }
        }
        else
        {
            LoadNextScene();
        }
    }

    void UpdateTimerDisplay()
    {
        // Update the TextMeshPro UI
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = "Time Left: " + seconds.ToString() + "s";
    }

    void PlayTickSound()
    {
        // Play ticking sound
        if (tickSound != null)
        {
            audioSource.PlayOneShot(tickSound);
        }
    }

    void TriggerWarning()
    {
        // Play warning sound
        if (warningSound != null)
            audioSource.PlayOneShot(warningSound);

        if (timerOverSound != null)
            audioSource.PlayOneShot(timerOverSound);
        // Instantiate the warning prefab
        if (warningPrefab != null && instantiatePosition != null)
        {
            //Instantiate(warningPrefab, instantiatePosition.position, Quaternion.identity);
            GameObject warningObject = Instantiate(warningPrefab, instantiatePosition.position, Quaternion.identity);
            FollowPosition followScript = warningObject.GetComponent<FollowPosition>();
            if (followScript != null && instantiatePosition != null)
            {
                followScript.target = instantiatePosition; // Set the XR rig as the target
            }
        }
    }

    void LoadNextScene()
    {
    
        SceneManager.LoadScene(nextSceneName);
    }
}
