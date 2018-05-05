using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public Camera PlayerCamera;
    public GameObject Player;

    private bool GyroEnabled;
    private Gyroscope Gyro;
    private Quaternion GyroRotForward;
    private Quaternion TargetGyroRot;
    public float GyroRotSpeed = 5f;
    public Vector3 PlayerSpawn;

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
    public bool ClampRotation = true;
    public bool lockCursor = true;
    public bool smooth = true;

    private Quaternion xTargetRotation;
    private Quaternion yTargetRotation;
    private bool curserIsLocked = true;
    private float NextFire = 0f;
    private bool IsAiming;


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

        GyroEnabled = EnableGyro();

        IsAiming = false;
    }
	
	// Update is called once per frame
	void Update()
    {
        if (GyroEnabled)
            GyroRotation();
        else
            LookRotation();
        CheckInputs();      
        
    }

    bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Gyro = Input.gyro;
            Gyro.enabled = true;

            Player.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            Player.transform.position = PlayerSpawn;
            GyroRotForward = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }

    void CheckInputs()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).position.x > Screen.width / 2)
        {
            Touch AimTouch = Input.GetTouch(0);

            if (AimTouch.phase == TouchPhase.Began)
            {
                IsAiming = true;
                Aim();
            }
            else if (AimTouch.phase == TouchPhase.Ended)
            {
                IsAiming = false;
                DeAim();
            }        
        }

        if (Input.touchCount == 2 && Input.GetTouch(1).position.x < Screen.width / 2 && IsAiming && Time.time > NextFire)
        {
            Fire();
        }







        //Check which type of input is being used
        /*if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).position.x > Screen.width / 2 && !IsAiming)
                {
                    Aim();
                    IsAiming = true;
                    break;
                }
                else if (Input.GetTouch(i).position.x > Screen.width / 2 && IsAiming)
                {
                    DeAim();
                    IsAiming = false;
                    break;
                }
                if (Input.GetTouch(i).position.x < Screen.width / 2 && IsAiming)
                {
                    Fire();
                    break;
                }
            }
        }*/


        //if (Input.GetMouseButton(1) && Time.time > NextFire)
        //    Aim();
        //else if (!Input.GetMouseButton(1))
        //    DeAim();
    }

    void Aim()
    {
        //Bow.transform.localPosition = Vector3.Slerp(Bow.transform.localPosition, BowAimedPoint, AimSpeed * Time.deltaTime);
        Bow.transform.localPosition = BowAimedPoint;
        PlayerCamera.fieldOfView = 45f;
        GyroRotSpeed = 1f;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Fire();
        //}

        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    if (Input.GetTouch(i).position.x < Screen.width / 2)
        //    {
        //        Fire();
        //        break;
        //    }
        //}
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
        //Bow.transform.localPosition = Vector3.Slerp(Bow.transform.localPosition, BowHipPoint, AimSpeed * Time.deltaTime);
        Bow.transform.localPosition = BowHipPoint;
        PlayerCamera.fieldOfView = 60f;
        GyroRotSpeed = 3f;
    }

    public void GyroRotation()
    {
        //Get current rotation
        //Set target rotation to be added on from current rotation
        //Clamp target rotation

        //Quaternion CurrentGyroRot = Gyro.attitude;

        //TargetGyroRot *= CurrentGyroRot;

        //if (ClampRotation)
        //    TargetGyroRot = ClampRotationAroundBothAxis(TargetGyroRot);

        PlayerCamera.transform.localRotation = Quaternion.Slerp(PlayerCamera.transform.localRotation, Gyro.attitude * GyroRotForward, GyroRotSpeed * Time.deltaTime);//Gyro.attitude * GyroRot;
        //PlayerCamera.transform.localRotation = Quaternion.Slerp(PlayerCamera.transform.localRotation, TargetGyroRot, GyroRotSpeed * Time.deltaTime);
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

    Quaternion ClampRotationAroundBothAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.y);

        angleY = Mathf.Clamp(angleY, MinimumY, MaximumY);

        q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
