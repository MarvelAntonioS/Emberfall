using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator transition; 
    public float transitionTime = 1f; 
    public GameObject sceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        //Set the gameobject to false.
        //Essentially, the menu is currently disabled on Start()
        sceneTransition.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (NextScene.SceneChange == true)
        {
            loadTransition();
        }
    }

    private void loadTransition()
    {
        sceneTransition.SetActive(true); 
        transition.SetTrigger("Start");
    }
}
