using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    public GameObject Arrow;
    public Rigidbody Rb;
    public BoxCollider Coll;
    public Vector3 ArrowTip;

	// Use this for initialization
	void Start ()
    {
        Rb.centerOfMass = ArrowTip;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Arrow.transform.LookAt(Arrow.transform.position + Rb.velocity);      
	}

    void OnCollisionEnter(Collision Other)
    {
        if (Other.gameObject.tag == "Grass")
        {
            Rb.velocity = new Vector3(0f, 0f, 0f);
            Rb.Sleep();
        }
    }
}
