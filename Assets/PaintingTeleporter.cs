using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PaintingTeleporter : MonoBehaviour
{
    public string targetScene; 
    public GameObject portalEffectPrefab; 
    public AudioClip teleportSound; 
    private float effectDuration = 5f; 
    private AudioSource audioSource;
    

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
            Instantiate(portalEffectPrefab, transform.position, Quaternion.identity);
        }
       
        if (teleportSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(teleportSound);
        }

   
        yield return new WaitForSeconds(effectDuration);

        SceneManager.LoadScene(targetScene);
    }
}
