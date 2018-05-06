using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum AnimalType { Chicken, Condor, Dragon };

public class GameController : MonoBehaviour {

    public int MaxAI = 8;
    public int CurrentNumAI;
    
    public GameObject Chicken;
    public GameObject Condor;
    public GameObject Dragon;
    private AnimalType CurrentAnimal;

    private float MinX = -29f;
    private float MaxX = 29f;
    private float MinY = 2f;
    private float MaxY = 10f;    
    private float MinZ = 13f;
    private float MaxZ = 48f;

    public ScoreController Score;
    public int CurrentScore;
    public int AverageFPS;
    public Text FPSText;
    public bool ShowFPS = true;

    public float Timer = 120f;
    public Text TimerText;

    private bool GameActive;
    private AssetBundle Scenes;

    // Use this for initialization
    void Start ()
    {
        CurrentScore = 0;
        CurrentNumAI = 0;
        Score.GetComponent<ScoreController>();
        Scenes = AssetBundle.LoadFromFile("Assets/Scenes");
        GameActive = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckAI();
        UpdateFPS();
        UpdateTimer();
        CheckGameState();
	}

    void UpdateFPS()
    {
        if (ShowFPS)
        {
            FPSText.enabled = true;
            float Current = 0;
            Current = Time.frameCount / Time.time;
            AverageFPS = (int)Current;
            FPSText.text = "FPS: " + AverageFPS.ToString();
        }
        else
            FPSText.enabled = false;
    }

    void UpdateTimer()
    {
        Timer -= Time.deltaTime;

        int Mins = Mathf.FloorToInt(Timer / 60f);
        int Seconds = Mathf.RoundToInt(Timer % 60f);

        if (Seconds == 60)
        {
            Seconds = 0;
            Mins++;
        }

        TimerText.text = Mins.ToString("00") + ":" + Seconds.ToString("00");
    }

    void CheckGameState()
    {
        if (Timer < 0f)
        {
            GameActive = false;
            Debug.Log("Loading Highscore Screen...");
            SceneManager.LoadScene("Highscore Screen");
        }
        //Show highscore and return to menu
    }

    void CheckAI()
    {
        if (CurrentNumAI < MaxAI)
        {
            float X, Y, Z, Rotation;
            GameObject AIChosen;            
            Vector3 SpawnPoint;
            Quaternion SpawnRotation;

            X = Random.Range(MinX, MaxX);
            Y = Random.Range(MinY, MaxY);
            Z = Random.Range(MinZ, MaxZ);
            Rotation = Random.Range(0f, 360f);
            SpawnPoint = new Vector3(X, Y, Z);
            SpawnRotation = Quaternion.Euler(0f, Rotation, 0f);

            AIChosen = Instantiate(ChooseRandomAI(), SpawnPoint, SpawnRotation);
            AIChosen.GetComponent<AIController>().SetAnimalType(CurrentAnimal);
            CurrentNumAI++;
        }
    }

    GameObject ChooseRandomAI()
    {
        float AINum;
        AINum = Random.Range(1f, 10f);
        if (AINum < 6)
        {
            CurrentAnimal = AnimalType.Chicken;
            return Chicken;
        }
        else if (AINum < 9)
        {
            CurrentAnimal = AnimalType.Condor;
            return Condor;
        }
        else
        {
            CurrentAnimal = AnimalType.Dragon;
            return Dragon;
        }
    }

    void OnTriggerExit(Collider Other)
    {
        //Debug.Log("Something exited");
        if (Other.gameObject.tag == "Arrow" || Other.gameObject == null)
            return;
        
        Other.gameObject.GetComponent<AIController>().TurnAround();               
    }

    public void DecrementAINum()
    {
        Debug.Log("Number decremented");
        CurrentNumAI--;
    }

    public void IncreaseScore(int AddedScore)
    {
        CurrentScore += AddedScore;
        Score.ChangeScore(CurrentScore);
    }
}
