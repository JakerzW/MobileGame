using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int MaxAI = 8;
    private int CurrentNumAI;

    public GameObject Chicken;
    public GameObject Condor;
    public GameObject Dragon;

    private float MinX = -29f;
    private float MaxX = 29f;
    private float MinY = 2f;
    private float MaxY = 10f;    
    private float MinZ = 13f;
    private float MaxZ = 48f;

    // Use this for initialization
    void Start ()
    {
        CurrentNumAI = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckAI();
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
            CurrentNumAI++;
        }
    }

    GameObject ChooseRandomAI()
    {
        float AINum;
        AINum = Random.Range(1f, 10f);
        if (AINum < 6)
        {
            return Chicken;
        }
        else if (AINum < 9)
        {
            return Condor;
        }
        else
        {
            return Dragon;
        }
    }
}
