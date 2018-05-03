using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject ArrowPrefab;
    public Transform ArrowSpawn;
    public GameObject Bow;
    public Camera PlayerCamera;

    public float AimSpeed;
    public float ArrowSpeed;

    public Vector3 BowHipPoint; //0.35f
    public Vector3 BowAimedPoint; //0.025f
    public Vector3 ArrowStartPoint; //0f
    public Vector3 ArrowAimedPoint; //1f


	// Use this for initialization
	void Start()
    {
    
	}
	
	// Update is called once per frame
	void Update()
    {
        CheckInputs();
    }

    void CheckInputs()
    {
        if (Input.GetMouseButton(1))
            Aim();
        else
            DeAim();
    }

    void Aim()
    {
        Bow.transform.localPosition = Vector3.Slerp(Bow.transform.localPosition, BowAimedPoint, AimSpeed * Time.deltaTime);
        PlayerCamera.fieldOfView = 45f;
        if (Input.GetMouseButtonDown(0))
            Fire();
    }

    void Fire()
    {
        GameObject Arrow;
        //Transform OriginalSpawn = ArrowSpawn;
        Arrow = Instantiate(ArrowPrefab, ArrowSpawn.position, ArrowSpawn.rotation);
        Arrow.GetComponent<Rigidbody>().velocity = Arrow.transform.forward * ArrowSpeed;
        Destroy(Arrow, 30f);
    }

    void DeAim()
    {
        Bow.transform.localPosition = Vector3.Slerp(Bow.transform.localPosition, BowHipPoint, AimSpeed * Time.deltaTime);
        PlayerCamera.fieldOfView = 60f;
    }
}
