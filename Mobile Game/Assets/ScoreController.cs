using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    private int CurrentScore;
    public Text Score;

	// Use this for initialization
	void Start ()
    {
        CurrentScore = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Score.text = CurrentScore.ToString();
	}

    public void ChangeScore(int NewScore)
    {
        CurrentScore = NewScore;
    }
}
