using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTimer : MonoBehaviour
{
   [SerializeField] private float timeRemaining = 60f;    
    public TMP_Text timerText;
    [SerializeField] private int nextSceneName ; 

    [Header("Audio Clips")]
    [SerializeField] private AudioClip tickSound;
    [SerializeField] private AudioClip warningSound;   
    [SerializeField] private AudioClip timerOverSound;

    [Header("Prefab to Instantiate")]
    public GameObject warningPrefab;     
    public Transform instantiatePosition; 

    private AudioSource audioSource;     
    private bool warningTriggered = false; 

    [Header("Fog Settings")]
    public float fogMaxDensity = 0.45f;
    public float fogTransitionTime = 5f;
    public Color fogColor = Color.gray;

    void Start()
    {
        // Add AudioSource component dynamically
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
    }


    private void FogSettings()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = 0f;
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
            FogSettings();
            //LoadNextScene();

            StartCoroutine(TeleportWithFog());
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

    private IEnumerator TeleportWithFog()
    {
        
        float elapsed = 0f;
        while (elapsed < fogTransitionTime)
        {
            elapsed += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(0f, fogMaxDensity, elapsed / fogTransitionTime);
            yield return null;
        }

        
        yield return new WaitForSeconds(1f);


        LoadNextScene();
    }

    void LoadNextScene()
    {
    
        SceneManager.LoadScene(nextSceneName);
    }
}