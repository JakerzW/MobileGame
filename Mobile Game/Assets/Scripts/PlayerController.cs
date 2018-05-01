using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject BulletPrefab;
    public GameObject Bow;
    public Camera PlayerCamera;

    private Transform ArrowSpawn;

    public float AimSpeed;
    public float ArrowSpeed;

    public Vector3 BowHipPoint; //0.35f
    public Vector3 BowAimedPoint; //0.025f
    public Vector3 ArrowStartPoint; //0f
    public Vector3 ArrowAimedPoint; //1f


	// Use this for initialization
	void Start()
    {
        ArrowSpawn.localPosition.Set(1f, 0.1f, 0f);
	}
	
	// Update is called once per frame
	void Update()
    {
        CheckInputs();
        if (Input.GetMouseButtonDown(0))
            Fire();
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
        Arrow = Instantiate(BulletPrefab, ArrowSpawn);
        Arrow.GetComponent<Rigidbody>().velocity = Arrow.transform.forward * ArrowSpeed;
        Destroy(Arrow, 5f);
    }

    void DeAim()
    {
        Bow.transform.localPosition = Vector3.Slerp(Bow.transform.localPosition, BowHipPoint, AimSpeed * Time.deltaTime);
        PlayerCamera.fieldOfView = 60f;
    }
}
