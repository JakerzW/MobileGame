using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public Camera PlayerCamera;
    public GameObject Player;

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public float MinimumX = -25f;
    public float MaximumX = 10f;
    public float MinimumY = -50f;
    public float MaximumY = 50f;
    public float smoothTime = 5f;
    public float FireRate = 3f;
    public bool clampVerticalRotation = true;
    public bool clampHorizontalRotation = true;
    public bool lockCursor = true;
    public bool smooth = true;

    private Quaternion xTargetRotation;
    private Quaternion yTargetRotation;
    private bool curserIsLocked = true;
    private float NextFire = 0f;


    public GameObject ArrowPrefab;
    public Transform ArrowSpawn;
    public GameObject Bow;
    //public Camera PlayerCamera;

    public float AimSpeed;
    public float PullbackSpeed;
    public float ArrowSpeed;

    public Vector3 BowHipPoint; //0.35f
    public Vector3 BowAimedPoint; //0.025f
    public Vector3 ArrowStartPoint; //0f
    public Vector3 ArrowAimedPoint; //1f


	// Use this for initialization
	void Start()
    {
        xTargetRotation = PlayerCamera.transform.localRotation;
        yTargetRotation = PlayerCamera.transform.localRotation;
    }
	
	// Update is called once per frame
	void Update()
    {
        CheckInputs();
        LookRotation();
    }

    void CheckInputs()
    {
        if (Input.GetMouseButton(1) && Time.time > NextFire)
            Aim();
        else if (!Input.GetMouseButton(1))
            DeAim();
    }

    void Aim()
    {
        Bow.transform.localPosition = Vector3.Slerp(Bow.transform.localPosition, BowAimedPoint, AimSpeed * Time.deltaTime);
        PlayerCamera.fieldOfView = 45f;
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        NextFire = Time.time + FireRate;
        GameObject Arrow;
        Arrow = Instantiate(ArrowPrefab, ArrowSpawn.position, ArrowSpawn.rotation);
        Arrow.GetComponent<Rigidbody>().velocity = Arrow.transform.forward * ArrowSpeed;
        Destroy(Arrow, 5f);
    }

    void DeAim()
    {
        Bow.transform.localPosition = Vector3.Slerp(Bow.transform.localPosition, BowHipPoint, AimSpeed * Time.deltaTime);
        PlayerCamera.fieldOfView = 60f;
    }

    public void LookRotation()
    {
        float yRotation = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
        float xRotation = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

        yTargetRotation *= Quaternion.Euler(0f, yRotation, 0f);
        xTargetRotation *= Quaternion.Euler(-xRotation, 0f, 0f);

        if (clampVerticalRotation)
            xTargetRotation = ClampRotationAroundXAxis(xTargetRotation);
        if (clampHorizontalRotation)
            yTargetRotation = ClampRotationAroundYAxis(yTargetRotation);

        if (smooth)
        {
            PlayerCamera.transform.localRotation = Quaternion.Slerp(PlayerCamera.transform.localRotation, xTargetRotation,
                smoothTime * Time.deltaTime);
            Player.transform.localRotation = Quaternion.Slerp(Player.transform.localRotation, yTargetRotation,
                smoothTime * Time.deltaTime);
        }
        else
        {
            PlayerCamera.transform.localRotation = xTargetRotation;
            Player.transform.localRotation = yTargetRotation;
        }
        UpdateCursorLock();
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            curserIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            curserIsLocked = true;
        }

        if (curserIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!curserIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
    Quaternion ClampRotationAroundYAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.y);

        angleY = Mathf.Clamp(angleY, MinimumY, MaximumY);

        q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

        return q;
    }
}
