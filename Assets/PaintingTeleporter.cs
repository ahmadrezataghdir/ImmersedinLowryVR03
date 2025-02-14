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
        StartCoroutine(Teleport());
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
}
