using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreController : MonoBehaviour {

    private int Highscore;
    private int CurrentScore;
    public Text HighscoreText;
    public Text CurrentScoreText;

    private AssetBundle Scenes;

    // Use this for initialization
    void Start ()
    {
        CurrentScore = PlayerPrefs.GetInt("CurrentScore");

        if (!PlayerPrefs.HasKey("Highscore"))
        {
            Debug.Log("Setting Highscore 1");
            PlayerPrefs.SetInt("Highscore", CurrentScore);
            Highscore = CurrentScore;
        }
        else if (PlayerPrefs.GetInt("Highscore") < CurrentScore)
        {
            Debug.Log("Setting Highscore 2");
            PlayerPrefs.SetInt("Highscore", CurrentScore);
            Highscore = CurrentScore;
        }
        else
        {
            Debug.Log("Setting Highscore 3");
            Highscore = PlayerPrefs.GetInt("Highscore");
        }        

        ShowScores();

        Scenes = AssetBundle.LoadFromFile("Assets/Scenes");
    }
	
    void ShowScores()
    {
        HighscoreText.text = Highscore.ToString();
        CurrentScoreText.text = "Your Score: " + CurrentScore.ToString();
    }

    public void ExitToMainMenu()
    {
        Debug.Log("Loading Title Screen...");
        SceneManager.LoadScene("Title Screen");
    }

	// Update is called once per frame
	void Update () {
        
    }
}
