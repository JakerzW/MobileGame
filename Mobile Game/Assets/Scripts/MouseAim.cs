using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MouseAim : MonoBehaviour {

    public Camera Camera;
    public GameObject Player;

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public float MinimumX = -25f;
    public float MaximumX = 10f;
    public float MinimumY = -30f;
    public float MaximumY = 30f;
    public float smoothTime = 5f;
    public bool clampVerticalRotation = true;
    public bool clampHorizontalRotation = true;
    public bool lockCursor = true;
    public bool smooth = true;

    private Quaternion xTargetRotation;
    private Quaternion yTargetRotation;
    private bool curserIsLocked = true;

    // Use this for initialization
    void Start ()
    {
        xTargetRotation = Camera.transform.localRotation;
        yTargetRotation = Camera.transform.localRotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        LookRotation();
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

        //Quaternion FinalTargetRotation = xTargetRotation * yTargetRotation;
        //FinalTargetRotation.Set(FinalTargetRotation.x, FinalTargetRotation.y, 0.0f, FinalTargetRotation.w);

        if (smooth)
        {
            //Camera.transform.localRotation = Quaternion.Slerp(Camera.transform.localRotation, FinalTargetRotation, smoothTime * Time.deltaTime);


            Camera.transform.localRotation = Quaternion.Slerp(Camera.transform.localRotation, xTargetRotation,
                smoothTime * Time.deltaTime);
            Player.transform.localRotation = Quaternion.Slerp(Player.transform.localRotation, yTargetRotation,
                smoothTime * Time.deltaTime);
        }
        else
        {
            Camera.transform.localRotation = xTargetRotation;
            Player.transform.localRotation = yTargetRotation;
            //character.localRotation = m_CharacterTargetRot;
            //camera.localRotation = m_CameraTargetRot;
        }
        UpdateCursorLock();
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
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
