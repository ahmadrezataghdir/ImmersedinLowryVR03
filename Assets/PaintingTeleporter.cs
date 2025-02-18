using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PaintingTeleporter : MonoBehaviour
{
    public string targetScene;
    public GameObject portalEffectPrefab;
    public AudioClip teleportSound;
    private float effectDuration = 8f;
    private AudioSource audioSource;
    public GameObject ZonePosition;
    public Transform instantiatePosition; // Position to spawn prefab


    [Header("Fog Settings")]
    public float fogMaxDensity = 0.45f;
    public float fogTransitionTime = 5f;
    public Color fogColor = Color.gray;

    private void Start()
    {
        // Ensure the object has an AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        
    }

    public void OnSelect()
    {
        FogSettings();
        //StartCoroutine(Teleport());
        StartCoroutine(TeleportWithFog());
    }

    private void FogSettings()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = 0f;
    }

    private IEnumerator Teleport()
    {
        if (portalEffectPrefab != null)
        {
            GameObject warningObject = Instantiate(portalEffectPrefab, instantiatePosition.position, Quaternion.identity);
            FollowPosition followScript = warningObject.GetComponent<FollowPosition>();
            if (followScript != null && instantiatePosition != null)
            {
                followScript.target = instantiatePosition; // Set the XR rig as the target
            }
        }

        if (teleportSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(teleportSound);
        }


        yield return new WaitForSeconds(effectDuration);

        SceneManager.LoadScene(targetScene);
    }

    private IEnumerator TeleportWithFog()
    {
        if (portalEffectPrefab != null)
        {
            GameObject warningObject = Instantiate(portalEffectPrefab, instantiatePosition.position, Quaternion.identity);
            FollowPosition followScript = warningObject.GetComponent<FollowPosition>();
            if (followScript != null)
            {
                followScript.target = instantiatePosition;
            }
        }

        if (teleportSound != null)
        {
            audioSource.PlayOneShot(teleportSound);
        }

        float elapsed = 0f;
        while (elapsed < fogTransitionTime)
        {
            elapsed += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(0f, fogMaxDensity, elapsed / fogTransitionTime);
            yield return null;
        }

        // Wait a bit to make it fully foggy
        yield return new WaitForSeconds(1f);

        // Teleport to the next scene
        SceneManager.LoadScene(targetScene);
    }


}