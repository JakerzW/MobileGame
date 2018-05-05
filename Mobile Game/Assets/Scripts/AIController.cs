using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public GameObject Animal;
    public GameController GameControl;
    public Rigidbody Rb;
    public float Speed;
    public float RotationSpeed;
    public int Score;

    public AnimalType AnimalName;
    private float Direction;
    private Vector3 TargetRotation;
    private Vector3 RotationAmount = new Vector3(0f, 90f, 0f);

	// Use this for initialization
	void Start ()
    {
        //Animal.GetComponent<AIController>();
        GameControl = FindObjectOfType<GameController>();
        Rb = FindObjectOfType<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Rb.velocity = Animal.transform.forward * Speed;
    }

    void OnCollisionEnter(Collision Other)
    {
        if (Other.gameObject.tag == "Arrow")
        {
            //Debug.Log("Arrow detected on animal");
            KillAnimal();
            GameControl.IncreaseScore(Score);          
        }            
        else if (Other.gameObject.tag == "Grass")
        {
            //Debug.Log("Collision occured");
            KillAnimal();
        }
        else if (Other.gameObject.tag == "Animal")
        {
            TurnAround();
        }              
    }
     
    public void TurnAround()
    {
        //Direction = Animal.transform.rotation.y + 90f;
        TargetRotation = new Vector3(0f, 180f, 0f);

        Animal.transform.Rotate(TargetRotation);
    }

    void KillAnimal()
    {
        Destroy(Animal, 1f);
        
        GameControl.DecrementAINum();
    }

    public void SetAnimalType(AnimalType TypeChosen)
    {
        AnimalName = TypeChosen;
    }
}
