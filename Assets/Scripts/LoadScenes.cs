using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public string[] scenes;
	void Start ()
    {
		for(int i = 0; i < scenes.Length; ++i)
        {
            SceneManager.LoadScene(scenes[i], LoadSceneMode.Additive);
        }
	}
}
