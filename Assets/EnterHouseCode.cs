using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string sceneName;
    public GameObject main;
    public static bool SceneChange;
    private static GameObject mainInstance;  // Track main object separately

    private void Awake()
    {
        // If mainInstance is null, assign the main object to it and make it persist
        if (mainInstance == null)
        {
            mainInstance = main;
            DontDestroyOnLoad(mainInstance);
        }
        else if (mainInstance != main)  // If mainInstance already exists, destroy the duplicate
        {
            Destroy(main);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
            SceneChange = true; 
        }
    }
}
