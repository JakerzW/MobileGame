using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

    private AssetBundle Scenes;

	// Use this for initialization
	void Start ()
    {
        Scenes = AssetBundle.LoadFromFile("Assets/Scenes");
        	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayGame()
    {
        Debug.Log("Loading Scene...");
        SceneManager.LoadScene("Main Scene");
    }
}
