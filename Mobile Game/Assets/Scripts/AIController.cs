using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public GameObject Animal;
    public float Speed;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Animal.transform.LookAt(Animal.transform.forward);
        
        Animal.GetComponent<Rigidbody>().velocity = Animal.transform.forward * Speed;
	}

    void OnTriggerExit(Collider GameArea)
    {
        if (GameArea.gameObject.tag != "GameArea")
        {
            return;
        }
        //If the object exits the game space, turn the object 90 degrees
        Vector3 NewDirection;
        NewDirection = (Animal.transform.forward + new Vector3(0f, 90f, 0f));
        Animal.transform.rotation = Quaternion.FromToRotation(Animal.transform.forward, NewDirection);


        //Quaternion NewDirection;
        //float Step;
        
        //NewDirection = Quaternion.Euler(Animal.transform.rotation.x, Animal.transform.rotation.y + 90f, Animal.transform.rotation.z);
        //Step = Speed * Time.deltaTime;

        //Animal.transform.rotation = Quaternion.RotateTowards(Animal.transform.rotation, NewDirection, Step);
    }
     
}
