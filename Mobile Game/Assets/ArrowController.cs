using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    public GameObject Arrow;
    public Rigidbody Rb;
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
}
