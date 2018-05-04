using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public GameObject Animal;
    public GameObject GameControl;
    public float Speed;
    public float RotationSpeed;

    private float Direction;
    private Vector3 TargetRotation;
    private Vector3 RotationAmount = new Vector3(0f, 90f, 0f);

	// Use this for initialization
	void Start ()
    {
        //Direction = Animal.transform.rotation.y;
        //TargetRotation = new Vector3(0f, Direction, 0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Animal.transform.eulerAngles = Vector3.Slerp(Animal.transform.eulerAngles, TargetRotation, Time.deltaTime * RotationSpeed);
        //Animal.transform.rotation.SetFromToRotation(Animal.transform.forward, TargetRotation);
        Animal.GetComponent<Rigidbody>().velocity = Animal.transform.forward * Speed;
	}

    void OnCollisionEnter(Collision Other)
    {
        Debug.Log("Collision occured");
        KillAnimal();        
    }
     
    public void TurnAround()
    {
        //Direction = Animal.transform.rotation.y + 90f;
        TargetRotation = new Vector3(0f, 180f, 0f);

        Animal.transform.Rotate(TargetRotation);
    }

    void KillAnimal()
    {
        Animal.GetComponent<Rigidbody>().velocity = Animal.transform.forward * 0;
        Animal.GetComponent<Rigidbody>().useGravity = true;
        Destroy(Animal, 3f);
    }
}
