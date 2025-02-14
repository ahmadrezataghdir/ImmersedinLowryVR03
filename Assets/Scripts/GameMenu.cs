using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameScene()
    {

        SceneManager.LoadScene(sceneName);
    }

    public void OnExitButtonClick()
    {
        #if UNITY_EDITOR
                // If in the Unity Editor, stop play mode
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // If in a standalone build, quit the application
                Application.Quit();
        #endif
    }
}
