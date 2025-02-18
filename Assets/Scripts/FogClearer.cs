using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FogClearer : MonoBehaviour
{
    public float fogClearTime = 3f;

    private void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0.45f;

        StartCoroutine(DisableFogAfterTime(5f));
    }

    private void Update()
    {
        if (RenderSettings.fogDensity > 0f)
        {
            RenderSettings.fogDensity -= Time.deltaTime / fogClearTime;
        }
    }

    private IEnumerator DisableFogAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        RenderSettings.fog = false;
    }
}